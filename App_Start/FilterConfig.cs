using System.Web;
using System.Web.Mvc;

namespace Cloud22010446_Dut4life
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
