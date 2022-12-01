using Microsoft.AspNetCore.Mvc;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{
    [Models.Authorize]
    public class TKoperasiController : Controller
    {
        private readonly ApplicationDBContext _context;
        public TKoperasiController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TempData["activelink"] = "active";
            IEnumerable<TKoperasi> objCatlist = _context.TKoperasi.Where(x => x.isdelete == 0); ;
            return View(objCatlist);
        }
        public IActionResult Create()
        {
            TempData["activelink"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TKoperasi empobj)
        {
            TempData["activelink"] = "active";
            if (ModelState.IsValid)
            {
                empobj.update_date = DateTime.Now;
                _context.TKoperasi.Add(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Record Added Successfully !";
                return RedirectToAction("Index");
            }
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;
            return View(empobj);
        }
        public IActionResult Edit(int? id)
        {
            TempData["activelink"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.TKoperasi.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TKoperasi empobj)
        {
            TempData["activelink"] = "active";
            if (ModelState.IsValid)
            {
                _context.TKoperasi.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Index");
            }
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;
            return View(empobj);
        }
        public IActionResult Delete(int? id)
        {
            TempData["activelink"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.TKoperasi.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteKoperasi(TKoperasi empobj)
        {
            TempData["activelink"] = "active";
            if (ModelState.IsValid)
            {
                empobj.isdelete = 1;
                _context.TKoperasi.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Deleted Successfully !";
                return RedirectToAction("Index");
            }
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;
            return View(empobj);

        }
    }
}
