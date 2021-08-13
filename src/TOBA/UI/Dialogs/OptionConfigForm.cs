namespace TOBA.UI.Dialogs
{
	using Controls;

	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Drawing;
	using System.Linq;
	using System.Windows.Forms;

	public partial class OptionConfigForm : Form
	{
		IOptionConfigUI _currentConfigUI;

		public OptionConfigForm()
		{
			InitializeComponent();

			EnableResize = false;
			optList.SelectedIndexChanged += optList_SelectedIndexChanged;
		}

		void optList_SelectedIndexChanged(object sender, EventArgs e)
		{
			var item = optList.SelectedItem as IOptionConfigUI;
			if (_currentConfigUI == item)
				return;

			_currentConfigUI = item;
			panelMain.Controls.Clear();
			if (item != null)
			{
				var ctl = item.OptionPanel;
				ctl.Dock = DockStyle.Fill;
				panelMain.Controls.Add(ctl);

				lblTitle.Text = item.DisplayText;
				pbIcon.Image = item.BigImage ?? item.Image;
			}
			else
			{
				pbIcon.Image = null;
				lblTitle.Text = "请选择一个配置页";
			}
		}

		/// <summary>
		/// 获得或设置当前设置的选项页
		/// </summary>
		public IOptionConfigUI SelectedConfig
		{
			get => optList.SelectedItem as IOptionConfigUI;
			set => optList.SelectedItem = value;
		}

		/// <summary>
		/// 选定指定的选项卡
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public bool SelectConfigUI<T>() where T : IOptionConfigUI
		{
			var item = FindConfigUI<T>().FirstOrDefault();
			if (item != null)
				SelectedConfig = item;

			return item != null;
		}

		/// <summary>
		/// 查找指定的选项卡
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public IEnumerable<T> FindConfigUI<T>() where T : IOptionConfigUI
		{
			return optList.Items.OfType<T>();
		}

		/// <summary>
		/// 获得或设置是否允许调整宽度
		/// </summary>
		[DefaultValue(false)]
		public bool EnableResize
		{
			get => !splitContainer1.IsSplitterFixed;
			set => splitContainer1.IsSplitterFixed = !value;
		}

		/// <summary>
		/// 添加配置选项
		/// </summary>
		/// <param name="ui"></param>
		public void AddOption(IOptionConfigUI ui)
		{
			if (ui == null) return;

			if (optList.Items.Contains(ui)) return;
			optList.Items.Add(ui);
			if (optList.SelectedIndex == -1) optList.SelectedIndex = 0;
		}

		public interface IOptionConfigUI : Controls.Common.IListBoxExItem
		{
			/// <summary>
			/// 配置面板
			/// </summary>
			Control OptionPanel { get; }

			/// <summary>
			/// 获得16PX大图像
			/// </summary>
			Image BigImage { get; }

			/// <summary>
			/// 请求保存
			/// </summary>
			/// <returns></returns>
			bool Save();

		}

		/// <summary>
		/// 抽象配置基类
		/// </summary>
		internal class AbstractOptionConfigUI : ControlBase, IOptionConfigUI
		{
			public AbstractOptionConfigUI()
			{
				Size = new Size(535, 366);
			}

			/// <summary>
			/// 获得文字
			/// </summary>
			public virtual string DisplayText
			{
				get;
				set;
			}


			#region Implementation of IListBoxExItem

			/// <summary>
			/// 获得图片
			/// </summary>
			public virtual Image Image { get; set; }

			/// <summary>
			/// 获得副标题
			/// </summary>
			public virtual string Description { get; set; }

			/// <summary>
			/// 获得是否应该阻止默认的绘图
			/// </summary>
			public virtual bool PreventDefaultDrawing { get; set; }

			/// <summary>
			/// 调用项目
			/// </summary>
			/// <param name="e"></param>
			public virtual void MeasureItem(MeasureItemEventArgs e) { }

			/// <summary>
			/// 绘制内容
			/// </summary>
			/// <param name="e"></param>
			public virtual void DrawItem(DrawItemEventArgs e) { }

			/// <summary>
			/// 文字颜色
			/// </summary>
			public virtual Color? ForeColor { get; set; }

			#endregion

			#region Implementation of IOptionConfigUI

			/// <summary>
			/// 配置面板
			/// </summary>
			[Browsable(false)]
			public virtual Control OptionPanel { get { return this; } }

			/// <summary>
			/// 获得32PX大图像
			/// </summary>
			public virtual Image BigImage { get; set; }


			/// <summary>
			/// 请求保存
			/// </summary>
			/// <returns></returns>
			public virtual bool Save() { return true; }

			#endregion
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.OK;
			Close();
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);

			if (!QuerySave()) e.Cancel = true;
		}

		/// <summary>
		/// 请求关闭
		/// </summary>
		/// <returns>返回false阻止关闭</returns>
		protected virtual bool QuerySave()
		{
			foreach (IOptionConfigUI ui in optList.Items)
			{
				if (!ui.Save()) return false;
			}

			return true;
		}
	}
}
