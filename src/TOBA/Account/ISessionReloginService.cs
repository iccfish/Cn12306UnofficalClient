using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Account
{
	using System.Threading.Tasks;
	using System.Windows.Forms;

	internal interface ISessionReloginService
	{
		/// <summary>
		/// 重试登录
		/// </summary>
		/// <returns></returns>
		Task<bool> ReloginAsync();
	}
}
