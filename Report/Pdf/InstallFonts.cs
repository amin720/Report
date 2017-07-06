using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Report.Pdf
{
	public class InstallFonts
	{
		public static iTextSharp.text.Font GetFont(string fontName)
		{
			if (!FontFactory.IsRegistered(fontName))
			{
				var fontPath = AppPath.ApplicationPath + "Pdf\\fonts\\" + fontName + ".ttf";
				FontFactory.Register(fontPath);
			}

			return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

		}
	}
}