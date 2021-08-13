using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Passenger
{
	using Controls.Common;

	using Data;

	internal partial class ImportPassenger : DialogBase
	{
		OpenFileDialog _openFileDialog = new OpenFileDialog()
		{
			Title = "打开联系人列表...",
			Filter = "联系人列表文件(*.txt;*.lst)|*.txt;*.lst"
		};

		bool _importCanceled = false;

		public ImportPassenger()
		{
			InitializeComponent();

			Load += ImportPassenger_Load;
			KeyPreview = true;
			KeyUp += ImportPassenger_KeyUp;
		}

		#region 导入

		void BeginImport()
		{
			var target = lst.Items.Cast<ListViewItem>().Where(s => s.Tag != null).Select(s => new KeyValuePair<ListViewItem, Entity.Web.Passenger>(s, s.Tag as Entity.Web.Passenger)).ToQueue();
			if (!target.Any())
			{
				this.Information("没有需要要导入的联系人 ⊙﹏⊙!");
				return;
			}

			ThreadPool.QueueUserWorkItem(_ =>
			{
				AppContext.HostForm.Invoke(() =>
				{
					btnOk.Enabled = false;
					loading.Visible = true;
				});

				try
				{
					var worker = new Otn.Workers.PassengerManager() { Session = Session };
					while (!_importCanceled && target.Count > 0)
					{
						var item = target.Dequeue();
						AppContext.HostForm.Invoke(() =>
						{
							item.Key.ImageIndex = 1;
							ListViewResource.SwitchListViewItemStyle(item.Key, RowStyleType.DeepBlue);
							item.Key.EnsureVisible();
						});
						var result = worker.AddPassenger(item.Value);
						if (result.IsNullOrEmpty())
						{
							AppContext.HostForm.Invoke(() =>
							{
								item.Key.ImageIndex = 2;
								ListViewResource.SwitchListViewItemStyle(item.Key, RowStyleType.Green);
								item.Key.Tag = null;
							});
						}
						else
						{
							AppContext.HostForm.Invoke(() =>
							{
								item.Key.ImageIndex = 3;
								ListViewResource.SwitchListViewItemStyle(item.Key, RowStyleType.Red);
								item.Key.Tag = null;
							});
						}
					}
				}
				catch (Exception ex)
				{
					this.Error("导入的过程中出现错误：" + ex.ToString());
					Trace.TraceError(ex.ToString());
				}

				AppContext.HostForm.Invoke(() =>
				{
					btnOk.Enabled = true;
					loading.Visible = false;
					if (!_importCanceled)
						this.Information("导入完成 ♪(´▽｀)");
					DialogResult = DialogResult.OK;
					Close();
				});
			});
		}

		#endregion

		void ImportPassenger_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.V)
				ImportFromClipboard();
		}

		void ImportPassenger_Load(object sender, EventArgs ee)
		{
			btnFromCb.Click += (s, e) => ImportFromClipboard();
			btnFromFile.Click += (s, e) => ImportFromFile();
			btnOk.Click += (s, e) => BeginImport();
			imgList.Images.Add(UiUtility.Get24PxImageFrom16PxImg(Properties.Resources.user_16));
			imgList.Images.Add(UiUtility.Get24PxImageFrom16PxImg(Properties.Resources.ArrowNormal));
			imgList.Images.Add(UiUtility.Get24PxImageFrom16PxImg(Properties.Resources.tick_16));
			imgList.Images.Add(UiUtility.Get24PxImageFrom16PxImg(Properties.Resources.delete_16));
		}

		void ImportFromFile()
		{
			if (_openFileDialog.ShowDialog() != DialogResult.OK)
				return;

			DoImport(File.ReadAllText(_openFileDialog.FileName));
		}

		void ImportFromClipboard()
		{
			var data = Clipboard.GetDataObject();
			if (data == null)
			{
				this.Information("剪贴板中木有任何数据...");
				return;
			}
			if (data.GetDataPresent(DataFormats.Text))
			{
				DoImport(data.GetData(DataFormats.Text) as string);
			}
			else
			{
				this.Information("剪贴板中木有任何可以识别的数据...");
				return;
			}

		}


		static Regex _passengerAnalyzeReg = new Regex(@"^\s*(成人|儿童|学生|残军),\s*([^,\s]+?),\s*([一二]代身份证|(港澳|台湾)通行证|护照),\s*([^,\s]+?),\s*(\d+)?$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

		void DoImport(string text)
		{
			var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Where(s => !s.StartsWith("#")).ToArray();
			if (lines.IsEmpty())
			{
				this.Information("没有可以导入的乘客信息...");
				return;
			}

			if (_passengerAnalyzeReg.IsMatch(lines[0]))
			{
				DoImport(lines.Select(s =>
				{
					var m = _passengerAnalyzeReg.Match(s);
					if (!m.Success) return null;

					var m1 = new Entity.Web.Passenger();
					m1.Name = m.Groups[2].Value;
					m1.Type = ParamData.GetPassengerTypeByName(m.Groups[1].Value);
					m1.SetId(ParamData.GetPassengerIdTypeCodeByName(m.Groups[3].Value), m.Groups[5].Value);
					m1.MobileNo = m.Groups[6].Value;

					return m1.IdTypeCode == '\0' || m1.Type == 0 ? null : m1;
				}).ExceptNull().ToArray());

				return;
			}

			this.Information("很抱歉，暂时无法支持您尝试导入的数据。\n如果您尝试导入的数据是其他软件生成的，请向作者反应，并附上导出的数据。");
		}

		void DoImport(Entity.Web.Passenger[] list)
		{
			//去掉重复的。
			var target = list
				.Where(s => s.Type != 3 && !Session.UserProfile.Passengers.Any(x => x.Name == s.Name && s.IdNo == x.IdNo) && !lst.Items.Cast<ListViewItem>().Any(x => x.Tag != null && (x.Tag as Entity.Web.Passenger).Name == s.Name && (x.Tag as Entity.Web.Passenger).IdNo == s.IdNo)).ToList();
			if (target.Count == 0)
			{
				this.Information("没有可以导入的乘客信息，或所有的联系人已经在当前账号中导入.");
				return;
			}

			//追加
			lst.Items.AddRange(target.Select(s =>
			{
				var item = new ListViewItem(new[] { s.Name, ParamData.PassengerType.GetValue(s.Type), ParamData.PassengerIdType.GetValue(s.IdTypeCode), s.IdNo, s.MobileNo }, 0)
				{
					Tag = s
				};
				ListViewResource.SwitchListViewItemStyle(item, RowStyleType.Blue);
				return item;
			}).ToArray());
		}

		/// <summary>
		/// 引发 <see cref="E:System.Windows.Forms.Form.FormClosing"/> 事件。
		/// </summary>
		/// <param name="e">一个包含事件数据的 <see cref="T:System.Windows.Forms.FormClosingEventArgs"/>。</param>
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			_importCanceled = true;
		}
	}
}
