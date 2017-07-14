using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using PdfRpt.Core.Contracts;
using Report.Models;
using Report.Pdf.SampleReport.Headers_Footers;
using Report.Pdf.SampleReport.InlineHeader;
using Reportig;

namespace Report.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

	    public ActionResult List()
	    {
		    var rpt = new Report.Pdf.Sample.List().CreatePdfReport();
		    var outputFilePath = rpt.FileName.Replace(HttpRuntime.AppDomainAppPath, string.Empty);
		    return File(outputFilePath, "application/pdf", "pdfRpt.pdf");
	    }
	    public ActionResult List2()
	    {
		    var rpt = new Report.Pdf.Sample.List().CreatePdfReport();
		    var outputFilePath = rpt.FileName.Replace(HttpRuntime.AppDomainAppPath, string.Empty);
		    return Redirect(outputFilePath);
	    }
	    public ActionResult Headers()
	    {
		    var rpt = new Headers_Footers().CreatePdfReport();
		    var outputFilePath = rpt.FileName.Replace(HttpRuntime.AppDomainAppPath, string.Empty);
		    return Redirect(outputFilePath);
	    }
	    public ActionResult InlineHeaders()
	    {
		    var rpt = new InlineHeaders().CreatePdfReport();
		    var outputFilePath = rpt.FileName.Replace(HttpRuntime.AppDomainAppPath, string.Empty);
		    return Redirect(outputFilePath);
	    }

	    public ActionResult Reporting()
	    {
		    var dbFirst = new DBFirst();

		    var  report = new Report.Pdf.ConstructurePdfReport()
		    {
				Author = "Mohammad Amin Zeynali",
				Application = "Deca Provider",
				Keywords = "Accounting, Deca",
				Subject = "Report",
				Title = "Data",
				PageSize = PdfPageSize.A4,
				Orientation = PageOrientation.Portrait
			};

		    var querySql = @"SELECT * FROM [DecaFinancial].[Accounting].[VW_AccountingDocumentPrint]";

			var outputFilePath = report.AccountingReport(dbFirst,querySql);

			return Redirect(outputFilePath);
	    }

	    public ActionResult Grouping()
	    {
		    var dbFirst = new DBFirst();

		    var report = new Report.Pdf.ConstructurePdfReport()
		    {
			    Author = "Mohammad Amin Zeynali",
			    Application = "Deca Provider",
			    Keywords = "Accounting, Deca",
			    Subject = "Report",
			    Title = "Data",
			    PageSize = PdfPageSize.A4,
			    Orientation = PageOrientation.Portrait
		    };

		    var querySql = @"SELECT * FROM [DecaFinancial].[Accounting].[VW_AccountingDocumentPrint]";

		    var outputFilePath = report.AccountingReportGrouping(dbFirst, querySql,tempraryStatus:true);

		    return Redirect(outputFilePath);
	    }

	    public ActionResult Printing()
	    {
		    var report = new Report.Pdf.ConstructurePdfReport();

		    var files = new List<string>
		    {
			    AppPath.ApplicationPath + "Pdf\\1 (1).pdf",
			    AppPath.ApplicationPath + "Pdf\\1 (2).pdf",
			    AppPath.ApplicationPath + "Pdf\\1 (3).pdf",
			    AppPath.ApplicationPath + "Pdf\\1 (4).pdf",
			    AppPath.ApplicationPath + "Pdf\\1 (5).pdf",
			    AppPath.ApplicationPath + "Pdf\\1 (6).pdf",
			    AppPath.ApplicationPath + "Pdf\\1 (7).pdf",
			    AppPath.ApplicationPath + "Pdf\\1 (8).pdf",
			    AppPath.ApplicationPath + "Pdf\\1 (9).pdf",
			    AppPath.ApplicationPath + "Pdf\\1 (10).pdf"
		    };


		    string outFile = AppPath.ApplicationPath + "Pdf\\Data\\test.pdf";


			report.Merge_Printing(inFiles: files, outFile: outFile);


			//var outputFilePath = report.AccountingReportGrouping(dbFirst, querySql, tempraryStatus: true);

		    return RedirectToAction("Index");
	    }

	}
}