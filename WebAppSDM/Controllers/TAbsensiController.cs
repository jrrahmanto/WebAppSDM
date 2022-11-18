using Microsoft.AspNetCore.Mvc;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{
    public class TAbsensiController : Controller
    {
        private readonly ApplicationDBContext _context;
        public TAbsensiController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<ViewTAbsensi> objCatlist = _context.ViewTAbsensi.OrderByDescending(x=>x.update_date).ToList();
            return View(objCatlist);
        }
        public IActionResult Edit(int? id)
        {
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
            DateTime enddate = Convert.ToDateTime(empobj.Jam_Keluar);
            TimeSpan lembur = (empobj.Jam_Keluar - Convert.ToDateTime(enddate.ToString("yyyy-MM-dd 17:00:00"))).Value;
            if (lembur.TotalHours < 1)
            {
                lembur = TimeSpan.Parse("00:00:00");
            }

            empobj.Lembur = lembur;
            if (ModelState.IsValid)
            {
                _context.TAbsensi.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Index");
            }

            return View(empobj);
        }
    }
}
