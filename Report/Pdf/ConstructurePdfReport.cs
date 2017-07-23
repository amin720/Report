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
using Report.Pdf.SampleReport.Balance;
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

	#region TaxBalance

	public enum TypeBalance
	{
		/// <summary>
		/// تراز کل
		/// </summary>
		Total,
		/// <summary>
		/// تراز معین
		/// </summary>
		Certain,
		/// <summary>
		/// تراز تفصیلی
		/// </summary>
		Detailed
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
		/// <summary>
		/// اندازه کاغذ را مشخص کنید
		/// </summary>
		public PdfPageSize PageSize { get; set; }
		/// <summary>
		/// عمودی یا افقی بودن سند را مشخص کنید
		/// </summary>
		public PageOrientation Orientation { get; set; }
		/// <summary>
		/// شخص تایید کننده
		/// </summary>
		public string Confirmatory { get; set; }
		/// <summary>
		/// شخص تنظیم کننده
		/// </summary>
		public string Regulator { get; set; }
		/// <summary>
		/// مدیر مالی
		/// </summary>
		public string FinancialMaanager { get; set; }
		/// <summary>
		/// نوع سند را مشخص کنید برای مثال سند حسابداری
		/// </summary>
		public string DocumentName { get; set; }
		/// <summary>
		/// نام شرکت مرتبط را وارد کنید
		/// </summary>
		public string CompanyName { get; set; }
		/// <summary>
		/// نوع تراز را وارد کنید
		/// </summary>
		public TypeBalance TypeBalance { get; set; }

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
		/// <summary>
		/// قلم برای نوشته پس زمینه صفحه
		/// </summary>
		/// <returns>یک قلم استاندارد</returns>
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
		/// <param name="report"></param>
		/// <param name="tempraryStatus">نمایش یا عدم نمایش شماره موقت</param>
		/// <param name="sqlQuery"></param>
		/// <param name="model">Callbacked Database</param>
		/// <returns>یک رشته که مسیر فایل بعد از خروجی است مشخص میکند</returns>
		public string AccountingReport(ConstructurePdfReport report, bool tempraryStatus = false, string sqlQuery = null, DbContext model = null)
		{
			var rpt = new InlineProvide().CreatePdfReport(report, model, sqlQuery, tempraryStatus);
			var outputFilePath = rpt.FileName.Replace(HttpRuntime.AppDomainAppPath, string.Empty);
			return outputFilePath;
		}

		/// <summary>
		/// برای استفاده از کافی است خروجی را داخل 
		/// Redirect
		/// قرار دهید
		/// </summary>
		/// <param name="report"></param>
		/// <param name="model">Callbacked Database</param>
		/// <param name="sqlQuery"></param>
		/// <param name="tempraryStatus">نمایش یا عدم نمایش شماره موقت</param>
		/// <returns>یک رشته که مسیر فایل بعد از خروجی است مشخص میکند</returns>
		public string AccountingReportGrouping(ConstructurePdfReport report, DbContext model = null, string sqlQuery = null, bool tempraryStatus = false)
		{
			var rpt = new AccGroup().CreatePdfReport(report, model, sqlQuery, tempraryStatus);
			var outputFilePath = rpt.FileName.Replace(HttpRuntime.AppDomainAppPath, string.Empty);
			return outputFilePath;
		}

		/// <summary>
		/// برای استفاده از کافی است خروجی را داخل 
		/// Redirect
		/// قرار دهید
		/// </summary>
		/// <param name="report"></param>
		/// <param name="model">Callbacked Database</param>
		/// <param name="sqlQuery"></param>
		/// <param name="tempraryStatus">نمایش یا عدم نمایش شماره موقت</param>
		/// <returns>یک رشته که مسیر فایل بعد از خروجی است مشخص میکند</returns>
		public string AccountingReportTaxBalance(ConstructurePdfReport report, DbContext model = null, string sqlQuery = null, bool tempraryStatus = false)
		{
			var rpt = new TaxBalance().CreatePdfReport(report, model, sqlQuery, tempraryStatus);
			var outputFilePath = rpt.FileName.Replace(HttpRuntime.AppDomainAppPath, string.Empty);
			return outputFilePath;
		}


		#endregion

		#region Printing&MergingPdfs
		/// <summary>
		/// این متد تمامی فایل پی دی اف که مورد نظر از در یک فایل قرار داده و بالافاصله اقدام به چاپ میکند
		/// </summary>
		/// <param name="inFiles">فایل های مرتبط را انتخاب کنید</param>
		/// <param name="outFile">مسیر خروجی فایل را مشخص کنید</param>
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
