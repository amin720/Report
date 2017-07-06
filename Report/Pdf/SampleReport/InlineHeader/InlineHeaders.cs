using System;
using System.Collections.Generic;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfRpt.Core.Contracts;
using PdfRpt.Core.Helper;
using PdfRpt.FluentInterface;
using Report.Models;

namespace Report.Pdf.SampleReport.InlineHeader
{
	public class InlineHeaders
	{
		DBFirst _db = new DBFirst();

		private readonly Font font1 = InstallFonts.GetFont("bnazanb");
		private readonly Font font2 = InstallFonts.GetFont("arial");
		private static readonly Font font3 = InstallFonts.GetFont("BTitrBd");
		public IPdfReportData CreatePdfReport()
		{
			return new PdfReport().DocumentPreferences(doc =>
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
					fonts.Size(9);
					fonts.Color(System.Drawing.Color.Black);
				})
				.PagesFooter(footer =>
				{
					var date = DateTime.Now.ToString("MM/dd/yyyy");
					footer.InlineFooter(inlineFooter =>
					{
						inlineFooter.FooterProperties(new FooterBasicProperties
						{
							PdfFont = footer.PdfFont,
							HorizontalAlignment = HorizontalAlignment.Center,
							RunDirection = PdfRunDirection.LeftToRight,
							SpacingBeforeTable = 30,
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
					header.InlineHeader(inlineHeader =>
					{
						inlineHeader.AddPageHeader(data =>
						{
							var valDate = DateTime.Now.ToPersianDateTime(" / ", false);

							return CreateHeader(header,valDate);
						});
					});
				})
				.MainTableTemplate(template =>
				{
					template.BasicTemplate(BasicTemplate.ClassicTemplate);
				})
				.MainTablePreferences(table =>
				{
					table.ColumnsWidthsType(TableColumnWidthType.Relative);
				})
				.MainTableDataSource(dataSource =>
				{
					var employess = _db.Employees.OrderBy(e => e.FullName).ToList();

					dataSource.StronglyTypedList(employess);
				})
				.MainTableSummarySettings(summarySettings =>
				{
					summarySettings.OverallSummarySettings("Summary");
					summarySettings.PreviousPageSummarySettings("Previous Page Summary");
					summarySettings.PageSummarySettings("Page Summary");
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
						column.PropertyName<Employee>(x => x.Id);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(1);
						column.Width(2);
						column.HeaderCell("Id");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(x => x.FullName);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(2);
						column.Width(3);
						column.HeaderCell("Name");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(x => x.Salary);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(3);
						column.Width(2);
						column.HeaderCell("Balance");
						column.ColumnItemsTemplate(template =>
						{
							template.TextBlock();
							template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
								? string.Empty : string.Format("{0:n0}", obj));
						});
						column.AggregateFunction(aggregateFunction =>
						{
							aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
							aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
								? string.Empty : string.Format("{0:n0}", obj));
						});
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<Employee>(x => x.Id);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(4);
						column.Width(2);
						column.HeaderCell("QRCode");
						column.ColumnItemsTemplate(itemsTemplate =>
						{
							itemsTemplate.InlineField(inlineField =>
							{
								inlineField.RenderCell(cellData =>
								{
									var data = cellData.Attributes.RowData.TableRowData;
									var id = data.GetSafeStringValueOf<Employee>(x => x.Id);

									var qrcode = new BarcodeQRCode(id, 1, 1, null);
									var image = qrcode.GetImage();
									var mask = qrcode.GetImage();
									mask.MakeMask();
									image.ImageMask = mask; // making the background color transparent
									var pdfCell = new PdfPCell(image, fit: false);

									return pdfCell;
								});
							});
						});
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
				.Generate(data => data.AsPdfFile(string.Format("{0}\\Pdf\\InlineProvidersPdfReport-{1}.pdf", AppPath.ApplicationPath, Guid.NewGuid().ToString("N"))), debugMode: true);
		}

		private static PdfGrid CreateHeader(PagesHeaderBuilder header,string valDate)
		{
			var table = new PdfGrid(numColumns: 3)
			{
				WidthPercentage = 100,
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				SpacingAfter = 25
			};
			table.DefaultCell.Border = Rectangle.NO_BORDER;

			table.SetWidths(new int[]{33,33,33});

			// table 1
			var tb1 = new PdfPTable(numColumns: 2)
			{
				//PaddingTop = 30
			};
			tb1.DefaultCell.MinimumHeight = 60;
			tb1.DefaultCell.Border = Rectangle.NO_BORDER;
			tb1.DefaultCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

			var noMov = header.PdfFont.FontSelector.Process("شماره موقت:");
			var valMov = header.PdfFont.FontSelector.Process("123214");
			var noDae = header.PdfFont.FontSelector.Process("شماره دائم:");
			var valDae = header.PdfFont.FontSelector.Process("12324");
			var date = header.PdfFont.FontSelector.Process("تاریخ:");
			var datePhrase = header.PdfFont.FontSelector.Process(valDate);

			
			tb1.AddCell(new PdfPCell(valMov)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Border = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Padding = 5,
				

			});
			tb1.AddCell(new PdfPCell(noMov)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_RIGHT,
				ArabicOptions = 1,
				Padding = 5,
			});

			
			tb1.AddCell(new PdfPCell(valDae)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Border = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Padding = 10,

			});
			tb1.AddCell(new PdfPCell(noDae)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_RIGHT,
				ArabicOptions = 1,
				Padding = 10,
			});


			
			tb1.AddCell(new PdfPCell(datePhrase)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_LTR,
				Border = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				//PaddingRight = 0,
				//PaddingTop = 10,
				//PaddingLeft = 0,
				//PaddingBottom = 15,
				Padding = 5

			});
			tb1.AddCell(new PdfPCell(date)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				//PaddingRight = 0,
				//PaddingTop = 10,
				//PaddingLeft = 0,
				//PaddingBottom = 15,
				Padding = 5

			});

			table.AddCell(new PdfPTable(tb1));

			// table 2
			var tb2 = new PdfPTable(numColumns: 1);
			tb2.DefaultCell.MinimumHeight = 60;
			tb2.DefaultCell.Border = Rectangle.NO_BORDER;

			var headerTitle = header;
			var headerTitle2 = headerTitle;

			if (headerTitle.PdfFont != null)
			{
				headerTitle.PdfFont.FontSelector.AddFont(font3);
				headerTitle.PdfFont.Size = 25;
				headerTitle.PdfFont.Style = DocumentFontStyle.Bold;

			

				var nameCo = headerTitle.PdfFont.FontSelector.Process("نام شرکت");

				tb2.AddCell(new PdfPCell(nameCo)
				{
					RunDirection = PdfWriter.RUN_DIRECTION_RTL,
					BorderWidth = 0,
					HorizontalAlignment = Element.ALIGN_CENTER,
					Top = 25,

				});

				headerTitle2.PdfFont.Size = 40;
				headerTitle2.PdfFont.Style = DocumentFontStyle.Bold;

				var nameDoc = headerTitle2.PdfFont.FontSelector.Process("نام سند");
				tb2.AddCell(new PdfPCell(nameDoc)
				{
					RunDirection = PdfWriter.RUN_DIRECTION_RTL,
					BorderWidth = 0,
					HorizontalAlignment = Element.ALIGN_CENTER,
					PaddingTop = 5,
					PaddingBottom = 15
				});
				table.AddCell(new PdfPTable(tb2));

				// table 3
				var tb3 = new PdfPTable(numColumns: 2);
				tb3.DefaultCell.MinimumHeight = 30;
				tb3.DefaultCell.Border = Rectangle.NO_BORDER;

				header.PdfFont.Size = 9;
				header.PdfFont.Style = DocumentFontStyle.Normal;

				var docType = header.PdfFont.FontSelector.Process("نوع سند:");
				var valType = header.PdfFont.FontSelector.Process("123214");
				var docDesc = header.PdfFont.FontSelector.Process("شرح سند:");
				var valDesc = header.PdfFont.FontSelector.Process("12324");

			
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


			
				tb3.AddCell(new PdfPCell(valDesc)
				{
					RunDirection = PdfWriter.RUN_DIRECTION_RTL,
					Border = 0,
					HorizontalAlignment = Element.ALIGN_CENTER,
					Padding = 10,

				});
				tb3.AddCell(new PdfPCell(docDesc)
				{
					RunDirection = PdfWriter.RUN_DIRECTION_RTL,
					BorderWidth = 0,
					HorizontalAlignment = Element.ALIGN_RIGHT,
					Padding = 10,
				});

				table.AddCell(new PdfPTable(tb3));
			}

			return table;
		}

		private static PdfGrid CreateFooter(PagesFooterBuilder footer, string date, FooterData data)
		{
			var table = new PdfGrid(numColumns: 4)
			{
				WidthPercentage = 100,
				RunDirection = PdfWriter.RUN_DIRECTION_LTR
			};

			var datePhrase = footer.PdfFont.FontSelector.Process(date);
			var datePdfCell = new PdfPCell(datePhrase)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_LTR,
				BorderWidthLeft = 0,
				BorderWidthRight = 0,
				BorderWidthTop = 1,
				BorderWidthBottom = 0,
				Padding = 4,
				BorderColorTop = new BaseColor(System.Drawing.Color.LightGray.ToArgb()),
				HorizontalAlignment = Element.ALIGN_CENTER
			};

			var nullPdfCell = new PdfPCell
			{
				RunDirection = PdfWriter.RUN_DIRECTION_LTR,
				BorderWidthLeft = 0,
				BorderWidthRight = 0,
				BorderWidthTop = 1,
				BorderWidthBottom = 0,
				Padding = 4,
				BorderColorTop = new BaseColor(System.Drawing.Color.LightGray.ToArgb()),
				HorizontalAlignment = Element.ALIGN_RIGHT
			};

			var pageNumberPhrase = footer.PdfFont.FontSelector.Process("Page " + data.CurrentPageNumber + " of ");
			var pageNumberPdfCell = new PdfPCell(pageNumberPhrase)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_LTR,
				BorderWidthLeft = 0,
				BorderWidthRight = 0,
				BorderWidthTop = 1,
				BorderWidthBottom = 0,
				Padding = 4,
				BorderColorTop = new BaseColor(System.Drawing.Color.LightGray.ToArgb()),
				HorizontalAlignment = Element.ALIGN_RIGHT
			};

			var totalPagesNumberImagePdfCell = new PdfPCell(data.TotalPagesCountImage)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_LTR,
				BorderWidthLeft = 0,
				BorderWidthRight = 0,
				BorderWidthTop = 1,
				BorderWidthBottom = 0,
				Padding = 4,
				PaddingLeft = 0,
				BorderColorTop = new BaseColor(System.Drawing.Color.LightGray.ToArgb()),
				HorizontalAlignment = Element.ALIGN_LEFT
			};

			table.AddCell(datePdfCell);
			table.AddCell(nullPdfCell);
			table.AddCell(pageNumberPdfCell);
			table.AddCell(totalPagesNumberImagePdfCell);
			return table;
		}
	}
}