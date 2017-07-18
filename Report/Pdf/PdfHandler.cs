using System.IO;
using System.Net.Mime;
using System.Web;

namespace Report.Pdf
{
	public class PdfHandler : IHttpHandler
	{
		private static byte[] inMemoryFile()
		{
			//تولید پویای فایل در حافظه و یا حتی خواندن از یک نمونه موجود
			return File.ReadAllBytes(System.IO.Path.Combine(Reportig.AppPath.ApplicationPath, "Pdf\\Data\\headerDoc.pdf"));
		}

		public void ProcessRequest(HttpContext context)
		{
			var pdf = inMemoryFile();

			context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			context.Response.ContentType = MediaTypeNames.Application.Pdf;
			context.Response.AddHeader("Content-Length", pdf.Length.ToString());
			//context.Response.AddHeader("content-disposition", "attachment;filename=test.pdf");
			context.Response.AddHeader("content-disposition", "inline;filename=test.pdf");
			context.Response.Buffer = true;
			context.Response.Clear();
			context.Response.OutputStream.Write(pdf, 0, pdf.Length);
			context.Response.OutputStream.Flush();
			context.Response.OutputStream.Close();
			context.Response.End();
		}

		public bool IsReusable
		{
			get { return false; }
		}
	}
}