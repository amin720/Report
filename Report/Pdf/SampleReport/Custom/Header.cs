using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfRpt.Core.Contracts;
using PdfRpt.Core.Helper;
using PdfRpt.DataSources;
using PdfRpt.FluentInterface;
using Report.Models;

namespace Report.Pdf.SampleReport.Custom
{
	public class Header : IPageHeader
	{
		public ConstructurePdfReport Report { get; set; }
		public IPdfFont PdfRptFont { get; set; }
		public PagesHeaderBuilder HeaderBuilder { get; set; }
		public bool Temprary { get; set; }
		public DbContext Context { get; set; }
		public string SqlQuery { get; set; }

		private bool IsPermenant = false;
		private bool IsConfirmed = false;
		private int TemporaryDocumentNumber = 0;
		private int PermanantDocumentNumber = 0;
		private DateTime DocumentDate = DateTime.Now;
		private string DocumentDescription = string.Empty;

		public PdfGrid RenderingGroupHeader(Document pdfDoc, PdfWriter pdfWriter, IList<CellData> newGroupInfo, IList<SummaryCellData> summaryData)
		{
			return null;
		}

		public PdfGrid RenderingReportHeader(Document pdfDoc, PdfWriter pdfWriter, IList<SummaryCellData> summaryData)
		{
			var db = new DecaFinancialEntities();

			var modeldb = db.VW_AccountingDocumentPrint.ToList();

			//foreach (var print in modeldb)
			//{
			//	IsPermenant = print.IsPermenant;
			//	IsConfirmed = (bool) print.IsConfirmed;
			//	TemporaryDocumentNumber = (int) print.TemporaryDocumentNumber;
			//	PermanantDocumentNumber = (int) print.PermanantDocumentNumber;
			//	DocumentDate = (DateTime) print.DocumentDate;
			//	DocumentDescription = print.Header;
			//}



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

			var noMov = HeaderBuilder.PdfFont.FontSelector.Process("شماره موقت:");
			var valMov = HeaderBuilder.PdfFont.FontSelector.Process(TemporaryDocumentNumber.ToString());
			var noDae = HeaderBuilder.PdfFont.FontSelector.Process("شماره دائم:");
			var valDae = HeaderBuilder.PdfFont.FontSelector.Process(PermanantDocumentNumber.ToString());
			var state = HeaderBuilder.PdfFont.FontSelector.Process("وضعیت سند:");
			var valState = HeaderBuilder.PdfFont.FontSelector.Process(string.Empty);

			if (IsConfirmed)
			{
				valState = HeaderBuilder.PdfFont.FontSelector.Process(IsPermenant ? "دائم" : "تاییده شده");
			}


			var date = HeaderBuilder.PdfFont.FontSelector.Process("تاریخ سند:");
			var datePhrase = HeaderBuilder.PdfFont.FontSelector.Process(DocumentDate.ToString());

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

			var HeaderTitle = HeaderBuilder;
			var HeaderTitle2 = HeaderTitle;

			//HeaderTitle.PdfFont.FontSelector.AddFont(font3);
			HeaderTitle.PdfFont.Size = 25;
			HeaderTitle.PdfFont.Style = DocumentFontStyle.Bold;



			var nameCo = HeaderTitle.PdfFont.FontSelector.Process(Report.DocumentName);

			tb2.AddCell(new PdfPCell(nameCo)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				BorderWidth = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Top = 25,

			});

			HeaderTitle2.PdfFont.Size = 36;
			HeaderTitle2.PdfFont.Style = DocumentFontStyle.Bold;

			var nameDoc = HeaderTitle2.PdfFont.FontSelector.Process(Report.CompanyName);
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

			HeaderBuilder.PdfFont.Size = 9;
			HeaderBuilder.PdfFont.Style = DocumentFontStyle.Normal;

			var docType = HeaderBuilder.PdfFont.FontSelector.Process("نوع سند:");
			var valType = HeaderBuilder.PdfFont.FontSelector.Process("123214");



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

			HeaderBuilder.PdfFont.Size = 9;
			HeaderBuilder.PdfFont.Style = DocumentFontStyle.Normal;

			var valDesc = HeaderBuilder.PdfFont.FontSelector.Process("-");

			tb3.AddCell(new PdfPCell(valDesc)
			{
				RunDirection = PdfWriter.RUN_DIRECTION_RTL,
				Border = 0,
				HorizontalAlignment = Element.ALIGN_CENTER,
				Padding = 10,
			});

			HeaderBuilder.PdfFont.Size = 9;
			var docDesc = HeaderBuilder.PdfFont.FontSelector.Process("شرح سند:");


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
	}
}