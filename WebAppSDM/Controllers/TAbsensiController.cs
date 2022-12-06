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
            IEnumerable<ViewTAbsensi> objCatlist = _context.ViewTAbsensi.Where(x=>x.update_date == DateTime.Now.Date).OrderBy(x=>x.nama);
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x=>x.nama).ToList();
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
            if (empobj.Jam_Keluar != null )
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
                    IEnumerable<ViewTAbsensi> objCatlist = _context.ViewTAbsensi.Where(x => x.update_date >= tAbsen.periodestart && x.update_date <= tAbsen.periodeend).OrderByDescending(x => x.update_date).ThenBy(x=>x.nama).ToList();
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
    }
}
