﻿using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfRpt.Core.Contracts;
using PdfRpt.Core.Helper;
using PdfRpt.FluentInterface;
using Report.Models;
using Reportig.Template;

namespace Report.Pdf.SampleReport
{
	class InlineProvide
	{
		private static readonly Report.Pdf.ConstructurePdfReport _report = new Report.Pdf.ConstructurePdfReport();

		private static bool _temprary = false;

		// Install Fonts
		private readonly Font font1 = _report.GetFont(PersianFont.BNazanin);
		private readonly Font font2 = _report.GetFont(EnglishFont.Calibri);

		public IPdfReportData CreatePdfReport(DbContext modelDbContext = null, string sqlQuery = null, bool tempraryStatus = false)
		{
			_temprary = tempraryStatus;

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
					doc.DiagonalWatermark(new DiagonalWatermark()
					{
						Text = "تست می باشد",
						RunDirection = PdfRunDirection.RightToLeft,
						Font = _report.GetWatermarkFont()
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
					//if (!string.IsNullOrEmpty(sqlQuery))
					//{
					// TODO: repair
					//var model = new Deca();

					//string querySql = @"SELECT * FROM [DecaFinancial].[dbo].[AccountingPdfReport]";

					var db = modelDbContext.Database.SqlQuery<AccountingPdfReport>(sql: sqlQuery).ToList();
					//}
					//else
					//{
					//	//var employess = model.ViewModel.AccountingViewModels.OrderBy(e => e.FullName).ToList();

					//}

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
						column.Width(1);
						column.HeaderCell("#");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<AccountingPdfReport>(x => x.Total);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(1);
						column.Width(2);
						column.HeaderCell("حساب کل");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<AccountingPdfReport>(x => x.Certain);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(2);
						column.Width(2);
						column.HeaderCell("حساب معین");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<AccountingPdfReport>(x => x.Detailed);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(3);
						column.Width(2);
						column.HeaderCell("حساب تفصیلی");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<AccountingPdfReport>(x => x.Description);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(4);
						column.Width(4);
						column.HeaderCell("شرح");
					});

					columns.AddColumn(column =>
					{
						column.PropertyName<AccountingPdfReport>(x => x.Debtor);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(5);
						column.Width(2);
						column.HeaderCell("بستانکار");
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

					columns.AddColumn(column =>
					{
						column.PropertyName<AccountingPdfReport>(x => x.Creditor);
						column.CellsHorizontalAlignment(HorizontalAlignment.Center);
						column.IsVisible(true);
						column.Order(6);
						column.Width(2);
						column.HeaderCell("بدهکار");
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

					//columns.AddColumn(column =>
					//{
					//	column.PropertyName<ViewModel.AccountingViewModel>(x => x.Id);
					//	column.CellsHorizontalAlignment(HorizontalAlignment.Center);
					//	column.IsVisible(true);
					//	column.Order(4);
					//	column.Width(2);
					//	column.HeaderCell("QRCode");
					//	column.ColumnItemsTemplate(itemsTemplate =>
					//	{
					//		itemsTemplate.InlineField(inlineField =>
					//		{
					//			inlineField.RenderCell(cellData =>
					//			{
					//				var data = cellData.Attributes.RowData.TableRowData;
					//				var id = data.GetSafeStringValueOf<ViewModel.AccountingViewModel>(x => x.Id);
					//				var person = data.GetSafeStringValueOf<ViewModel.AccountingViewModel>(e => e.FullName);
					//				var salary = data.GetSafeStringValueOf<ViewModel.AccountingViewModel>(e => e.Salary);
					//				var department = data.GetSafeStringValueOf<ViewModel.AccountingViewModel>(e => e.Department.Name);
					//				var manager = data.GetSafeStringValueOf<ViewModel.AccountingViewModel>(e => e.Manager.ViewModel.AccountingViewModel.FullName);

					//				var dataString =
					//					$"The {person} with id: {id} , Salary: {salary}$ \n\n Department: {department} \n\n Manager: {manager}";

					//				var qrcode = new BarcodeQRCode(dataString, 1, 1, null);
					//				var image = qrcode.GetImage();
					//				var mask = qrcode.GetImage();
					//				mask.MakeMask();
					//				image.ImageMask = mask; // making the background color transparent
					//				var pdfCell = new PdfPCell(image, fit: false);

					//				return pdfCell;
					//			});
					//		});
					//	});
					//});
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
								args.Cell.RowData.PropertyName == "Debtor")
							{
								args.Cell.RowData.FormattedValue += " ریال";
							}

							if (!string.IsNullOrEmpty(args.Cell.RowData.FormattedValue) &&
								args.Cell.RowData.PropertyName == "Creditor")
							{
								args.Cell.RowData.FormattedValue += " ریال";
							}
						}
					});

					events.MainTableAdded(args =>
					{
						/*var objData = args.ColumnCellsSummaryData.Where(x => x.CellData.PropertyName.Equals("Price"))
							.OrderByDescending(x => x.OverallRowNumber)
							.First()
							.OverallAggregateValue;*/

						var taxTable = new PdfGrid(relativeWidths: args.Table.RelativeWidths)
						{
							WidthPercentage = args.Table.WidthPercentage,
							SpacingBefore = args.Table.SpacingBefore
						}; // Create a clone of the MainTable's structure 

						var creditor = args.LastOverallAggregateValueOf<AccountingPdfReport>(y => y.Creditor);
						var debtor = args.LastOverallAggregateValueOf<AccountingPdfReport>(y => y.Debtor);

						var msgCreditor = $"مجموع بدهکار:  {creditor:n0} , \t" + double.Parse(creditor, NumberStyles.AllowThousands, CultureInfo.InvariantCulture).NumberToText(Language.Persian) +" ریال";
						var msgDebtor = $"مجموع بستانکار:  {debtor:n0} , \t" + double.Parse(debtor, NumberStyles.AllowThousands, CultureInfo.InvariantCulture).NumberToText(Language.Persian)+" ریال";

						var infoTable = new PdfGrid(numColumns: 1)
						{
							WidthPercentage = 100
						};

						//infoTable.AddSimpleRow(
						//	(cellData, properties) =>
						//	{
						//		cellData.Value = "Show data after the main table ...";
						//		properties.PdfFont = events.PdfFont;
						//		properties.RunDirection = PdfRunDirection.RightToLeft;
						//	});

						infoTable.AddSimpleRow(
							(cellData, properties) =>
							{
								cellData.Value = msgCreditor;
								properties.CellPadding = 10;
								properties.PdfFont = events.PdfFont;
								properties.RunDirection = PdfRunDirection.RightToLeft;
							});

						infoTable.AddSimpleRow(
							(cellData, properties) =>
							{
								cellData.Value = msgDebtor;
								properties.CellPadding = 10;
								properties.PdfFont = events.PdfFont;
								properties.RunDirection = PdfRunDirection.RightToLeft;
							});

						//taxTable.AddSimpleRow(
						//	null, null, null,
						//	(data, cellProperties) =>
						//	{
						//		data.Value = "جمع بدهکار";
						//		cellProperties.PdfFont = args.PdfFont;
						//		cellProperties.HorizontalAlignment = HorizontalAlignment.Right;
						//	},
						//	(data, cellProperties) =>
						//	{
						//		data.Value = $"{creditor:n0}";
						//		cellProperties.BorderColor = BaseColor.LIGHT_GRAY;
						//		cellProperties.ShowBorder = true;
						//	});

						//taxTable.AddSimpleRow(
						//	null, null, null,
						//	null,
						//	(data, cellProperties) =>
						//	{
						//		data.Value = long.Parse(creditor, NumberStyles.AllowThousands, CultureInfo.InvariantCulture).NumberToText(Language.Persian) + " ریال";
						//		cellProperties.PdfFont = args.PdfFont;
						//		cellProperties.BorderColor = BaseColor.LIGHT_GRAY;
						//		cellProperties.ShowBorder = true;
						//		cellProperties.PdfFontStyle = DocumentFontStyle.Bold;
						//	});

						//taxTable.AddSimpleRow(
						//	null, null, null,
						//	(data, cellProperties) =>
						//	{
						//		data.Value = "جمع بستانکار";
						//		cellProperties.PdfFont = args.PdfFont;
						//		cellProperties.HorizontalAlignment = HorizontalAlignment.Right;
						//	},
						//	(data, cellProperties) =>
						//	{
						//		data.Value = $"{debtor:n0}";
						//		cellProperties.PdfFont = args.PdfFont;
						//		cellProperties.BorderColor = BaseColor.LIGHT_GRAY;
						//		cellProperties.ShowBorder = true;
						//	});

						//taxTable.AddSimpleRow(
						//	null, null, null,
						//	null,
						//	(data, cellProperties) =>
						//	{
						//		data.Value = long.Parse(debtor, NumberStyles.AllowThousands, CultureInfo.InvariantCulture).NumberToText(Language.Persian) + " ریال";
						//		cellProperties.PdfFont = args.PdfFont;
						//		cellProperties.BorderColor = BaseColor.LIGHT_GRAY;
						//		cellProperties.ShowBorder = true;
						//		cellProperties.PdfFontStyle = DocumentFontStyle.Bold;
						//	});

						//args.PdfDoc.Add(taxTable);

						args.PdfDoc.Add(infoTable.AddBorderToTable(borderColor: BaseColor.LIGHT_GRAY, spacingBefore: 10f));
					});
				})
				.Export(export =>
				{
					export.ToExcel();
				})
				.Generate(data => data.AsPdfFile(
					$"{Reportig.AppPath.ApplicationPath}\\Pdf\\InlineProvidersPdfReport-{Guid.NewGuid():N}.pdf"), debugMode: true);
		}

		private static PdfGrid CreateHeader(PagesHeaderBuilder header, string valDate, DbContext model, string querySQL)
		{
			var db = model.Database.SqlQuery<AccountingPdfReport>(sql: querySQL).ToList();

			// todo: make state document
			//var IsPermenant = db.sele

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

			var noMov = header.PdfFont.FontSelector.Process("شماره موقت:");
			var valMov = header.PdfFont.FontSelector.Process("123214");
			var noDae = header.PdfFont.FontSelector.Process("شماره دائم:");
			var valDae = header.PdfFont.FontSelector.Process("12324");
			var state = header.PdfFont.FontSelector.Process("وضعیت سند:");
			var valState = header.PdfFont.FontSelector.Process("موقت");
			var date = header.PdfFont.FontSelector.Process("تاریخ سند:");
			var datePhrase = header.PdfFont.FontSelector.Process(valDate);

			// todo: make state document
			//if ()
			//{

			//}

			if (_temprary)
			{
				tb1.AddCell(new PdfPCell(valMov)
				{
					RunDirection = PdfWriter.RUN_DIRECTION_RTL,
					Border = 0,
					HorizontalAlignment = Element.ALIGN_CENTER,
					Padding = 5,
					PaddingTop = 13,

				});
				tb1.AddCell(new PdfPCell(noMov)
				{
					RunDirection = PdfWriter.RUN_DIRECTION_RTL,
					BorderWidth = 0,
					HorizontalAlignment = Element.ALIGN_CENTER,
					ArabicOptions = 1,
					Padding = 5,
					PaddingTop = 13,
				});
			}
			
			tb1.AddCell(new PdfPCell(valDae)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Border = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Padding = 5,

			});
			tb1.AddCell(new PdfPCell(noDae)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				ArabicOptions = 1,
				Padding = 5,
			});

			tb1.AddCell(new PdfPCell(valState)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Border = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Padding = 5,

			});
			tb1.AddCell(new PdfPCell(state)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				ArabicOptions = 1,
				Padding = 5,
			});



			tb1.AddCell(new PdfPCell(datePhrase)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_LTR,
				Border = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Padding = 5

			});
			tb1.AddCell(new PdfPCell(date)
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
			headerTitle.PdfFont.Size = 25;
			headerTitle.PdfFont.Style = DocumentFontStyle.Bold;



			var nameCo = headerTitle.PdfFont.FontSelector.Process("نام سند");

			tb2.AddCell(new PdfPCell(nameCo)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Top = 25,

			});

			headerTitle2.PdfFont.Size = 40;
			headerTitle2.PdfFont.Style = DocumentFontStyle.Bold;

			var nameDoc = headerTitle2.PdfFont.FontSelector.Process("نام شرکت");
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

			var docType = header.PdfFont.FontSelector.Process("نوع سند:");
			var valType = header.PdfFont.FontSelector.Process("123214");



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

			var valDesc = header.PdfFont.FontSelector.Process("سلام خوبی منم خوبم \n راستی این برنامه جدید رو دیدی که \n دارن نرم افزار حسابداری درست میکنن");

			tb3.AddCell(new PdfPCell(valDesc)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Border = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Padding = 10,
			});

			header.PdfFont.Size = 9;
			var docDesc = header.PdfFont.FontSelector.Process("شرح سند:");


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

			var rowTB_1 = new PdfPTable(numColumns: 3)
			{
				SpacingAfter = 4
			};
			rowTB_1.DefaultCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
			rowTB_1.DefaultCell.Border = Rectangle.TOP_BORDER;
			rowTB_1.DefaultCell.BorderColorRight = BaseColor.LIGHT_GRAY;
			rowTB_1.DefaultCell.BorderWidthTop = 1;
			rowTB_1.SetWidths(new int[] { 33, 33, 33 });

			#region Row 1

			#region Table 3

			var tb13 = new PdfPTable(numColumns: 2);
			tb13.DefaultCell.MinimumHeight = 20;
			tb13.DefaultCell.Border = Rectangle.NO_BORDER;


			var valConfirmation3 = footer.PdfFont.FontSelector.Process("محمد امین زینالی");
			tb13.AddCell(new PdfPCell(valConfirmation3)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Padding = 4,
				PaddingLeft = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Border = Rectangle.NO_BORDER
			});

			var dataConfirmation3 = footer.PdfFont.FontSelector.Process("تایید کننده:");
			tb13.AddCell(new PdfPCell(dataConfirmation3)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Padding = 4,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Border = Rectangle.NO_BORDER
			});

			rowTB_1.AddCell(tb13);
			#endregion

			#region Table 2

			var tb12 = new PdfPTable(numColumns: 2);
			tb12.DefaultCell.MinimumHeight = 20;
			tb12.DefaultCell.Border = Rectangle.NO_BORDER;


			var valConfirmation = footer.PdfFont.FontSelector.Process("محمد امین زینالی");
			tb12.AddCell(new PdfPCell(valConfirmation)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Padding = 4,
				PaddingLeft = 0,
				BorderColorTop = new BaseColor(System.Drawing.Color.LightGray.ToArgb()),
				HorizontalAlignment = Element.ALIGN_CENTER,
				Border = Rectangle.NO_BORDER
			});
			var dataConfirmation = footer.PdfFont.FontSelector.Process("تایید کننده:");
			tb12.AddCell(new PdfPCell(dataConfirmation)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Padding = 4,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Border = Rectangle.NO_BORDER
			});

			rowTB_1.AddCell(tb12);
			#endregion

			#region Table 1

			var tb11 = new PdfPTable(numColumns: 2);
			tb11.DefaultCell.MinimumHeight = 20;
			tb11.DefaultCell.Border = Rectangle.NO_BORDER;

			var valRegolator = footer.PdfFont.FontSelector.Process("محمد امین زینالی");
			tb11.AddCell(new PdfPCell(valRegolator)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Padding = 4,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Border = Rectangle.NO_BORDER
			});

			var dateRegolator = footer.PdfFont.FontSelector.Process("تنظیم کننده:");
			tb11.AddCell(new PdfPCell(dateRegolator)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Padding = 4,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Border = Rectangle.NO_BORDER
			});

			rowTB_1.AddCell(tb11);
			#endregion

			#endregion

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


			table.AddCell(rowTB_1);
			table.AddCell(rowTB_2);
			return table;
		}
	}
}
