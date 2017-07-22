using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfRpt.Core.Contracts;
using PdfRpt.Core.Helper;
using PdfRpt.FluentInterface;
using Report.Models;
using Reportig.Template;

namespace Report.Pdf.SampleReport.Balance
{
	public class DetailedBalance
	{
		private static Report.Pdf.ConstructurePdfReport _report = new Report.Pdf.ConstructurePdfReport();

		private static bool _temprary = false;

		// Install Fonts
		private readonly Font font1 = _report.GetFont(PersianFont.BNazanin);
		private readonly Font font2 = _report.GetFont(EnglishFont.Calibri);

		public IPdfReportData CreatePdfReport(ConstructurePdfReport report, DbContext modelDbContext = null, string sqlQuery = null, bool tempraryStatus = false)
		{
			_temprary = tempraryStatus;
			_report = report;
			//فعال سازی حالت InMemory
			if (HttpContext.Current == null)
				return null;

			var fileName = $"test-{Guid.NewGuid().ToString("N")}-InlineProvidersPdfReport.pdf";

			return new PdfReport().DocumentPreferences(doc =>
				{
					doc.RunDirection(PdfRunDirection.RightToLeft);
					doc.Orientation(_report.Orientation);
					doc.PageSize(_report.PageSize);
					doc.DocumentMetadata(new DocumentMetadata
					{
						Author = _report.Author,
						Application = _report.Application,
						Keywords = _report.Keywords,
						Subject = _report.Subject,
						Title = _report.Title
					});
					doc.Compression(new CompressionSettings
					{
						EnableCompression = true,
						EnableFullCompression = true
					});
					// Watermark
					doc.DiagonalWatermark(new DiagonalWatermark()
					{
						Text = "تست می باشد",
						RunDirection = PdfRunDirection.RightToLeft,
						Font = _report.GetWatermarkFont()
					});
					// فرمان مستقیم برای چاپ
					doc.PrintingPreferences(new PrintingPreferences()
					{
						ShowPrintDialogAutomatically = false,

					});
				})
				.DefaultFonts(fonts =>
				{
					fonts.Path(System.IO.Path.Combine(Reportig.AppPath.ApplicationPath, "Pdf\\fonts\\" + font1.Familyname + ".ttf"),
						System.IO.Path.Combine(Reportig.AppPath.ApplicationPath, "Pdf\\fonts\\" + font2.Familyname + ".ttf"));
					fonts.Size(9);
					fonts.Color(System.Drawing.Color.Black);
				})
				.PagesFooter(footer =>
				{
					var date = DateTime.Now.ToPersianDateTime(" / ", false);
					footer.InlineFooter(inlineFooter =>
					{
						inlineFooter.FooterProperties(new FooterBasicProperties
						{
							PdfFont = footer.PdfFont,
							HorizontalAlignment = HorizontalAlignment.Right,
							RunDirection = PdfRunDirection.RightToLeft,
							SpacingBeforeTable = 5,
							TotalPagesCountTemplateHeight = 9,
							TotalPagesCountTemplateWidth = 50
						});

						inlineFooter.AddPageFooter(data =>
						{
							return CreateFooter(footer, date, data);
						});
					});
				})
				.PagesHeader(header =>
				{
					header.CacheHeader(cache: true); // It's a default setting to improve the performance.
					//header.CustomHeader(new Report.Pdf.SampleReport.Custom.Header
					//{
					//	PdfRptFont = header.PdfFont, HeaderBuilder = header,Temprary = tempraryStatus,Report = report,Context = modelDbContext,SqlQuery = sqlQuery
					//});
					header.CacheHeader(cache: true); // It's a default setting to improve the performance.
					header.InlineHeader(inlineHeader =>
					{
						inlineHeader.AddPageHeader(data =>
						{
							var valDate = DateTime.Now.ToPersianDateTime(" / ", false);

							return CreateHeader(header, valDate, modelDbContext, sqlQuery);
						});
					});
				})
				.MainTableTemplate(template =>
				{
					// the template is custom
					template.CustomTemplate(new GrayTemplate());
					// The template is made
					//template.BasicTemplate(BasicTemplate.ClassicTemplate);
				})
				.MainTablePreferences(table =>
				{
					table.ColumnsWidthsType(TableColumnWidthType.Relative);
					table.NumberOfDataRowsPerPage(15);
				})
				.MainTableDataSource(dataSource =>
				{


					var db = modelDbContext.Database.SqlQuery<STP_GetDetailedAccountBalance4Columns_Result>(sql: sqlQuery).ToList();

					dataSource.StronglyTypedList(db);
				})
				.MainTableSummarySettings(summarySettings =>
				{
					summarySettings.OverallSummarySettings("مجموع کل");
					summarySettings.PreviousPageSummarySettings("مجموع صفحه قبل");
					summarySettings.PageSummarySettings("محموع صفحه");
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
						column.Width((float)0.666);
						column.HeaderCell("ردیف", captionRotation: 90);
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<STP_GetDetailedAccountBalance4Columns_Result>(x => x.Code);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(1);
						column.Width(1);
						column.HeaderCell("کد");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<STP_GetDetailedAccountBalance4Columns_Result>(x => x.Name);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(2);
						column.Width(3);
						column.HeaderCell("نام");
					});


					columns.AddColumn(column =>
					{
						column.PropertyName<STP_GetDetailedAccountBalance4Columns_Result>(x => x.TotalDebtor);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(3);
						column.Width(2);
						column.HeaderCell("گردش بستانکار");
						column.ColumnItemsTemplate(template =>
						{
							template.TextBlock();
							template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
								? "0" : $"{obj:n0}");
						});
						column.AggregateFunction(aggregateFunction =>
						{
							aggregateFunction.CustomAggregateFunction(new SumNull());
							aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
								? "0" : $"{obj:n0}");
						});
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<STP_GetDetailedAccountBalance4Columns_Result>(x => x.TotalCreditor);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(4);
						column.Width(2);
						column.HeaderCell("گردش بدهکار");
						column.ColumnItemsTemplate(template =>
						{
							template.TextBlock();
							template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
								? "0" : $"{obj:n0}");
						});
						column.AggregateFunction(aggregateFunction =>
						{
							aggregateFunction.CustomAggregateFunction(new SumNull());
							aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
								? "0" : $"{obj:n0}");
						});

					});

					columns.AddColumn(column =>
					{
						column.PropertyName<STP_GetDetailedAccountBalance4Columns_Result>(x => x.RemainDebtor);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(5);
						column.Width(2);
						column.HeaderCell("مانده بستانکار");
						column.ColumnItemsTemplate(template =>
						{
							template.TextBlock();
							template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
								? "0" : $"{obj:n0}");
						});
						column.AggregateFunction(aggregateFunction =>
						{
							aggregateFunction.CustomAggregateFunction(new SumNull());
							aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
								? "0" : $"{obj:n0}");
						});
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<STP_GetDetailedAccountBalance4Columns_Result>(x => x.RemainCreditor);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(6);
						column.Width(2);
						column.HeaderCell("مانده بدهکار");
						column.ColumnItemsTemplate(template =>
						{
							template.TextBlock();
							template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
								? "0" : $"{obj:n0}");
						});
						column.AggregateFunction(aggregateFunction =>
						{
							aggregateFunction.CustomAggregateFunction(new SumNull());
							aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
								? "0" : $"{obj:n0}");
						});

					});
				})
				.MainTableEvents(events =>
				{
					events.DataSourceIsEmpty(message: "هیچ داده ای در دسترسی برای نمایش وجو ندارد.");

					events.CellCreated(args =>
					{
						if (args.CellType == CellType.PreviousPageSummaryCell ||
						    args.CellType == CellType.PageSummaryCell ||
						    args.CellType == CellType.SummaryRowCell)
						{
							if (!string.IsNullOrEmpty(args.Cell.RowData.FormattedValue) &&
							    args.Cell.RowData.PropertyName == "TotalDebtor")
							{
								args.Cell.RowData.FormattedValue += " ریال ";
							}

							if (!string.IsNullOrEmpty(args.Cell.RowData.FormattedValue) &&
							    args.Cell.RowData.PropertyName == "TotalCreditor")
							{
								args.Cell.RowData.FormattedValue += " ریال ";
							}
							if (!string.IsNullOrEmpty(args.Cell.RowData.FormattedValue) &&
							    args.Cell.RowData.PropertyName == "RemainDebtor")
							{
								args.Cell.RowData.FormattedValue += " ریال ";
							}

							if (!string.IsNullOrEmpty(args.Cell.RowData.FormattedValue) &&
							    args.Cell.RowData.PropertyName == "RemainCreditor")
							{
								args.Cell.RowData.FormattedValue += " ریال ";
							}
						}
					});

					//events.MainTableAdded(args =>
					//{
					//	/*var objData = args.ColumnCellsSummaryData.Where(x => x.CellData.PropertyName.Equals("Price"))
					//		.OrderByDescending(x => x.OverallRowNumber)
					//		.First()
					//		.OverallAggregateValue;*/

					//	var taxTable = new PdfGrid(relativeWidths: args.Table.RelativeWidths)
					//	{
					//		WidthPercentage = args.Table.WidthPercentage,
					//		SpacingBefore = args.Table.SpacingBefore
					//	}; // Create a clone of the MainTable's structure 

					//	var creditor = args.LastOverallAggregateValueOf<VW_AccountingDocumentPrint>(y => y.Creditor);
					//	var debtor = args.LastOverallAggregateValueOf<VW_AccountingDocumentPrint>(y => y.Debtor);

					//	var msgCreditor = $"مجموع بدهکار:  {creditor:n0} , \t" + double.Parse(creditor, NumberStyles.AllowThousands, CultureInfo.InvariantCulture).NumberToText(Language.Persian) + " ریال";
					//	var msgDebtor = $"مجموع بستانکار:  {debtor:n0} , \t" + double.Parse(debtor, NumberStyles.AllowThousands, CultureInfo.InvariantCulture).NumberToText(Language.Persian) + " ریال";

					//	var infoTable = new PdfGrid(numColumns: 1)
					//	{
					//		WidthPercentage = 100
					//	};

					//	infoTable.AddSimpleRow(
					//		(cellData, properties) =>
					//		{
					//			cellData.Value = msgCreditor;
					//			properties.CellPadding = 10;
					//			properties.PdfFont = events.PdfFont;
					//			properties.RunDirection = PdfRunDirection.RightToLeft;
					//		});

					//	infoTable.AddSimpleRow(
					//		(cellData, properties) =>
					//		{
					//			cellData.Value = msgDebtor;
					//			properties.CellPadding = 10;
					//			properties.PdfFont = events.PdfFont;
					//			properties.RunDirection = PdfRunDirection.RightToLeft;
					//		});

					//	args.PdfDoc.Add(infoTable.AddBorderToTable(borderColor: BaseColor.LIGHT_GRAY, spacingBefore: 10f));
					//});
				})
				.Export(export =>
				{
					export.ToExcel();
				})
				.Generate(data =>
				{
					fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
					data.FlushInBrowser(fileName, FlushType.Inline);
				}); // creating an in-memory PDF file
		}

		private static PdfGrid CreateHeader(PagesHeaderBuilder header, string valDate, DbContext model, string querySQL)
		{
			var table = new PdfGrid(numColumns: 3)
			{
				WidthPercentage = 100,
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				SpacingAfter = 25
			};
			table.DefaultCell.Border = Rectangle.NO_BORDER;

			table.SetWidths(new int[] { 33, 33, 33 });

			#region Table 1

			var tb1 = new PdfPTable(numColumns: 2)
			{
				PaddingTop = 50,
			};

			tb1.DefaultCell.Top = 50;
			tb1.DefaultCell.PaddingTop = 50;
			tb1.DefaultCell.MinimumHeight = 60;
			tb1.DefaultCell.Border = Rectangle.NO_BORDER;
			tb1.DefaultCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

			var year = header.PdfFont.FontSelector.Process("سال مالی:");
			// todo: insert year finacial
			var valYear = header.PdfFont.FontSelector.Process(Convert.ToDateTime(valDate).ToString("yyyy"));
			var fromDate = header.PdfFont.FontSelector.Process("از تاریخ:");
			// todo: insert from date
			var valFromDate = header.PdfFont.FontSelector.Process(Convert.ToDateTime(valDate).ToString("yyyy/mm/dd"));
			var toDate = header.PdfFont.FontSelector.Process("تا تاریخ:");
			// todo: insert to date
			var valToDate = header.PdfFont.FontSelector.Process(Convert.ToDateTime(valDate).ToString("yyyy/mm/dd"));

			var balance = header.PdfFont.FontSelector.Process("حساب تفصیلی:");
			var valBalance = header.PdfFont.FontSelector.Process(string.Empty);


			tb1.AddCell(new PdfPCell(valYear)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Border = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Padding = 5,
				PaddingTop = 13,

			});
			tb1.AddCell(new PdfPCell(year)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				ArabicOptions = 1,
				Padding = 5,
				PaddingTop = 13,
			});


			tb1.AddCell(new PdfPCell(valFromDate)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Border = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Padding = 5,

			});
			tb1.AddCell(new PdfPCell(fromDate)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				ArabicOptions = 1,
				Padding = 5,
			});

			tb1.AddCell(new PdfPCell(valToDate)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Border = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Padding = 5,

			});
			tb1.AddCell(new PdfPCell(toDate)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				ArabicOptions = 1,
				Padding = 5,
			});



			tb1.AddCell(new PdfPCell(valBalance)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_LTR,
				Border = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Padding = 5

			});
			tb1.AddCell(new PdfPCell(balance)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Padding = 5

			});

			table.AddCell(new PdfPTable(tb1));

			#endregion

			#region Table 2

			var tb2 = new PdfPTable(numColumns: 1);
			tb2.DefaultCell.MinimumHeight = 60;
			tb2.DefaultCell.Border = Rectangle.NO_BORDER;

			var headerTitle = header;
			var headerTitle2 = headerTitle;

			//headerTitle.PdfFont.FontSelector.AddFont(font3);
			headerTitle.PdfFont.Size = 20;
			headerTitle.PdfFont.Style = DocumentFontStyle.Bold;



			var nameCo = headerTitle.PdfFont.FontSelector.Process(_report.DocumentName);

			tb2.AddCell(new PdfPCell(nameCo)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Top = 25,

			});

			headerTitle2.PdfFont.Size = 30;
			headerTitle2.PdfFont.Style = DocumentFontStyle.Bold;

			var nameDoc = headerTitle2.PdfFont.FontSelector.Process(_report.CompanyName);
			tb2.AddCell(new PdfPCell(nameDoc)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				PaddingTop = 5,
				PaddingBottom = 15
			});
			table.AddCell(new PdfPTable(tb2));


			#endregion

			#region Table 3

			var tb3 = new PdfPTable(numColumns: 2);
			tb3.DefaultCell.MinimumHeight = 30;
			tb3.DefaultCell.Border = Rectangle.NO_BORDER;

			header.PdfFont.Size = 9;
			header.PdfFont.Style = DocumentFontStyle.Normal;

			var docType = header.PdfFont.FontSelector.Process("تاریخ ایجاد:");
			// todo:insert date created
			var valType = header.PdfFont.FontSelector.Process(Convert.ToDateTime(valDate).ToString("yyyy/mm/dd"));



			tb3.AddCell(new PdfPCell(valType)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Border = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Padding = 15,


			});
			tb3.AddCell(new PdfPCell(docType)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_RIGHT,
				Padding = 15,
			});

			header.PdfFont.Size = 9;
			header.PdfFont.Style = DocumentFontStyle.Normal;
			// todo: insert person created
			var valDesc = header.PdfFont.FontSelector.Process("amin");

			tb3.AddCell(new PdfPCell(valDesc)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Border = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Padding = 10,
			});

			header.PdfFont.Size = 9;
			var docDesc = header.PdfFont.FontSelector.Process("ایجاد کننده:");


			tb3.AddCell(new PdfPCell(docDesc)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_RIGHT,
				Padding = 10,
			});



			table.AddCell(new PdfPTable(tb3));

			#endregion

			return table;
		}

		private static PdfGrid CreateFooter(PagesFooterBuilder footer, string date, FooterData data)
		{
			var table = new PdfGrid(numColumns: 1)
			{
				WidthPercentage = 100,
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				SpacingAfter = 10,
			};

			table.DefaultCell.Border = Rectangle.NO_BORDER;
			table.SetWidths(new int[] { 100 });

			//var rowTB_1 = new PdfPTable(numColumns: 3)
			//{
			//	SpacingAfter = 4
			//};
			//rowTB_1.DefaultCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
			//rowTB_1.DefaultCell.Border = Rectangle.TOP_BORDER;
			//rowTB_1.DefaultCell.BorderColorRight = BaseColor.LIGHT_GRAY;
			//rowTB_1.DefaultCell.BorderWidthTop = 1;
			//rowTB_1.SetWidths(new int[] { 33, 33, 33 });

			//#region Row 1

			//#region Table 3

			//var tb13 = new PdfPTable(numColumns: 2);
			//tb13.DefaultCell.MinimumHeight = 20;
			//tb13.DefaultCell.Border = Rectangle.NO_BORDER;


			//var valConfirmation3 = footer.PdfFont.FontSelector.Process(_report.Confirmatory);
			//tb13.AddCell(new PdfPCell(valConfirmation3)
			//{
			//	RunDirection = PdfWriter.RUN_DIRECTION_RTL,
			//	Padding = 4,
			//	PaddingLeft = 0,
			//	HorizontalAlignment = Element.ALIGN_CENTER,
			//	Border = Rectangle.NO_BORDER
			//});

			//var dataConfirmation3 = footer.PdfFont.FontSelector.Process("تایید کننده :");
			//tb13.AddCell(new PdfPCell(dataConfirmation3)
			//{
			//	RunDirection = PdfWriter.RUN_DIRECTION_RTL,
			//	Padding = 4,
			//	HorizontalAlignment = Element.ALIGN_CENTER,
			//	Border = Rectangle.NO_BORDER
			//});

			//rowTB_1.AddCell(tb13);
			//#endregion

			//#region Table 2

			//var tb12 = new PdfPTable(numColumns: 2);
			//tb12.DefaultCell.MinimumHeight = 20;
			//tb12.DefaultCell.Border = Rectangle.NO_BORDER;


			//var valConfirmation = footer.PdfFont.FontSelector.Process(_report.FinancialMaanager);
			//tb12.AddCell(new PdfPCell(valConfirmation)
			//{
			//	RunDirection = PdfWriter.RUN_DIRECTION_RTL,
			//	Padding = 4,
			//	PaddingLeft = 0,
			//	BorderColorTop = new BaseColor(System.Drawing.Color.LightGray.ToArgb()),
			//	HorizontalAlignment = Element.ALIGN_CENTER,
			//	Border = Rectangle.NO_BORDER
			//});
			//var dataConfirmation = footer.PdfFont.FontSelector.Process("مدیر مالی :");
			//tb12.AddCell(new PdfPCell(dataConfirmation)
			//{
			//	RunDirection = PdfWriter.RUN_DIRECTION_RTL,
			//	Padding = 4,
			//	HorizontalAlignment = Element.ALIGN_CENTER,
			//	Border = Rectangle.NO_BORDER
			//});

			//rowTB_1.AddCell(tb12);
			//#endregion

			//#region Table 1

			//var tb11 = new PdfPTable(numColumns: 2);
			//tb11.DefaultCell.MinimumHeight = 20;
			//tb11.DefaultCell.Border = Rectangle.NO_BORDER;

			//var valRegolator = footer.PdfFont.FontSelector.Process(_report.Regulator);
			//tb11.AddCell(new PdfPCell(valRegolator)
			//{
			//	RunDirection = PdfWriter.RUN_DIRECTION_RTL,
			//	Padding = 4,
			//	HorizontalAlignment = Element.ALIGN_CENTER,
			//	Border = Rectangle.NO_BORDER
			//});

			//var dateRegolator = footer.PdfFont.FontSelector.Process("تنظیم کننده :");
			//tb11.AddCell(new PdfPCell(dateRegolator)
			//{
			//	RunDirection = PdfWriter.RUN_DIRECTION_RTL,
			//	Padding = 4,
			//	HorizontalAlignment = Element.ALIGN_CENTER,
			//	Border = Rectangle.NO_BORDER
			//});

			//rowTB_1.AddCell(tb11);
			//#endregion

			//#endregion

			var rowTB_2 = new PdfPTable(3)
			{
				SpacingAfter = 15
			};
			rowTB_2.DefaultCell.Border = Rectangle.TOP_BORDER;
			rowTB_2.DefaultCell.BorderColorRight = BaseColor.LIGHT_GRAY;
			rowTB_2.DefaultCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
			rowTB_2.DefaultCell.BorderWidthTop = 1;
			rowTB_2.SetWidths(new int[] { 33, 33, 33 });

			#region Row 2

			#region Table 1
			var tb21 = new PdfPTable(numColumns: 1);
			tb21.DefaultCell.MinimumHeight = 20;


			var nameCo = footer.PdfFont.FontSelector.Process("ساخته شده توسط نرم افزار مدیریت مالی دکا");
			tb21.AddCell(new PdfPCell(nameCo)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Padding = 4,
				HorizontalAlignment = Element.ALIGN_RIGHT,
				Border = Rectangle.NO_BORDER
			});

			rowTB_2.AddCell(tb21);
			#endregion

			#region Table 2

			var tb22 = new PdfPTable(numColumns: 1);
			tb22.DefaultCell.MinimumHeight = 20;

			tb22.AddCell(new PdfPCell()
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Padding = 4,
				HorizontalAlignment = Element.ALIGN_RIGHT,
				Border = Rectangle.NO_BORDER
			});

			rowTB_2.AddCell(tb22);
			#endregion

			#region Table 3

			var tb23 = new PdfPTable(numColumns: 2);
			tb23.DefaultCell.MinimumHeight = 20;

			tb23.AddCell(new PdfPCell(data.TotalPagesCountImage)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Padding = 4,
				PaddingLeft = 0,
				HorizontalAlignment = Element.ALIGN_LEFT,
				Border = Rectangle.NO_BORDER
			});

			var pageNumberPhrase = footer.PdfFont.FontSelector.Process("صفحه " + data.CurrentPageNumber + "  از  ");
			tb23.AddCell(new PdfPCell(pageNumberPhrase)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Padding = 4,
				HorizontalAlignment = Element.ALIGN_RIGHT,
				Border = Rectangle.NO_BORDER
			});



			rowTB_2.AddCell(tb23);
			#endregion

			#endregion


			//table.AddCell(rowTB_1);
			table.AddCell(rowTB_2);
			return table;
		}
	}
}