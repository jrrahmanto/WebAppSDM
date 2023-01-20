using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using WebAppSDM.Data;
using WebAppSDM.Models;
using static WebAppSDM.Models.Reportparameters;

namespace WebAppSDM.Controllers
{
    [Models.Authorize]
    public class RptRekapAbsen : Controller
    {
        private readonly ApplicationDBContext _context;

        public RptRekapAbsen(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TempData["activeAbsensi"] = "active";
            List<MDivisi> DivisiList = _context.MDivisi.Where(x => x.is_delete == 0).OrderBy(x => x.divisi_name).ToList();
            ViewData["DivisiList"] = DivisiList;

            return View();
        }
        public ActionResult ReportRekapAbsen([Bind("id,periodestart,periodeend")] RptRekapAbsens rptRekapAbsen)
        {
            string url = "http://182.253.222.68:33677/ReportServer?/SDMInternalReports/Rpt_RecapAbsensi&rs:Command=Render&rs:Format=EXCELOPENXML&periodestart=" + rptRekapAbsen.periodestart.ToString("yyyy-MM-dd") + "&periodeend=" + rptRekapAbsen.periodeend.ToString("yyyy-MM-dd") + "&id_divisi=" + rptRekapAbsen.id + "";

            System.Net.HttpWebRequest Req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            Req.Credentials = new NetworkCredential(@"PTKBI\admin", @"Jakarta2020");
            Req.Method = "GET";

            System.Net.WebResponse objResponse = Req.GetResponse();
            System.IO.Stream stream = objResponse.GetResponseStream();

            var net = new System.Net.WebClient();
            var content = stream;

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Rekap Absensi "+rptRekapAbsen.periodestart.ToString("yyyyMMdd") + ".xlsx");
        }
    }
}
