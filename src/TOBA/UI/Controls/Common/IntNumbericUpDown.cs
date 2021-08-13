namespace TOBA.UI.Controls.Common
{
	using System;

	/// <summary>
	/// 支持Int数值的编辑器
	/// </summary>
	[SmartAssembly.Attributes.DoNotPruneType, SmartAssembly.Attributes.DoNotObfuscate]
	public class IntNumbericUpDown : System.Windows.Forms.NumericUpDown
	{

		/// <summary>
		/// 当前编辑值的整数部分
		/// </summary>
		public int IntValue
		{
			get { return (int)Value; }
			set
			{
				Value = value;
			}
		}

		/// <summary>
		/// <see cref="IntValue"/> 发生变化
		/// </summary>
		public event EventHandler IntValueChanged;

		/// <summary>
		/// 引发 <see cref="IntValueChanged" /> 事件
		/// </summary>
		protected virtual void OnIntValueChanged()
		{
			var handler = IntValueChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}


		/// <summary>
		/// 引发 <see cref="E:System.Windows.Forms.NumericUpDown.ValueChanged"/> 事件。
		/// </summary>
		/// <param name="e">包含事件数据的 <see cref="T:System.EventArgs"/>。</param>
		protected override void OnValueChanged(EventArgs e)
		{
			base.OnValueChanged(e);
			OnIntValueChanged();
		}
	}
}
