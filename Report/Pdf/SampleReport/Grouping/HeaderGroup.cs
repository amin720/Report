using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfRpt.ColumnsItemsTemplates;
using PdfRpt.Core.Contracts;
using PdfRpt.Core.Helper;
using PdfRpt.FluentInterface;
using Report.Models;

namespace Report.Pdf.SampleReport.Grouping
{
	public class HeaderGroup : IPageHeader
	{
		public IPdfFont PdfRptFont { get; set; }
		public PagesHeaderBuilder Header { get; set; }
		public string Date { get; set; }
		public bool Temprary { get; set; }
		public PdfGrid RenderingGroupHeader(Document pdfDoc, PdfWriter pdfWriter, IList<CellData> newGroupInfo, IList<SummaryCellData> summaryData)
		{
			var IsPermenant = (bool)newGroupInfo.GetValueOf<AccountingPdfReport>(x => x.IsPermenant);
			var IsConfirmed = (bool)newGroupInfo.GetValueOf<AccountingPdfReport>(x => x.IsConfirmed);

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

			var noMov = Header.PdfFont.FontSelector.Process("شماره موقت:");
			var valMov = Header.PdfFont.FontSelector.Process(newGroupInfo.GetSafeStringValueOf<AccountingPdfReport>(x => x.TemporaryDocumentNumber));
			var noDae = Header.PdfFont.FontSelector.Process("شماره دائم:");
			var valDae = Header.PdfFont.FontSelector.Process(newGroupInfo.GetSafeStringValueOf<AccountingPdfReport>(x => x.PermanantDocumentNumber));
			var state = Header.PdfFont.FontSelector.Process("وضعیت سند:");
			var valState = Header.PdfFont.FontSelector.Process(string.Empty);

			if (IsConfirmed)
			{
				valState = Header.PdfFont.FontSelector.Process(IsPermenant ? "دائم" : "تاییده شده");
			}


			var date = Header.PdfFont.FontSelector.Process("تاریخ سند:");
			var datePhrase = Header.PdfFont.FontSelector.Process(Date);

			if (Temprary)
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

			if (!string.IsNullOrEmpty(valState.Content))
			{
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
			}
			



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

			var HeaderTitle = Header;
			var HeaderTitle2 = HeaderTitle;

			//HeaderTitle.PdfFont.FontSelector.AddFont(font3);
			HeaderTitle.PdfFont.Size = 25;
			HeaderTitle.PdfFont.Style = DocumentFontStyle.Bold;



			var nameCo = HeaderTitle.PdfFont.FontSelector.Process("نام سند");

			tb2.AddCell(new PdfPCell(nameCo)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Top = 25,

			});

			HeaderTitle2.PdfFont.Size = 36;
			HeaderTitle2.PdfFont.Style = DocumentFontStyle.Bold;

			var nameDoc = HeaderTitle2.PdfFont.FontSelector.Process(newGroupInfo.GetSafeStringValueOf<AccountingPdfReport>(x => x.OrganizationTitle));
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

			Header.PdfFont.Size = 9;
			Header.PdfFont.Style = DocumentFontStyle.Normal;

			var docType = Header.PdfFont.FontSelector.Process("نوع سند:");
			var valType = Header.PdfFont.FontSelector.Process("123214");



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

			Header.PdfFont.Size = 9;
			Header.PdfFont.Style = DocumentFontStyle.Normal;

			var valDesc = Header.PdfFont.FontSelector.Process(newGroupInfo.GetSafeStringValueOf<AccountingPdfReport>(x => x.Header));

			tb3.AddCell(new PdfPCell(valDesc)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Border = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Padding = 10,
			});

			Header.PdfFont.Size = 9;
			var docDesc = Header.PdfFont.FontSelector.Process("شرح سند:");


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

		public PdfGrid RenderingReportHeader(Document pdfDoc, PdfWriter pdfWriter, IList<SummaryCellData> summaryData)
		{
			//var table = new PdfGrid(numColumns: 1) { WidthPercentage = 100 };

			//table.AddSimpleRow(
			//	(cellData, cellProperties) =>
			//	{
			//		cellData.Value = "گروه بندی کارمندان بر اساس AccountDocumentId";
			//		cellProperties.PdfFont = PdfRptFont;
			//		cellProperties.PdfFontStyle = DocumentFontStyle.Bold;
			//		cellProperties.HorizontalAlignment = HorizontalAlignment.Center;
			//		cellProperties.RunDirection = PdfRunDirection.RightToLeft;
			//	});
			//return table.AddBorderToTable();
			return null;
		}
	}
}