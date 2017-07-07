using System.Windows.Forms;
using System.Web;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfRpt;
using PdfRpt.Core.Contracts;
using static Report.Pdf.Pdf2Image;
using Font = iTextSharp.text.Font;

namespace Report.Pdf
{
	#region Fonts

	public enum PersianFont
	{
		IranianSans,
		BNazanin,
		ADanesh

	}

	public enum EnglishFont
	{
		Calibri,
		Arial,
		Tahoma

	}

	#endregion

	public class PdfReports
	{
		private Font _pFont;
		private Font _eFont;
		public PdfReports() : this(PersianFont.BNazanin, EnglishFont.Calibri)
		{

		}
		public PdfReports(PersianFont persianFont, EnglishFont englishFont)
		{
			PFont = persianFont;
			EFont = englishFont;
		}

		public PersianFont PFont { get; set; }

		public EnglishFont EFont { get; set; }

		#region InstallFonts

		public static iTextSharp.text.Font GetFont(PersianFont persianFont)
		{
			if (!FontFactory.IsRegistered(persianFont.ToString()))
			{
				var fontPath = AppPath.ApplicationPath + "Pdf\\fonts\\" + persianFont.ToString() + ".ttf";
				FontFactory.Register(fontPath);
			}

			return FontFactory.GetFont(persianFont.ToString(), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
		}

		public static Font GetFont(EnglishFont englishFont)
		{
			if (!FontFactory.IsRegistered(englishFont.ToString()))
			{
				var fontPath = AppPath.ApplicationPath + "Pdf\\fonts\\" + englishFont.ToString() + ".ttf";
				FontFactory.Register(fontPath);
			}

			return FontFactory.GetFont(englishFont.ToString(), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
		}

		#endregion

		#region AppPath

		// مسیر آدرس دهی برای اپلیکشن تحت ویندوز و وب مشخص میشود

		public static string ApplicationPath
		{
			get
			{
				if (IsInWeb)
					return HttpRuntime.AppDomainAppPath;
				//آدرس دهی در برنامه تحت ویندوز
				return Application.StartupPath;
				//آدرس دهی در برنامه تحت وب
				//return Server.MapPath;
			}
		}

		private static bool IsInWeb
		{
			get
			{
				return HttpContext.Current != null;
			}
		}

		#endregion

		#region Watermark

		public IPdfFont GetWatermarkFont()
		{
			_pFont = GetFont(PFont);
			_eFont = GetFont(EFont);

			var watermarkFont = new GenericFontProvider(
				System.IO.Path.Combine(AppPath.ApplicationPath, "Pdf\\fonts\\" + _pFont.ToString() + ".ttf"),
				System.IO.Path.Combine(AppPath.ApplicationPath, "Pdf\\fonts\\" + _eFont.ToString() + ".ttf"))
			{
				Color = BaseColor.LIGHT_GRAY,
				Size = 50
			};


			return watermarkFont;
		}

		#endregion

		#region Pdf2Image

		public void PdfToImage(string pdfPath)
		{
			Pdf2Image.AllPdfPagesToPng((pageImageData, pageNumber, numPages) =>
			{
				System.IO.File.WriteAllBytes($"{Application.StartupPath}\\page-{pageNumber}.png", pageImageData);
			}, pdfFilePath: pdfPath);
		}
		public void PdfToImage(string pdfPath, int pageNumber = 0)
		{
			var pageImageData = Pdf2Image.PdfPageToPng(pdfFilePath: pdfPath, pageNumber: pageNumber);
			System.IO.File.WriteAllBytes($"{Application.StartupPath}\\page-{pageNumber}.png", pageImageData);
		}

		#endregion

	}


}