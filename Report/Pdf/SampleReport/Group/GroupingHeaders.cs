using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfRpt.ColumnsItemsTemplates;
using PdfRpt.Core.Contracts;
using PdfRpt.Core.Helper;
using Report.Models;

namespace Report.Pdf.SampleReport.Group
{
	// برای شخصی سازی هدر است ، حتما باید از این کلاس ارث بری شود
	public class GroupingHeaders : IPageHeader
	{
		public IPdfFont PdfRptFont { set; get; }
		public PdfGrid RenderingGroupHeader(Document pdfDoc, PdfWriter pdfWriter, IList<CellData> newGroupInfo, IList<SummaryCellData> summaryData)
		{
			var groupName = newGroupInfo.GetSafeStringValueOf<Employee>(e => e.Department);
			var age = newGroupInfo.GetSafeStringValueOf<Employee>(e => e.Age);
			var sex = newGroupInfo.GetSafeStringValueOf<Employee>(e => e.Gender);

			var table = new PdfGrid(relativeWidths: new[] { 1f, 5f }) { WidthPercentage = 100 };
			table.AddSimpleRow(
				(cellData, cellProperties) =>
				{
					cellData.Value = "جنسیت:";
					cellProperties.PdfFont = PdfRptFont;
					cellProperties.PdfFontStyle = DocumentFontStyle.Bold;
					cellProperties.HorizontalAlignment = HorizontalAlignment.Right;
					cellProperties.RunDirection = PdfRunDirection.RightToLeft;

				},
				(cellData, cellProperties) =>
				{
					cellData.Value = groupName;
					cellProperties.PdfFont = PdfRptFont;
					cellProperties.HorizontalAlignment = HorizontalAlignment.Right;
					cellProperties.RunDirection = PdfRunDirection.RightToLeft;
				});
			//table.AddSimpleRow(
			//	(cellData, cellProperties) =>
			//	{
			//		cellData.Value = "دپارتمان:";
			//		cellProperties.PdfFont = PdfRptFont;
			//		cellProperties.PdfFontStyle = DocumentFontStyle.Bold;
			//		cellProperties.HorizontalAlignment = HorizontalAlignment.Right;
			//		cellProperties.RunDirection = PdfRunDirection.RightToLeft;

			//	},
			//	(cellData, cellProperties) =>
			//	{
			//		cellData.Value = groupName;
			//		cellProperties.PdfFont = PdfRptFont;
			//		cellProperties.HorizontalAlignment = HorizontalAlignment.Right;
			//		cellProperties.RunDirection = PdfRunDirection.RightToLeft;

			//	});
			//table.AddSimpleRow((cellData, cellProperties) =>
			//	{
			//		cellData.Value = "سن:";
			//		cellProperties.PdfFont = PdfRptFont;
			//		cellProperties.PdfFontStyle = DocumentFontStyle.Bold;
			//		cellProperties.HorizontalAlignment = HorizontalAlignment.Right;
			//		cellProperties.RunDirection = PdfRunDirection.RightToLeft;
			//	},
			//	(cellData, cellProperties) =>
			//	{
			//		cellData.Value = age;
			//		cellProperties.PdfFont = PdfRptFont;
			//		cellProperties.HorizontalAlignment = HorizontalAlignment.Right;
			//		cellProperties.RunDirection = PdfRunDirection.RightToLeft;
			//	});
			return table.AddBorderToTable(borderColor: BaseColor.MAGENTA, spacingBefore: 10f);
		}

		public PdfGrid RenderingReportHeader(Document pdfDoc, PdfWriter pdfWriter, IList<SummaryCellData> summaryData)
		{
			var table = new PdfGrid(numColumns: 1) { WidthPercentage = 100 };

			table.AddSimpleRow(
				(cellData, cellProperties) =>
				{
					cellData.CellTemplate = new ImageFilePathField();
					cellData.Value = System.IO.Path.Combine(AppPath.ApplicationPath, "Pdf\\Images\\01.png");
					cellProperties.HorizontalAlignment = HorizontalAlignment.Center;
					cellProperties.RunDirection = PdfRunDirection.RightToLeft;
				});
			table.AddSimpleRow(
				(cellData, cellProperties) =>
				{
					cellData.Value = "گروه بندی کارمندان بر اساس جنسیت";
					cellProperties.PdfFont = PdfRptFont;
					cellProperties.PdfFontStyle = DocumentFontStyle.Bold;
					cellProperties.HorizontalAlignment = HorizontalAlignment.Center;
					cellProperties.RunDirection = PdfRunDirection.RightToLeft;
				});
			return table.AddBorderToTable();
		}
	}
}