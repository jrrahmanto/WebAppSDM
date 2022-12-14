using Microsoft.AspNetCore.Mvc;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{
    [Models.Authorize]
    public class MMesinAbsenController : Controller
    {
        private readonly ApplicationDBContext _context;
        public MMesinAbsenController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TempData["activeAbsensi"] = "active";
            IEnumerable<ViewMasterAbsen> objCatlist = _context.ViewMasterAbsen;
            return View(objCatlist);
        }
        public IActionResult Create()
        {
            TempData["activeAbsensi"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MMesinAbsen empobj)
        {
            TempData["activeAbsensi"] = "active";
            empobj.isdelete = 0;
            if (ModelState.IsValid)
            {
                _context.MMesinAbsen.Add(empobj);
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
            TempData["activeAbsensi"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.MMesinAbsen.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }

            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MMesinAbsen empobj)
        {
            TempData["activeAbsensi"] = "active";
            if (ModelState.IsValid)
            {
                _context.MMesinAbsen.Update(empobj);
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
            TempData["activeAbsensi"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.MMesinAbsen.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMAbsen(MMesinAbsen empobj)
        {
            TempData["activeAbsensi"] = "active";
            if (ModelState.IsValid)
            {
                empobj.isdelete = 1;
                _context.MMesinAbsen.Update(empobj);
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
