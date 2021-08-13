using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Passenger
{
	using Data;

	using Entity.Web;

	internal partial class AddPassenger : FormBase
	{
		/// <summary>
		/// 获得正在编辑的联系人
		/// </summary>
		public Passenger Passenger { get; private set; }

		public AddPassenger()
			: this(null)
		{
		}

		public AddPassenger(Passenger passenger)
		{
			Passenger = passenger;
			InitializeComponent();

			this.Load += AddPassenger_Load;
		}

		void AddPassenger_Load(object sender, EventArgs e)
		{
			if (Passenger == null)
			{
				Passenger = new Passenger();
			}

			txtName.Text = Passenger.Name;
			txtId.Text = Passenger.IdNo;
			txtMobile.Text = Passenger.MobileNo;

			//类型
			cbType.DataSource = ParamData.PassengerType.Where(s => s.Key != 3).ToArray();
			cbType.ValueMember = "Key";
			cbType.DisplayMember = "Value";
			cbType.SelectedValue = Passenger.Type;

			//联系人类型
			cbIdType.DataSource = ParamData.PassengerIdType.ToArray();
			cbIdType.ValueMember = "Key";
			cbIdType.DisplayMember = "Value";
			cbIdType.SelectedValue = Passenger.IdTypeCode;

			//国家
			cbCountry.DataSource = ParamData.CountryMap.ToArray();
			cbCountry.ValueMember = "Key";
			cbCountry.DisplayMember = "Value";
			cbCountry.SelectedValue = Passenger.CountryCode.DefaultForEmpty("CN");

			//日期
			//dtBorn.MinDate = ;
			//if (Passenger.BornDate != null)
			//	dtBorn.Value = DateTime.Parse(Passenger.BornDate);

			//性别
			cbSex.DataSource = ParamData.SexList;
			cbSex.ValueMember = "Code";
			cbSex.DisplayMember = "Name";
			cbSex.SelectedValue = Passenger.Sex.DefaultForEmpty("M");

			txtId.TextChanged += (s, ee) =>
			{
				if ((char)cbIdType.SelectedValue != '1' || txtId.Text.Length < 17)
					return;
				var m = txtId.Text.RegMatch(@"\d{6}(\d{4})(\d{2})(\d{2})\d{2}(\d)");
				if (m == null)
					return;
				//var dt = (m[1] + "-" + m[2] + "-" + m[3]).ToDateTimeNullable();
				//if (dt != null && dt.Value.Year > 1900)
				//{
				//	dtBorn.Value = dt.Value;
				//}
				cbSex.SelectedValue = m[4].ToInt32() % 2 == 1 ? "M" : "F";
			};
		}

		public bool SaveToMyList
		{
			get
			{
				//return ckAddToPassenger.Checked;
				return false;
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (txtName.Text.IsNullOrEmpty())
			{
				Information("请输入姓名");
			}
			else if (txtId.Text.IsNullOrEmpty())
			{
				Information("请输入证件号码");
			}
			else if ((int)cbType.SelectedValue == 3)
			{
				Information("暂时不支持学生联系人");
			}
			else
			{
				Passenger.Name = txtName.Text;
				Passenger.IdNo = txtId.Text;
				Passenger.MobileNo = txtMobile.Text;
				Passenger.Type = (int)cbType.SelectedValue;
				Passenger.IdTypeCode = (char)cbIdType.SelectedValue;
				Passenger.CountryCode = (string)cbCountry.SelectedValue;
				Passenger.Sex = (string)cbSex.SelectedValue;
				//Passenger.BornDate = dtBorn.Value.ToString("yyyy-MM-dd");

				DialogResult = DialogResult.OK;
				Close();
			}
		}
	}
}
