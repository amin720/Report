using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using PdfRpt.Core.Contracts;
using PdfRpt.Core.Helper;
using PdfRpt.FluentInterface;
using Report.Models;
using Report.Pdf.SampleReport.Headers_Footers.Header_1;
using Font = iTextSharp.text.Font;


namespace Report.Pdf.SampleReport.Headers_Footers
{
	public class Headers_Footers
	{
		readonly DBFirst _db = new DBFirst();
		readonly C1Header _customHeader = new C1Header();
		private readonly Font font1 = InstallFonts.GetFont("bnazanb");
		private readonly Font font2 = InstallFonts.GetFont("arial");
		public IPdfReportData CreatePdfReport()
		{
			return new PdfReport().DocumentPreferences(doc =>
				{
					doc.RunDirection(PdfRunDirection.LeftToRight);
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
					doc.Compression(new CompressionSettings
					{
						EnableCompression = true,
						EnableFullCompression = true
					});
				})
				.DefaultFonts(fonts =>
				{
					fonts.Path(System.IO.Path.Combine(AppPath.ApplicationPath, "Pdf\\fonts\\bnazanb.ttf"),
						System.IO.Path.Combine(AppPath.ApplicationPath, "Pdf\\fonts\\arial.ttf"));
					fonts.Size(10);
					fonts.Color(Color.Black);
				})
				.PagesFooter(footer =>
				{
					footer.DefaultFooter(PersianDate.ToPersianDateTime(DateTime.Now, "/", false));
				})
				.PagesHeader(header =>
				{
					header.CacheHeader(cache: true); // It's a default setting to improve the performance.
					header.CustomHeader(_customHeader);
				})
				.MainTableTemplate(template =>
				{
					template.BasicTemplate(BasicTemplate.SilverTemplate);
				})
				.MainTablePreferences(table =>
				{
					table.ColumnsWidthsType(TableColumnWidthType.Relative);
					table.MultipleColumnsPerPage(new MultipleColumnsPerPage
					{
						ColumnsGap = 22,
						ColumnsPerPage = 10,
						ColumnsWidth = 250,
						IsRightToLeft = false,
						TopMargin = 7
					});
				})
				.MainTableDataSource(dataSource =>
				{
					var employess = _db.Employees.OrderBy(e => e.FullName).ThenBy(e => e.Age).ToList();

					dataSource.StronglyTypedList(employess);
				})
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
						column.HeaderCell("#");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(x => x.FullName);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(1);
						column.Width(3);
						column.HeaderCell("نام");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(x => x.Age); // nested property support
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(2);
						column.Width(1);
						column.HeaderCell("سن");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(x => x.Gender.Name);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(3);
						column.Width(2);
						column.HeaderCell("جنسیت");
					});
				})
				.MainTableEvents(events =>
				{
					events.DataSourceIsEmpty(message: "There is no data available to display.");
				})
				.Export(export =>
				{
					export.ToExcel();
				})
				.Generate(data => data.AsPdfFile(string.Format("{0}\\Pdf\\CustomHeaderFooterPdfReportSample-{1}.pdf", AppPath.ApplicationPath, Guid.NewGuid().ToString("N"))));
		}
	}
}