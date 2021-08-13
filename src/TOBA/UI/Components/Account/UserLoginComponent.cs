using System;
using System.ComponentModel;
using System.Windows.Forms;

using TOBA.Configuration;
using TOBA.UI.Dialogs.Account;

namespace TOBA.UI.Components.Account
{
	using AutoVc;

	using Dialogs;
	using Dialogs.Common;
	using Dialogs.Vc;

	using Service;

	using TOBA.Account;

	using WebLib;

	internal partial class UserLoginComponent : Component
	{
		/// <summary>
		/// 密码错误时是否允许回滚到会话框模式
		/// </summary>
		public bool EnableFallback { get; set; }

		public UserLoginComponent()
		{
			InitializeComponent();
		}

		public UserLoginComponent(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		public IWin32Window OwnerForm { get; set; }

		public void RunLoginProcedure(RequireSessionLoginWorker worker)
		{
			using (var wd = new YetAnotherWaitingDialog())
			{
				var vccount = 0;
				IVerifyCodeRecognizeResult vcResult = null;

				wd.WorkCallback = worker.DoLogin;
				worker.StateChanged += (ss, se) =>
				{
					wd.SetState(worker.State.ToExecutionState(), worker.Message);
				};
				worker.VerifyCodeError += (x, y) =>
				{
					if (vcResult != null && VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine != null)
					{
						VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine.MarkResult(vcResult, false);
						vcResult = null;
					}
				};
				worker.RequireEnterVerifyCode += (ss, ee) =>
				{
					wd.Invoke(() =>
					{
						if (vccount > 5 && ProgramConfiguration.Instance.AutoEnterLoginVcCode)
						{
							MessageDialog.Information("亲，验证码自动识别失败超过限制，自动识别已经关闭，请手动输入验证码。");
						}
						//var vcform = new UI.Dialogs.RequireVcCode
						//{
						//	Session = ee.Session,
						//	EnableAutoVc = vccount < 6 && ProgramConfiguration.Instance.AutoEnterLoginVcCode,
						//	AutoVcCount = vccount
						//};
						//if (vcform.ShowDialog(OwnerForm) == DialogResult.OK)
						//{
						//	vcResult = vcform.VcResult;
						//	vccount = vcform.AutoVcCount;
						//	ee.VerifyCode = vcform.Code;

						//	if (vcResult != null)
						//	{
						//		//report
						//		ApiEvents.OnAutoVc(null, new GeneralEventArgs<AutoVcLog>(new AutoVcLog()
						//																{
						//																	Account12306 = worker.UserName,
						//																	TypeID = 0,
						//																	TodayUsed = AutoVcBaseLimition.GetVcUsed()
						//																}));

						//	}
						//}
						var vc = new TouchClickVcSimpleForm(ee.Session)
						{
							RandCodeType = RandCodeType.SjRand,
							EnableAutoVc = vccount < 6 && ProgramConfiguration.Instance.AutoEnterLoginVcCode
						};
						if (vc.ShowDialog(OwnerForm) == DialogResult.OK)
						{
							ee.VerifyCode = vc.Code;
							vcResult = vc.AutoVcResult;

							if (vcResult != null)
							{
								vccount++;
							}
						}
					});
					vccount++;
				};

				wd.ShowDialog(OwnerForm);

				if (EnableFallback && worker.Session == null && (string.IsNullOrEmpty(worker.Message) || worker.Message.IndexOf("密码输入错误") != -1))
				{
					//返回
					using (var logindlg = new Login()
					{
						PreSelectUser = worker.UserName
					})
					{
						logindlg.ShowDialog(OwnerForm);
					}
				}
				else if (worker.State == OpearationState.Blocked && !worker.Message.IsNullOrEmpty())
				{
					MessageDialog.Error(OwnerForm, "尝试登录的时候遇到了问题：\n\n" + worker.Message);
				}
				else if (wd.Exception != null)
				{
					MessageDialog.Error(OwnerForm, "尝试登录的时候遇到了问题：\n\n" + wd.Exception.ToString());
				}
				if (worker.Session != null && worker.LoginConflict)
				{
					MessageDialog.Information(OwnerForm, "此次登录检测到会话冲突，如果您此时还在其它的软件或浏览器上登录此账号，它们将会被12306无情地注销掉。\n\n世界就是这样的残酷，多坑点身份证注册几个账号吧……");
				}
			}

		}
	}
}
