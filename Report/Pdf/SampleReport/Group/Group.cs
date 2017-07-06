using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PdfRpt.Core.Contracts;
using PdfRpt.FluentInterface;
using iTextSharp.text;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using PdfRpt.Core.Helper;
using PdfRpt.Core.Helper.HtmlToPdf;
using Report.Models;


namespace Report.Pdf.SampleReport.Group
{
	public class Group
	{
		private readonly Font font1 = InstallFonts.GetFont("bnazanb");
		private readonly Font font2 = InstallFonts.GetFont("arial");

		private readonly DBFirst _db = new DBFirst();

		public IPdfReportData CreatePdfReport()
		{
			 return new PdfReport().DocumentPreferences(doc =>
				{
					doc.RunDirection(PdfRunDirection.RightToLeft);
					doc.Orientation(PageOrientation.Landscape);
					doc.PageSize(PdfPageSize.A4);
					doc.DocumentMetadata(new DocumentMetadata
					{
						Author = "محمد امین زینالی",
						Application = "Report",
						Keywords = "گزارش گیری کارمندان",
						Subject = "گروه بندی",
						Title = "گزارش گیری کارمندان"

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
				})
				.PagesFooter(footer =>
				{
					footer.DefaultFooter(DateTime.Now.ToPersianDateTime("/", false));
				})
				.PagesHeader(header =>
				{
					header.CacheHeader(cache: true); // It's a default setting to improve the performance.
					header.CustomHeader(new GroupingHeaders { PdfRptFont = header.PdfFont });
				})
				.MainTableTemplate(template =>
				{
					template.BasicTemplate(BasicTemplate.SnowyPineTemplate);
				})
				.MainTablePreferences(table =>
				{
					table.ColumnsWidthsType(TableColumnWidthType.Relative);
					table.GroupsPreferences(new GroupsPreferences
					{
						// آیا باید ستون‌های دخیل در گروه بندی، در گزارش نمایش داده شوند یا خیر
						GroupType = GroupType.HideGroupingColumns,
						// آیا سر ستون هر جدول، به ازای هر گروه باید تکرار شود؟ 
						RepeatHeaderRowPerGroup = true,
						//  آیا در هر صفحه یک گروه نمایش داده شود یا اینکه گروه‌ها به صورت متوالی در صفحات درج شوند
						ShowOneGroupPerPage = false,
						// فاصله جمع نهایی تمام گروه‌ها از آخرین گروه نمایش داده شده 
						SpacingBeforeAllGroupsSummary = 5f,
						// چه فاصله‌ای از انتهای صفحه، گروه جدیدی نباید درج شود و این گروه باید به صفحه بعدی منتقل شده و از آنجا شروع شود
						NewGroupAvailableSpacingThreshold = 150,
						SpacingAfterAllGroupsSummary = 5f
					});
					table.SpacingAfter(4f);
				})
				.MainTableDataSource(dataSource =>
				{
					var employees = _db.Employees.OrderBy(g => g.Gender.Name).ToList();

					dataSource.StronglyTypedList(employees);
				})
				.MainTableSummarySettings(summarySettings =>
				{
					summarySettings.PreviousPageSummarySettings("تعداد.");
					summarySettings.OverallSummarySettings("مجموع");
					summarySettings.AllGroupsSummarySettings("مجموع تمام گروه ها");
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
						column.PropertyName<Employee>(x => x.Gender.Name);
						column.CellsHorizontalAlignment(HorizontalAlignment.Right);
						column.Order(1);
						column.Width(2);
						column.HeaderCell("جنسیت");
						column.Group(
							(val1, val2) =>
							{
								return val1.ToString() == val2.ToString();
							});
					});
					

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(x => x.Department.Name);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(2);
						column.Width(3);
						column.HeaderCell("دپارتمان");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(x => x.Age);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(3);
						column.Width(1);
						column.HeaderCell("سن");
						
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(x => x.Id);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(4);
						column.Width(1);
						column.HeaderCell("شماره کارمندی");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(x => x.FullName);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(5);
						column.Width(3);
						column.HeaderCell("نام");
					});
					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(x => x.Gender.Name);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.Order(6);
						column.Width(2);
						column.IsVisible(true);
						column.HeaderCell("جنسیت");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(x => x.Salary);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(7);
						column.Width(3);
						column.HeaderCell("حقوق");
						column.ColumnItemsTemplate(template =>
						{
							template.TextBlock();
							template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
								? string.Empty : $"{obj:n0}");
						});
						column.AggregateFunction(aggregateFunction =>
						{
							aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
							aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
								? string.Empty : $"{obj:n0}");
						});
					});
				})
				.MainTableEvents(events =>
				{
					events.DataSourceIsEmpty(message: "There is no data available to display.");
					events.GroupAdded(args =>
					{
						//args.PdfDoc.Add(new Phrase("\nGroup added event."));

						/*var data = args.ColumnCellsSummaryData
							.Where(data => data.CellData.PropertyName.Equals("propertyName")
								 && data.GroupNumber == 1);*/

						var salary = args.LastOverallAggregateValueOf<Employee>(x => x.Salary);
						var table = new PdfGrid(1)
						{
							RunDirection = (int)PdfRunDirection.RightToLeft,
							WidthPercentage = args.PageSetup.MainTablePreferences.WidthPercentage
						};
						var htmlCell = new XmlWorkerHelper
						{
							// the registered fonts (DefaultFonts section) should be specified here
							Html = $@"<br/><span style='font-size:9pt;font-family:bnazanb;'>
                                                    <b>Group <i>added</i> event.</b>
                                                    مجموع حقوق: {salary}</span>",
							RunDirection = PdfRunDirection.RightToLeft,
							CssFilesPath = null, // optional
							ImagesPath = null, // optional
							InlineCss = null, // optional
							DefaultFont = args.PdfFont.Fonts[0] // b nazanin
						}.RenderHtml();
						htmlCell.Border = 0;
						table.AddCell(htmlCell);
						table.SpacingBefore = args.PageSetup.MainTablePreferences.SpacingBefore;

						args.PdfDoc.Add(table);
					});
				})
				.Export(export =>
				{
					//export.ToExcel();
				})
				.Generate(data => data.AsPdfFile(
					$"{AppPath.ApplicationPath}\\Pdf\\RptGroupingSample-{Guid.NewGuid():N}.pdf"));
		}
	}
}