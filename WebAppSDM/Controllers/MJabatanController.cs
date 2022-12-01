using Microsoft.AspNetCore.Mvc;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{
    [Models.Authorize]
    public class MJabatanController : Controller
    {
        private readonly ApplicationDBContext _context;
        public MJabatanController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TempData["activeMaster"] = "active";
            IEnumerable<MJabatan> objCatlist = _context.MJabatan.Where(x=> x.isdelete == 0);
            return View(objCatlist);
        }
        public IActionResult Create()
        {
            TempData["activeMaster"] = "active";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MJabatan empobj)
        {
            TempData["activeMaster"] = "active";
            if (ModelState.IsValid)
            {
                _context.MJabatan.Add(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Record Added Successfully !";
                return RedirectToAction("Index");
            }

            return View(empobj);
        }

        public IActionResult Edit(int? id)
        {
            TempData["activeMaster"] = "active";
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.MJabatan.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MJabatan empobj)
        {
            TempData["activeMaster"] = "active";
            if (ModelState.IsValid)
            {
                _context.MJabatan.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Index");
            }

            return View(empobj);
        }

        public IActionResult Delete(int? id)
        {
            TempData["activeMaster"] = "active";
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.MJabatan.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteJabatan(MJabatan empobj)
        {
            TempData["activeMaster"] = "active";
            if (ModelState.IsValid)
            {
                empobj.isdelete = 1;
                _context.MJabatan.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Deleted Successfully !";
                return RedirectToAction("Index");
            }

            return View(empobj);

        }
    }
}
