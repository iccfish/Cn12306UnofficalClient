namespace TOBA.UI.Controls.Common
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;
	using System.Windows.Forms.VisualStyles;

	public class DataGridViewButtonExtendCell : DataGridViewButtonCell
	{
		/// <summary>
		/// 创建 <see cref="DataGridViewButtonExtendCell" />  的新实例(DataGridViewButtonExtendCell)
		/// </summary>
		public DataGridViewButtonExtendCell()
		{
			ButtonVisible = true;
			Enabled = true;
		}

		/// <summary>
		/// 获得或设置是否可见
		/// </summary>
		public bool ButtonVisible { get; set; }

		/// <summary>
		/// 获得或设置是否可用
		/// </summary>
		public bool Enabled { get; set; }

		#region Overrides of DataGridViewCell

		/// <summary>
		/// 在单击单元格时进行调用。
		/// </summary>
		/// <param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/>。</param>
		protected override void OnClick(DataGridViewCellEventArgs e)
		{
			if (!ButtonVisible || !Enabled) return;

			base.OnClick(e);
		}

		#region Overrides of DataGridViewCell

		/// <summary>
		/// 在指针位于单元格上且用户同时单击鼠标按钮时进行调用。
		/// </summary>
		/// <param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/>。</param>
		protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
		{
			if (!ButtonVisible || !Enabled) return;

			base.OnMouseClick(e);
		}

		#endregion

		#endregion

		#region Overrides of DataGridViewButtonCell

		/// <summary>
		/// 创建此单元格的精确副本。
		/// </summary>
		/// <returns>
		/// <see cref="T:System.Object"/>，它表示克隆的 <see cref="T:System.Windows.Forms.DataGridViewButtonCell"/>。
		/// </returns>
		/// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		public override object Clone()
		{
			var obj = (DataGridViewButtonExtendCell)base.Clone();
			obj.Enabled = Enabled;
			obj.ButtonVisible = ButtonVisible;

			return obj;
		}

		#region Overrides of DataGridViewButtonCell

		/// <summary>
		/// 绘制当前的 <see cref="T:System.Windows.Forms.DataGridViewButtonCell"/>。
		/// </summary>
		/// <param name="graphics">用于绘制单元格的 <see cref="T:System.Drawing.Graphics"/>。</param><param name="clipBounds"><see cref="T:System.Drawing.Rectangle"/>，它表示需要重新绘制的 <see cref="T:System.Windows.Forms.DataGridView"/> 区域。</param><param name="cellBounds">一个包含所绘制的单元格边界的 <see cref="T:System.Drawing.Rectangle"/>。</param><param name="rowIndex">当前所绘制的单元格的行索引。</param><param name="elementState"><see cref="T:System.Windows.Forms.DataGridViewElementStates"/> 值的按位组合，用于指定单元格的状态。</param><param name="value">当前所绘制的单元格的数据。</param><param name="formattedValue">当前所绘制的单元格的格式化数据。</param><param name="errorText">与单元格关联的错误消息。</param><param name="cellStyle"><see cref="T:System.Windows.Forms.DataGridViewCellStyle"/>，它包含有关单元格的格式设置和样式的信息。</param><param name="advancedBorderStyle"><see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/>，它包含当前所绘制的单元格的边框样式。</param><param name="paintParts"><see cref="T:System.Windows.Forms.DataGridViewPaintParts"/> 值的按位组合，用于指定需要绘制的单元格部分。</param>
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (ButtonVisible && Enabled)
			{
				base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
				return;
			}

			if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
			{
				using (var cellBackground = new SolidBrush((elementState & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected ? cellStyle.SelectionBackColor : cellStyle.BackColor))
				{
					graphics.FillRectangle(cellBackground, cellBounds);
				}
			}
			// Draw the cell borders, if specified.
			if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
			{
				PaintBorder(graphics, clipBounds, cellBounds, cellStyle,
					advancedBorderStyle);
			}

			if (ButtonVisible)
			{
				// Calculate the area in which to draw the button.
				var buttonArea = cellBounds;
				var buttonAdjustment = BorderWidths(advancedBorderStyle);
				buttonArea.X += buttonAdjustment.X;
				buttonArea.Y += buttonAdjustment.Y;
				buttonArea.Height -= buttonAdjustment.Height;
				buttonArea.Width -= buttonAdjustment.Width;

				// Draw the disabled button.               
				if (!Enabled)
				{
					ButtonRenderer.DrawButton(graphics, buttonArea, PushButtonState.Disabled);

					// Draw the disabled button text.
					if (this.FormattedValue is String)
					{
						TextRenderer.DrawText(graphics, (string)this.FormattedValue, this.DataGridView.Font, buttonArea, SystemColors.GrayText);
					}
				}
				else
				{
					if (elementState == DataGridViewElementStates.Selected)
					{
						ButtonRenderer.DrawButton(graphics, buttonArea, PushButtonState.Hot);
					}
					else
					{
						ButtonRenderer.DrawButton(graphics, buttonArea, PushButtonState.Disabled);
					}

					// Draw the disabled button text.
					if (this.FormattedValue is String)
					{
						TextRenderer.DrawText(graphics, (string)this.FormattedValue, this.DataGridView.Font, buttonArea, DataGridView.ForeColor);
					}
				}
			}
		}

		#endregion

		#endregion
	}
}
