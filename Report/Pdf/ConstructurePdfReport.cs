using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Windows.Forms;
using System.Web;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using PdfRpt;
using PdfRpt.Core.Contracts;
using PdfRpt.Core.Helper;
using Report.Pdf.SampleReport;
using Report.Pdf.SampleReport.Grouping;
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

	public class ConstructurePdfReport
	{
		private Font _pFont;
		private Font _eFont;

		public ConstructurePdfReport() : this(PersianFont.BNazanin, EnglishFont.Calibri)
		{

		}

		public ConstructurePdfReport(PersianFont persianFont, EnglishFont englishFont)
		{
			PFont = persianFont;
			EFont = englishFont;
		}

		#region Properties

		public PersianFont PFont { get; set; }
		public EnglishFont EFont { get; set; }
		public string Author { get; set; }
		public string Application { get; set; }
		public string Keywords { get; set; }
		public string Subject { get; set; }
		public string Title { get; set; }
		public PdfPageSize PageSize { get; set; }
		public PageOrientation Orientation { get; set; }

		#endregion


		#region InstallFonts

		public iTextSharp.text.Font GetFont(PersianFont persianFont)
		{
			if (!FontFactory.IsRegistered(persianFont.ToString()))
			{
				var fontPath = AppPath.ApplicationPath + "Pdf\\fonts\\" + persianFont.ToString() + ".ttf";
				FontFactory.Register(fontPath);
			}

			return FontFactory.GetFont(persianFont.ToString(), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
		}

		public iTextSharp.text.Font GetFont(EnglishFont englishFont)
		{
			if (!FontFactory.IsRegistered(englishFont.ToString()))
			{
				var fontPath = AppPath.ApplicationPath + "Pdf\\fonts\\" + englishFont.ToString() + ".ttf";
				FontFactory.Register(fontPath);
			}

			return FontFactory.GetFont(englishFont.ToString(), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
		}

		#endregion

		#region Watermark

		public IPdfFont GetWatermarkFont()
		{
			_pFont = GetFont(PFont);
			_eFont = GetFont(EFont);

			var watermarkFont = new GenericFontProvider(
				System.IO.Path.Combine(AppPath.ApplicationPath, "Pdf\\fonts\\" + _pFont.Familyname + ".ttf"),
				System.IO.Path.Combine(AppPath.ApplicationPath, "Pdf\\fonts\\" + _eFont.Familyname + ".ttf"))
			{
				Color = BaseColor.LIGHT_GRAY,
				Size = 50
			};


			return watermarkFont;
		}

		#endregion

		#region Pdf2Image

		// TODO: AppPath repair

		//public void PdfToImage(string pdfPath)
		//{
		//	Pdf2Image.AllPdfPagesToPng((pageImageData, pageNumber, numPages) =>
		//	{
		//		System.IO.File.WriteAllBytes($"{AppPath.Application}\\page-{pageNumber}.png", pageImageData);
		//	}, pdfFilePath: pdfPath);
		//}
		//public void PdfToImage(string pdfPath, int pageNumber = 0)
		//{
		//	var pageImageData = Pdf2Image.PdfPageToPng(pdfFilePath: pdfPath, pageNumber: pageNumber);
		//	System.IO.File.WriteAllBytes($"{AppPath.Application}\\page-{pageNumber}.png", pageImageData);
		//}

		#endregion

		#region Accounting

		/// <summary>
		/// برای استفاده از کافی است خروجی را داخل 
		/// Redirect
		/// قرار دهید
		/// </summary>
		/// <param name="model">Callbacked Database</param>
		/// <param name="sqlQuery"></param>
		/// <param name="tempraryStatus">نمایش یا عدم نمایش شماره موقت</param>
		/// <returns>یک رشته که مسیر فایل بعد از خروجی است مشخص میکند</returns>
		public string AccountingReport(DbContext model = null, string sqlQuery = null, bool tempraryStatus = false)
		{
			var rpt = new InlineProvide().CreatePdfReport(model, sqlQuery, tempraryStatus);
			var outputFilePath = rpt.FileName.Replace(HttpRuntime.AppDomainAppPath, string.Empty);
			return outputFilePath;
		}

		/// <summary>
		/// برای استفاده از کافی است خروجی را داخل 
		/// Redirect
		/// قرار دهید
		/// </summary>
		/// <param name="model">Callbacked Database</param>
		/// <param name="sqlQuery"></param>
		/// <param name="tempraryStatus">نمایش یا عدم نمایش شماره موقت</param>
		/// <returns>یک رشته که مسیر فایل بعد از خروجی است مشخص میکند</returns>
		public string AccountingReportGrouping(DbContext model = null, string sqlQuery = null, bool tempraryStatus = false)
		{
			var rpt = new AccGroup().CreatePdfReport(model, sqlQuery, tempraryStatus);
			var outputFilePath = rpt.FileName.Replace(HttpRuntime.AppDomainAppPath, string.Empty);
			return outputFilePath;
		}

		#endregion

		#region Printing&MergingPdfs

		public void Merge_Printing(List<string> inFiles, string outFile)
		{

			using (FileStream stream = new FileStream(outFile, FileMode.Create))
			using (Document doc = new Document())
			using (PdfCopy pdf = new PdfCopy(doc, stream))
			{
				doc.Open();

				PdfReader reader = null;
				PdfImportedPage page = null;

				//fixed typo
				inFiles.ForEach(file =>
				{
					reader = new PdfReader(file);

					for (int i = 0; i < reader.NumberOfPages; i++)
					{
						page = pdf.GetImportedPage(reader, i + 1);
						pdf.AddPage(page);
					}

					pdf.FreeReader(reader);
					reader.Close();
					File.Delete(file);
				});
			}
			var print = new AcroPrint()
			{
				PdfFilePath = outFile,
				PrinterName = "Adobe PDF"
			};

			print.PrintPdfFile();
		}

		#endregion
	}
}
