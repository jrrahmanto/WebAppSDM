using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{
    [Models.Authorize]
    public class MTunjanganController : Controller
    {
        private readonly ApplicationDBContext _context;
        public MTunjanganController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TempData["activeTunjangan"] = "active";
            IEnumerable<ViewTunjangan> objCatlist = _context.ViewTunjangan;
            return View(objCatlist);
        }
        public IActionResult Create()
        {
            TempData["activeTunjangan"] = "active";
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            List<MListTunjangan> TunjanganList = _context.MListTunjangan.Where(x => x.keterangan == "all" && x.isdelete == 0).ToList();
            ViewData["TunjanganList"] = TunjanganList;
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MTunjangan empobj)
        {
            TempData["activeTunjangan"] = "active";
           
            if (ModelState.IsValid)
            {
                empobj.create_date = DateTime.Now;
                _context.MTunjangan.Add(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Record Added Successfully !";
                return RedirectToAction("Index");
            }

            return View(empobj);
        }
        public IActionResult Edit(int? id)
        {
            TempData["activeTunjangan"] = "active";
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            List<MListTunjangan> TunjanganList = _context.MListTunjangan.Where(x => x.keterangan == "all" && x.isdelete == 0).ToList();
            ViewData["TunjanganList"] = TunjanganList;
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.MTunjangan.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MTunjangan empobj)
        {
            TempData["activeTunjangan"] = "active";
            if (ModelState.IsValid)
            {
                _context.MTunjangan.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Index");
            }

            return View(empobj);
        }
        public IActionResult Delete(int? id)
        {
            TempData["activeTunjangan"] = "active";
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            List<MListTunjangan> TunjanganList = _context.MListTunjangan.Where(x => x.keterangan == "all" && x.isdelete == 0).ToList();
            ViewData["TunjanganList"] = TunjanganList;
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.MTunjangan.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTunjangan(MTunjangan empobj)
        {
            TempData["activeTunjangan"] = "active";
            if (ModelState.IsValid)
            {
                empobj.isdelete = 1;
                _context.MTunjangan.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Deleted Successfully !";
                return RedirectToAction("Index");
            }

            return View(empobj);

        }
    }
}
