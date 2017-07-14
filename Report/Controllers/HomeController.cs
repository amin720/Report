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

		    var querySql = @"SELECT * FROM [DecaFinancial].[dbo].[VW_AccountingDocumentPrint] WHERE AccountingDocumentId = 3012";

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

		    var querySql = @"SELECT * FROM [DecaFinancial].[dbo].[VW_AccountingDocumentPrint] ORDER BY OrganizationTitle";

		    var outputFilePath = report.AccountingReportGrouping(dbFirst, querySql,tempraryStatus:true);

		    return Redirect(outputFilePath);
	    }

	}
}