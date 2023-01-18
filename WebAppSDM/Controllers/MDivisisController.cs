using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{
    [Models.Authorize]
    public class MDivisisController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MDivisisController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: MDivisis
        public async Task<IActionResult> Index()
        {
              return View(await _context.MDivisi.Where(x=>x.is_delete == 0).ToListAsync());
        }

        // GET: MDivisis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MDivisi == null)
            {
                return NotFound();
            }

            var mDivisi = await _context.MDivisi
                .FirstOrDefaultAsync(m => m.id == id);
            if (mDivisi == null)
            {
                return NotFound();
            }

            return View(mDivisi);
        }

        // GET: MDivisis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MDivisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,divisi_name,created_date,is_delete")] MDivisi mDivisi)
        {
            mDivisi.created_date = DateTime.Now;
            mDivisi.is_delete = 0;
            if (ModelState.IsValid)
            {
                _context.Add(mDivisi);
                await _context.SaveChangesAsync();
                TempData["ResultOk"] = "Record Added Successfully !";
                return RedirectToAction(nameof(Index));
            }
            return View(mDivisi);
        }

        // GET: MDivisis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MDivisi == null)
            {
                return NotFound();
            }

            var mDivisi = await _context.MDivisi.FindAsync(id);
            if (mDivisi == null)
            {
                return NotFound();
            }
            return View(mDivisi);
        }

        // POST: MDivisis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,divisi_name,created_date,is_delete")] MDivisi mDivisi)
        {
            if (id != mDivisi.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mDivisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MDivisiExists(mDivisi.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mDivisi);
        }

        // GET: MDivisis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MDivisi == null)
            {
                return NotFound();
            }

            var mDivisi = await _context.MDivisi
                .FirstOrDefaultAsync(m => m.id == id);
            if (mDivisi == null)
            {
                return NotFound();
            }

            return View(mDivisi);
        }

        // POST: MDivisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, [Bind("id,divisi_name,created_date,is_delete")] MDivisi mDivisi)
        {
            mDivisi.is_delete = 1;
            if (id != mDivisi.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mDivisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MDivisiExists(mDivisi.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mDivisi);
        }

        private bool MDivisiExists(int id)
        {
          return _context.MDivisi.Any(e => e.id == id);
        }
    }
}
