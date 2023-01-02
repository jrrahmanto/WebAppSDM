using Microsoft.AspNetCore.Mvc;
using WebAppSDM.Data;
using WebAppSDM.Models;
using System.Linq.Dynamic.Core;
using static WebAppSDM.Models.DropdownList;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Drawing.Printing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace WebAppSDM.Controllers
{
    [Models.Authorize]
    public class TAbsensiController : Controller
    {
        private readonly ApplicationDBContext _context;
        public TAbsensiController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TempData["activeAbsensi"] = "active";
            IEnumerable<ViewTAbsensi> objCatlist = _context.ViewTAbsensi.Where(x => x.update_date == DateTime.Now.Date).OrderBy(x => x.nama);
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            return View(objCatlist);
        }
        public IActionResult Edit(int? id)
        {
            TempData["activeAbsensi"] = "active";
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.TAbsensi.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TAbsensi empobj)
        {
            TempData["activeAbsensi"] = "active";
            if (empobj.Jam_Keluar != null)
            {
                DateTime enddate = Convert.ToDateTime(empobj.Jam_Keluar);
                TimeSpan lembur = (empobj.Jam_Keluar - Convert.ToDateTime(enddate.ToString("yyyy-MM-dd 17:00:00"))).Value;
                if (lembur.TotalHours < 1)
                {
                    lembur = TimeSpan.Parse("00:00:00");
                }

                empobj.Lembur = lembur;
            }

            if (ModelState.IsValid)
            {
                _context.TAbsensi.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Index");
            }

            return View(empobj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nip,periodestart,periodeend")] filterAbsensi tAbsen)
        {
            TempData["activeAbsensi"] = "active";
            try
            {
                List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
                ViewData["KaryawanList"] = KaryawanList;
                if (tAbsen.nip == "-")
                {
                    IEnumerable<ViewTAbsensi> objCatlist = _context.ViewTAbsensi.Where(x => x.update_date >= tAbsen.periodestart && x.update_date <= tAbsen.periodeend).OrderByDescending(x => x.update_date).ThenBy(x => x.nama).ToList();
                    return View("Index", objCatlist);
                }
                else
                {
                    IEnumerable<ViewTAbsensi> objCatlist = _context.ViewTAbsensi.Where(x => x.NIP == tAbsen.nip && x.update_date >= tAbsen.periodestart && x.update_date <= tAbsen.periodeend).OrderByDescending(x => x.update_date).ThenBy(x => x.nama).ToList();
                    return View("Index", objCatlist);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult ReportAbsen([Bind("nip,periodestart,periodeend")] filterAbsensi tAbsen)
        {
            try
            {
                //string url = "http://182.253.222.68:33677/ReportServer?/DailyReport/rptReportBalanceDetail&rs:Command=Render&rs:Format=EXCELOPENXML";

                //string periodStart = DateTime.ParseExact(BusinessDateStart, "MM/dd/yyyy", provider).ToString("yyyy-MM-dd");
                //string periodFinish = DateTime.ParseExact(BusinessDateEnd, "MM/dd/yyyy", provider).ToString("yyyy-MM-dd");

                //url = url + "&startperiod=" + periodStart + "&endperiod=" + periodFinish;

                //System.Net.HttpWebRequest Req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                //Req.Credentials = new NetworkCredential(@"PTKBI\admin", @"Jakarta2020");
                //Req.Method = "GET";

                //string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

                //System.Net.WebResponse objResponse = Req.GetResponse();
                //System.IO.Stream stream = objResponse.GetResponseStream();

                //var net = new System.Net.WebClient();
                //var content = stream;
                //var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //var fileName = timestamp + @".xlsx";
                //return File(content, contentType, fileName);


                string url = "http://182.253.222.68:33677/ReportServer?/SDMInternalReports/Rpt_TAbsensi&rs:Command=Render&rs:Format=EXCELOPENXML&periodestart=" + tAbsen.periodestart.ToString("yyyy-MM-dd") + "&periodeend=" + tAbsen.periodeend.ToString("yyyy-MM-dd") + "&nama=" + tAbsen.nip + "";

                System.Net.HttpWebRequest Req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                Req.Credentials = new NetworkCredential(@"PTKBI\admin", @"Jakarta2020");
                Req.Method = "GET";

                System.Net.WebResponse objResponse = Req.GetResponse();
                System.IO.Stream stream = objResponse.GetResponseStream();


                //string path = Path.GetFullPath("wwwroot/dokumen/reportabsen.xlsx");

                //System.Net.WebResponse objResponse = Req.GetResponse();
                //System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create);
                //System.IO.Stream stream = objResponse.GetResponseStream();

                //byte[] buf = new byte[1024];
                //int len = stream.Read(buf, 0, 1024);

                //while (len > 0)
                //{
                //    fs.WriteAsync(buf, 0, len);
                //    len = stream.Read(buf, 0, 1024);
                //}
                //stream.Close();
                //fs.Close();

                //byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                //System.IO.File.Delete(path);

                var net = new System.Net.WebClient();
                var content = stream;

                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", tAbsen.periodestart.ToString("yyyyMMdd") + ".xlsx");

            }
            catch (Exception x)
            {
                TempData["ResultOk"] = "EROR ! "+x.Message;
                return View("Index");
            }
        }

    }
}
