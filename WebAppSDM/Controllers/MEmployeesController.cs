﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{
    [Models.Authorize]
    public class MEmployeesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MEmployeesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: MEmployees
        public async Task<IActionResult> Index()
        {
            TempData["activeEmployee"] = "active";
            string role = HttpContext.Session.GetString("role");
            if (role == "1")
            {
                return View(await _context.MEmployee.Where(x => x.isdelete == 0).OrderByDescending(x => x.emp_aktif).ThenBy(x => x.nama).ToListAsync());
            }
            else
            {
                string nip = HttpContext.Session.GetString("nip");
                return View(await _context.MEmployee.Where(x => x.nip == Convert.ToInt32(nip)).OrderByDescending(x => x.emp_aktif).ThenBy(x => x.nama).ToListAsync());
            }
        }

        // GET: MEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TempData["activeEmployee"] = "active";
            if (id == null || _context.MEmployee == null)
            {
                return NotFound();
            }

            var mEmployee = await _context.MEmployee.Where(x => x.id == id).ToListAsync();
            List<MEmpFamily> familyList = _context.MEmpFamily.Where(x => x.isdelete == 0 && x.nip == mEmployee[0].nip).ToList();
            ViewData["familyList"] = familyList;
            
            List<MEmpEducations> eduList = _context.MEmpEducations.Where(x => x.nip == mEmployee[0].nip).ToList();
            ViewData["eduList"] = eduList;
            if (mEmployee == null)
            {
                return NotFound();
            }

            return View(mEmployee);
        }

        // GET: MEmployees/Create
        public IActionResult Create()
        {
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            TempData["activeEmployee"] = "active";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile photo, MEmployee mEmployee)
        {
            try
            {
                if (photo != null)
                {
                    string filePath = Path.GetFullPath("wwwroot/photos/" + mEmployee.nip + ".jpg");

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }
                }

                List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
                List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
                ViewData["GradeList"] = gradelist;
                ViewData["JabatanList"] = jabatanlist;
                TempData["activeEmployee"] = "active";
                mEmployee.isdelete = 0;
                if (ModelState.IsValid)
                {
                    _context.Add(mEmployee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(mEmployee);
            }
            catch (Exception x)
            {

                throw;
            }

        }

        // GET: MEmployees/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            TempData["activeEmployee"] = "active";
            if (id == null || _context.MEmployee == null)
            {
                return NotFound();
            }

            var mEmployee = await _context.MEmployee.FindAsync(id);
            if (mEmployee == null)
            {
                return NotFound();
            }
            return View(mEmployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, IFormFile photo, MEmployee mEmployee)
        {
            if (photo != null)
            {
                string filePath = Path.GetFullPath("wwwroot/photos/" + mEmployee.nip + ".jpg");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

            }

            var mEmployeeExist = _context.MEmployee.Where(x => x.id == mEmployee.id).First();
            if (mEmployee.emp_aktif == null)
            {
                mEmployee.emp_aktif = mEmployeeExist.emp_aktif;
            }
            if (mEmployee.salary == null)
            {
                mEmployee.salary = mEmployeeExist.salary;
            }
            if (mEmployee.grade_id == null)
            {
                mEmployee.grade_id = mEmployeeExist.grade_id;
            }
            if (mEmployee.jabatan_id == null)
            {
                mEmployee.jabatan_id = mEmployeeExist.jabatan_id;
            }
            if (mEmployee.emp_status == null)
            {
                mEmployee.emp_status = mEmployeeExist.emp_status;
            }
            TempData["activeEmployee"] = "active";
            List<DropdownList.GradeList> gradelist = _context.GradeList.ToList();
            List<DropdownList.JabatanList> jabatanlist = _context.JabatanList.ToList();
            ViewData["GradeList"] = gradelist;
            ViewData["JabatanList"] = jabatanlist;
            mEmployee.isdelete = 0;
            if (id != mEmployee.id)
            {
                return NotFound();
            }

            try
            {
                mEmployee.emp_photo = mEmployee.nip + ".jpg";
                _context.Entry(mEmployeeExist).CurrentValues.SetValues(mEmployee);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MEmployeeExists(Convert.ToInt32(mEmployee.id)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            //}
            return View(mEmployee);
        }

        // GET: MEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TempData["activeEmployee"] = "active";
            if (id == null || _context.MEmployee == null)
            {
                return NotFound();
            }

            var mEmployee = await _context.MEmployee
                .FirstOrDefaultAsync(m => m.id == id);
            if (mEmployee == null)
            {
                return NotFound();
            }

            return View(mEmployee);
        }

        // POST: MEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            TempData["activeEmployee"] = "active";
            if (_context.MEmployee == null)
            {
                return Problem("Entity set 'ApplicationDBContext.MEmployee'  is null.");
            }
            var mEmployee = await _context.MEmployee.FindAsync(id);
            if (mEmployee != null)
            {
                mEmployee.isdelete = 1;
                _context.Update(mEmployee);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MEmployeeExists(int id)
        {
            TempData["activeEmployee"] = "active";
            return _context.MEmployee.Any(e => e.id == id);
        }
    }
}
