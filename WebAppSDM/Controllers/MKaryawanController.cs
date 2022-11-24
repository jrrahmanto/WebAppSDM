using Microsoft.AspNetCore.Mvc;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{
    [Models.Authorize]
    public class MKaryawanController : Controller
    {
        private readonly ApplicationDBContext _context;
        public MKaryawanController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<ViewKaryawan> objCatlist = _context.ViewKaryawan.Where(x=>x.isdelete == 0).OrderByDescending(y=>y.isdelete).OrderBy(x => x.Nama);
            return View(objCatlist);
        }
        public IActionResult Create()
        {
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MKaryawan empobj)
        {
            empobj.create_date = DateTime.Now;
            empobj.Tgl_Permanen = Convert.ToDateTime("2000-01-01");
            if (ModelState.IsValid)
            {
                _context.MKaryawan.Add(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Record Added Successfully !";
                return RedirectToAction("Index");
            }
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            return View(empobj);
        }

        public IActionResult Edit(int? id)
        {
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.MKaryawan.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MKaryawan empobj)
        {
            empobj.create_date = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.MKaryawan.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Index");
            }
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            return View(empobj);
        }

        public IActionResult Delete(int? id)
        {
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.MKaryawan.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }

            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteKaryawan(MKaryawan empobj)
        {
            empobj.create_date = DateTime.Now;
            if (ModelState.IsValid)
            {
                empobj.isdelete = 1;
                _context.MKaryawan.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Deleted Successfully !";
                return RedirectToAction("Index");
            }
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            return View(empobj);

        }
    }
}
