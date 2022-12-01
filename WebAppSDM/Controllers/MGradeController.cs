using Microsoft.AspNetCore.Mvc;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{
    [Models.Authorize]
    public class MGradeController : Controller
    {
        private readonly ApplicationDBContext _context;
        public MGradeController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TempData["activeMaster"] = "active";
            IEnumerable<MGrade> objCatlist = _context.MGrade.Where(x => x.isdelete == 0); ;
            return View(objCatlist);
        }
        public IActionResult Create()
        {
            TempData["activeMaster"] = "active";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MGrade empobj)
        {
            TempData["activeMaster"] = "active";
            if (ModelState.IsValid)
            {
                _context.MGrade.Add(empobj);
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
            var empfromdb = _context.MGrade.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MGrade empobj)
        {
            TempData["activeMaster"] = "active";
            if (ModelState.IsValid)
            {
                _context.MGrade.Update(empobj);
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
            var empfromdb = _context.MGrade.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteGrade(MGrade empobj)
        {
            TempData["activeMaster"] = "active";
            if (ModelState.IsValid)
            {
                empobj.isdelete = 1;
                _context.MGrade.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Deleted Successfully !";
                return RedirectToAction("Index");
            }

            return View(empobj);

        }
    }
}
