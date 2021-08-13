namespace TOBA.Platform.DeviceFingerprint
{
	using FSLib.Network.Http;

	using MsieJavaScriptEngine;

	using Newtonsoft.Json;

	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Reflection;
	using System.Text.RegularExpressions;

	class FingerprintService : IFingerprintService
	{
		private MsieJsEngine _engine;

		public bool IsInitialized { get; private set; }

		/// <inheritdoc />
		public IFingerprintInfo GetFingerprintInfoFromSession(Session session)
		{
			return session.GetSessionData<IFingerprintInfo>(HostContext.SessionDataKey);
		}

		/// <summary>
		/// 初始化
		/// </summary>
		/// <returns></returns>
		public bool Initialize()
		{
			if (!TryInitializeJsEngine())
				throw new Exception($"无法初始化脚本引擎");

			_engine.EmbedHostObject("host", this);
			IsInitialized = true;

			return IsInitialized;
		}

		bool TryInitializeJsEngine()
		{
			var testModes = new[] { JsEngineMode.Classic, JsEngineMode.ChakraActiveScript, JsEngineMode.ChakraIeJsRt };

			foreach (var mode in testModes)
			{
				try
				{
					_engine = new MsieJsEngine(new JsEngineSettings
					{
						EngineMode = mode
					});
					break;
				}
				catch (Exception e)
				{
					Trace.TraceError($"无法初始化脚本引擎 模式->{mode} 信息->{e.Message}");
				}
			}

			return _engine != null;
		}

		public (bool success, string message) InitTagStatus(HostContext context, string js = null)
		{
			var client = context.Session.NetClient;

			if (js.IsNullOrEmpty())
			{
				var jsCtx = client.Create<string>(HttpMethod.Get, context.ResourceUrl);
				jsCtx.Send();
				if (!jsCtx.IsValid())
				{
					return (false, "[ZA0002] RESOURCE LOAD ERROR DUE TO " + jsCtx.GetExceptionMessage("UNKNOWN") + " (" + jsCtx.Status + ")");
				}

				js = jsCtx.Result;
			}

			if (js.IsNullOrEmpty())
				return (true, null);

			var algIdM = Regex.Match(js, @"[""']\?algID\\x3d(.*?)\\x26", RegexOptions.Singleline);
			var tagM = Regex.Match(js, @"getCookieCode:.*?{.*?\(['""]([^'""]+)['""]", RegexOptions.Singleline);
			if (!algIdM.Success || !tagM.Success)
			{
				return (false, "[ZA0001] PARAMDATA/ALGID/TAGM/TAGS NOT FOUND!");
			}

			var algId = algIdM.GetGroupValue(1);
			var tag = tagM.GetGroupValue(1);

			context.CookieID = tag;
			context.FingerprintInfo = new FingerprintInfo() { AlgorithmId = algId };
			context.Js = js;
			context.DeviceData = context.GetDeviceInfo();

			return (true, $"ALGID => {algId} TAG => {tag}");
		}

		public (bool success, string message) BuildDeviceFingerprint(HostContext host)
		{
			var jsContent = host.Js;
			if (jsContent.IsNullOrEmpty())
				return (true, null);

			var param = "/*--seems that here no need to extract data--*/"; //Regex.Match(jsContent, @"\.VERSION=.*?(var\s+.*?;)if", RegexOptions.Singleline).GetGroupValue(1);
			var functionMaskM = Regex.Match(jsContent, @"initEc\s*:\s*function\s*\(([\$-_\w,\s,]+)\)(.*?)(?<=\.NeedUpdate[\(\)\{\}]{5})(.*?)[\w-_\$]+?\.getJSON", RegexOptions.Singleline);
			var fpVer = Regex.Match(jsContent, @"fp_ver""\s*,\s*""([\.\d]+)", RegexOptions.Singleline).GetGroupValue(1);
			var funmap = new Dictionary<string, string>();
			var outVarM = Regex.Match(jsContent, @"hashCode\\x3d.*?\+\s*([\w-_\$]+)\s*\+\s*([\w-_\$]+)", RegexOptions.Singleline);
			if (!functionMaskM.Success || param.IsNullOrEmpty() || !outVarM.Success)
			{
				return (false, "[FP0003] Failed to detect function body.");
			}

			host.FingerprintInfo.FpVersion = fpVer;


			var outVarSign = outVarM.GetGroupValue(1);
			var outVarQuery = outVarM.GetGroupValue(2);

			//extract variable pre context;
			var prevars = functionMaskM.GetGroupValue(1).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => $"var {s}=undefined;").JoinAsString("");
			prevars += Regex.Matches(functionMaskM.GetGroupValue(2), @"var\s+[^;]+;", RegexOptions.Singleline).Cast<Match>().Select(s => s.Value).JoinAsString();

			var functionMask = functionMaskM.GetGroupValue(3);
			functionMask = Regex.Replace(functionMask, @"[\w-_\$]+\.getpackStr\([\w-_\$]+\)", JsonConvert.SerializeObject(host.DeviceData));

			//embed hashAlg
			var hashAlg = Regex.Match(jsContent, @"hashAlg:\s*function\s*(\([^\)]*\)\s*{.*?return[^}]+?}(?=,))", RegexOptions.IgnoreCase | RegexOptions.Singleline).GetGroupValue(1);
			if (hashAlg.IsNullOrEmpty())
			{
				hashAlg = Regex.Match(jsContent, @"hashAlg:\s*function\s*(\([^\)]*\)\s*{.*?return.+?}(?=};\s*var\s*))", RegexOptions.IgnoreCase | RegexOptions.Singleline).GetGroupValue(1);
			}

			if (hashAlg.IsNullOrEmpty())
			{
				return (false, "[FP0005] Failed to detect hashalg function!");
			}

			functionMask = $"function hashAlg{hashAlg}" + Regex.Replace(functionMask, @"[a-z\d]+\.hashAlg", "hashAlg");


			var functions = Regex.Matches(functionMask, @"(?:=([\w-_\$]+)\(|\(new\s+([\w-_\$]+)\()", RegexOptions.Singleline).Cast<Match>().Select(s => s.GetGroupValue(1).DefaultForEmpty(s.GetGroupValue(2))).Distinct().ToArray();
			foreach (var fn in functions)
			{
				if (fn == "hashAlg")
					continue;

				var regU = Regex.Match(jsContent, $@"function\s+{fn}\s*\([-_\$\w\,\s]+\)\s*{{.*?}}(?=\s*function)", RegexOptions.Singleline);
				if (!regU.Success)
				{
					return (false, $"[FP0004] Failed to detect utility function [{fn}]!");
				}

				funmap[fn] = regU.Value;
			}

			var script = @"Array.prototype.indexOf||(Array.prototype.indexOf=function(a,b){var c;if(null==this)throw new TypeError(""'this' is null or undefined"");var d=Object(this),e=d.length>>>0;if(0===e)return-1;c=+b||0;Infinity===Math.abs(c)&&(c=0);if(c>=e)return-1;for(c=Math.max(0<=c?c:e-Math.abs(c),0);c<e;){if(c in d&&d[c]===a)return c;c++}return-1});";

			//定义变量
			var arrayConsts = Regex.Matches(functionMask, @"=\s*([a-zA-Z\d]{2,})(\.indexOf|\[)", RegexOptions.IgnoreCase).Cast<Match>().Select(s => s.GetGroupValue(1)).ToArray();
			if (arrayConsts.Length > 0)
			{
				var arrayConstsData = Regex.Matches(jsContent, $"({arrayConsts.JoinAsString("|")})\\s*=\\s*(\\[[^\\]]+\\]|['\"][^'\"]+['\"](\\.[^;,]+)?(?=[;,])|{{[^}}]+}})").Cast<Match>().ToDictionary(s => s.GetGroupValue(1), s => s.GetGroupValue(2));
				script += ";;;/*1*/;;;" + arrayConsts.Select(s => $"var {s}={arrayConstsData.GetValue(s).DefaultForEmpty("''")};").JoinAsString(string.Empty);
			}

			script += ";;;/*2*/;;;" + prevars + param + funmap.Values.JoinAsString(";");
			script += @";;;/*3*/;;;" + functionMask + ";;;/*4*/;;;";
			script = Regex.Replace(script, @"(?<=|return\s*)[\w-_\$]+\.SHA256\(([\w-_\$]+)\)\.toString\([\w-_\$]+\.enc\.Base64\)(?=$|;|}|{)", "host.AAA($1);", RegexOptions.IgnoreCase);

			var success = false;
			var msg = "";
			AppContext.HostForm.Invoke(new Action(() =>
			{
				try
				{
					var se = _engine;
					se.Execute(script);
					host.Sign = se.GetVariableValue(outVarSign).ToString();
					host.Query = se.GetVariableValue(outVarQuery).ToString();

					success = true;
				}
				catch (Exception ex)
				{
					Trace.TraceError(ex.ToString());
					Trace.TraceInformation(script);
					msg = "[ZA0005] Error build signature: " + ex.Message;
				}
			}));

			if (!success)
			{
				Events.OnWarning(this, new EventInfoArgs(msg));
			}

			return (success, success ? null : msg);
		}

		public (bool success, string message) PostFpInfo(HostContext host)
		{
			var client = host.Session.NetClient;
			if (host.FingerprintInfo == null)
				return (true, null);


			var postUrl = $"/otn/HttpZF/logdevice?algID={host.FingerprintInfo.AlgorithmId}&hashCode={host.Sign}{host.Query}";
			var ctx = client.Create<string>(HttpMethod.Get, postUrl, postUrl);
			ctx.Send();

			if (!ctx.IsValid())
				return (false, "[ZA0002] Unable to post log request. error: " + ctx.GetExceptionMessage().DefaultForEmpty("未知错误"));

			var data = Regex.Match(ctx.Result, @"{.*?}").GetGroupValue(0);
			if (!ctx.IsValid())
				return (false, "[ZA0003] Not recognizable message: " + ctx.Result);

			try
			{
				var result = JsonConvert.DeserializeAnonymousType(data,
					new
					{
						exp = 0L,
						cookieCode = "",
						dfp = ""
					});
				var fp = host.FingerprintInfo;
				fp.Dfp = result.dfp;
				fp.CookieCode = result.cookieCode;
				fp.Expire = System.FishDateTimeExtension.JsTicksStartBase.AddMilliseconds(result.exp);
				fp.Expiration = result.exp;

				return (true, null);
			}
			catch (Exception ex)
			{
				return (false, "[ZA0004] Not recognizable message: " + data);
			}
		}

		public void ClearFpInfo(Session session)
		{
			new HostContext(session).ClearFinterprintInfo();
		}

		[SmartAssembly.Attributes.DoNotObfuscate]
		[Obfuscation(Exclude = false, Feature = "-rename")]
		public string AAA(string str) => Utility.Sha256WithBase64(str);
	}
}
