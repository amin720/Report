using System;
using System.Diagnostics;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Report.Pdf.SampleReport.Headers_Footers.Header_1
{
	public class Program
	{


		public void Main()
		{
			Font font = InstallFonts.GetFont("Arial");

			string fillName = System.IO.Path.Combine(AppPath.ApplicationPath, "Pdf\\data\\headerDoc.pdf");

			string fillNameNew = System.IO.Path.Combine(AppPath.ApplicationPath, "Pdf\\data\\headerDoc222.pdf");

			using (var existingFileStream = new FileStream(fillName, FileMode.Open))
			using (var newFileStream = new FileStream(fillNameNew, FileMode.Create))
			{
				var pdfReader = new PdfReader(existingFileStream);
				using (var stamper = new PdfStamper(pdfReader, newFileStream, '\0', true))
				{
					//نکته مهم جهت کار با اطلاعات فارسی
					//در غیراینصورت شاهد ثبت اطلاعات نخواهید بود
					stamper.AcroFields.AddSubstitutionFont(font.BaseFont);

					//form.Fields.Keys = تمام فیلدهای موجود در فرم
					var form = stamper.AcroFields;

					//مقدار دهی فیلدهای فرم
					form.SetField("noMov", "مقدار1");
					form.SetField("noDae", "مقدار2");
					form.SetField("date", "مقدار2");
					form.SetField("nameCo", "مقدار2");
					form.SetField("nameDoc", "مقدار2");
					form.SetField("docType", "مقدار2");
					form.SetField("docDesc", "مقدار2");

					// "Yes" and "Off" are valid values here
					//form.SetField("Check Box 1", "Yes");

					// "" and "Off" are valid values here
					//form.SetField("Option Button 1", "");

					// نحوه مقدار دهی لیست
					//form.SetListOption("ListBox1", new[] { "1مقدار یک", "مقدار دو1" }, null);
					//form.SetField("ListBox1", null);

					// به این ترتیب فرم دیگر توسط کاربر قابل ویرایش نخواهد بود
					//stamper.PartialFormFlattening --> جهت غیرقابل ویرایش نمودن فیلدی مشخص
					stamper.FormFlattening = false;

					stamper.Close();
					pdfReader.Close();
				}
			}

			//Process.Start("headerDoc222.pdf");
		}
	}
}