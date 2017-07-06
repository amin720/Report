using System.Windows.Forms;
using System.Web;

namespace Report.Pdf
{
	public static class AppPath
	{
		// مسیر آدرس دهی برای اپلیکشن تحت ویندوز و وب مشخص میشود

		public static string ApplicationPath
		{
			get
			{
				if (isInWeb)
					return HttpRuntime.AppDomainAppPath;
				//آدرس دهی در برنامه تحت ویندوز
				return Application.StartupPath;
				//آدرس دهی در برنامه تحت وب
				//return Server.MapPath;
			}
		}

		private static bool isInWeb
		{
			get
			{
				return HttpContext.Current != null;
			}
		}

	}
}