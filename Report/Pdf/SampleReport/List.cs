using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Drawing;
// ارجاعات
using PdfRpt.Core.Contracts;
using PdfRpt.FluentInterface;
using PdfRpt.Core.Helper;
using Report.Models;
using Font = iTextSharp.text.Font;


namespace Report.Pdf.Sample
{
	public class List
	{
		// استفاده از پایگاه داده
		private readonly DBFirst _db = new DBFirst();

		// نصب فونت هایی که وجو ندارد؛ کافی است فقط اسم فونتی که مد نظر دارید تا نصب شود ذکر شود
		// در ضمن بایستی در فولدر مشخص شده گذاشته شود
		private readonly Font font1 = InstallFonts.GetFont("bnazanb");
		private readonly Font font2 = InstallFonts.GetFont("arial");

		public IPdfReportData CreatePdfReport()
		{
			return new PdfReport()
				// اطلاعات پایه ای از سند را تکمیل میشود
				.DocumentPreferences(doc =>
				{
					doc.RunDirection(PdfRunDirection.RightToLeft);
					doc.Orientation(PageOrientation.Portrait);
					doc.PageSize(PdfPageSize.A4);
					doc.DocumentMetadata(new DocumentMetadata
					{
						Author = "محمد امین زینالی",
						Application = "Report",
						Keywords = "گزارش گیری ساده",
						Subject = "گزارش",
						Title = "گزارش"

					});
					// عمل فشرده سازی مشخص شود؟
					doc.Compression(new CompressionSettings
					{
						EnableCompression = true,
						EnableFullCompression = true
					});
					// بعد از نمایش سند بلافاصله اماده پرینت شود 
					doc.PrintingPreferences(new PrintingPreferences
					{
						ShowPrintDialogAutomatically = false
					});
				})
				// قلم های مورد استفاده وارد میشود
				.DefaultFonts(fonts =>
				{
					fonts.Path(System.IO.Path.Combine(AppPath.ApplicationPath, "Pdf\\fonts\\bnazanb.ttf"),
						System.IO.Path.Combine(AppPath.ApplicationPath, "Pdf\\fonts\\arial.ttf"));
					fonts.Size(10);
					fonts.Color(Color.Black);
				})
				// نتظیمات قسمت پایین صفحه 
				.PagesFooter(footer =>
				{
					// تاریخ شمسی
					footer.DefaultFooter(PersianDate.ToPersianDateTime(DateTime.Now,"/",false));
				})
				// نتظیمات قسمت بالا صفحه
				.PagesHeader(header =>
				{
					// به صورت پیش فرض قرار بگیرد در اجرا کر بسیار موثر عمل میکند
					header.CacheHeader(cache: true);
					header.DefaultHeader(defaultHeader =>
					{
						defaultHeader.ImagePath(AppPath.ApplicationPath + "Pdf\\Images\\01.png");
						defaultHeader.RunDirection(PdfRunDirection.RightToLeft);
						defaultHeader.Message("این یک سند تست می باشد");
					});
				})
				// قالب ظاهری به صورت پیش فرض یکسری قالب هایی در نظر گرفته شده اس
				// امکان تعریف قالب سفارشی وجود دارد
				.MainTableTemplate(template =>
				{
					template.BasicTemplate(BasicTemplate.BlackAndBlue1Template);
				})
				// تنظیمات جدول اصلی را انجام میشود
				.MainTablePreferences(table =>
				{
					// تعیین عرض هر ستون
					table.ColumnsWidthsType(TableColumnWidthType.Relative);
					// تعداد ردیف در هر صفحه
					table.NumberOfDataRowsPerPage(10);
				})
				// منبع داده مورد نظر را وارد میکنیم
				// میتوانیم مبع سفارشی نیز وارد کنیم
				.MainTableDataSource(dataSource =>
				{
					var employess = _db.Employees.OrderBy(p => p.FullName).Include(g => g.Gender).ToList();

					dataSource.StronglyTypedList(employess);
				})
				// بر چسب های پایین صفحات مشخص میکنند
				.MainTableSummarySettings(summerySettings =>
				{
					summerySettings.OverallSummarySettings("خلاصه");
					summerySettings.PreviousPageSummarySettings("خلاصه صفحه قبلی");
					summerySettings.PageSummarySettings("خلاصه صفحه");
				})
				// ستون هایی که مد نظر داریم تا وارد کنیم لیست میکنیم
				.MainTableColumns(columns =>
				{
					columns.AddColumn(column =>
					{
						column.PropertyName("rowNo");
						column.IsRowNumber(true);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(0);
						column.Width(1);
						column.HeaderCell("ردیف");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(p => p.Id);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(1);
						column.Width(1);
						column.HeaderCell("شماره شخصی");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(p => p.FirstName);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(2);
						column.Width(2);
						column.HeaderCell("نام", horizontalAlignment: HorizontalAlignment.Right);
						column.Font(font =>
						{
							font.Size(11);
							font.Color(Color.BlueViolet);
						});
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(p => p.LastName);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(3);
						column.Width(2);
						column.HeaderCell("نام خانوادگی");
						column.Padding(5);
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(p => p.Age);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(4);
						column.Width(1);
						column.HeaderCell("سن");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(p => p.Gender.Name);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(5);
						column.Width(2);
						column.HeaderCell("جنسیت");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(p => p.Department.Name);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(5);
						column.Width(2);
						column.HeaderCell("دپارتمان");
					});
				})
				// رخداد هایی که مد نظر داریم را وارد میکنیم
				.MainTableEvents(events =>
				{
					events.DataSourceIsEmpty(message: "هیچ داده ای برای نمایش وجود ندارد");
				})
				// خروجی های فایل به غیر از پی دی اف 
				// امکان سافرش سازی وجو دارد
				.Export(export =>
				{
					export.ToCsv();
					export.ToExcel();
					export.ToXml();
				})
				// فایل گزارشی کجا ذخیره شود
				.Generate(data => data.AsPdfFile(string.Format("{0}\\Pdf\\Sample-{1}.pdf", AppPath.ApplicationPath, Guid.NewGuid().ToString("N"))));
		}
	}
}