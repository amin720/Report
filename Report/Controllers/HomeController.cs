using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Report.Pdf.SampleReport.Group;
using Report.Pdf.SampleReport.Headers_Footers;
using Report.Pdf.SampleReport.InlineHeader;

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
	    public ActionResult Group()
	    {
		    var rpt = new Group().CreatePdfReport();
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
	}
}