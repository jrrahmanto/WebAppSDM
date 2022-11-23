using Microsoft.AspNetCore.Mvc;
using WebAppSDM.Data;
using WebAppSDM.Models;
using System.Linq.Dynamic.Core;
using static WebAppSDM.Models.DropdownList;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Drawing.Printing;

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
            IEnumerable<ViewTAbsensi> objCatlist = _context.ViewTAbsensi;
            TempData["date"] = objCatlist.ToList()[0].update_date.ToString("dd MMM yyyy");
            return View(objCatlist);
        }

        //[HttpPost]
        //public ActionResult getList()
        //{
            //try
            //{
            //    int totalRecord = 0;
            //    int filterRecord = 0;
            //    var draw = Request.Form["draw"].FirstOrDefault();
            //    var sortColumn = "update_date";
            //    var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            //    var searchValue = Request.Form["search[value]"].FirstOrDefault();
            //    int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            //    int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
            //    var data = (from a in _context.ViewTAbsensi select a);
            //    //get total count of data in table
            //    totalRecord = data.Count();
            //    // search data when search value found
            //    if (!string.IsNullOrEmpty(searchValue))
            //    {
            //        data = data.Where(x => x.Jam_Masuk.ToLower().Contains(searchValue.ToLower()) || x.Jam_Keluar.ToLower().Contains(searchValue.ToLower()) || x.nama.ToLower().Contains(searchValue.ToLower()) || x.keterangan.ToLower().Contains(searchValue.ToLower()) || x.Status.ToLower().Contains(searchValue.ToLower()));
            //    }
            //    // get total count of records after search
            //    filterRecord = data.Count();
            //    //sort data
            //    if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection)) data = data.OrderBy(sortColumn + " " + sortColumnDirection);
            //    //pagination
            //    var empList = data.Skip(skip).Take(pageSize).ToList();

            //    var jsonData = new { draw = draw, recordsFiltered = filterRecord, recordsTotal = totalRecord, data = empList };
            //    return Ok(jsonData);

            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        //}

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
