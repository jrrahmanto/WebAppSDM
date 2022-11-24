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
    public class MParametersController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MParametersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: MParameters
        public async Task<IActionResult> Index()
        {
              return View(await _context.MParameter.ToListAsync());
        }

        // GET: MParameters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MParameters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,value,keterangan")] MParameter mParameter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mParameter);
                await _context.SaveChangesAsync();
                TempData["ResultOk"] = "Record Added Successfully !";
                return RedirectToAction(nameof(Index));
            }
            return View(mParameter);
        }

        // GET: MParameters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MParameter == null)
            {
                return NotFound();
            }

            var mParameter = await _context.MParameter.FindAsync(id);
            if (mParameter == null)
            {
                return NotFound();
            }
            return View(mParameter);
        }

        // POST: MParameters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,value,keterangan")] MParameter mParameter)
        {
            if (id != mParameter.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mParameter);
                  
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MParameterExists(mParameter.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["ResultOk"] = "Record Update Successfully !";
                return RedirectToAction(nameof(Index));
            }
            return View(mParameter);
        }

        // GET: MParameters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MParameter == null)
            {
                return NotFound();
            }

            var mParameter = await _context.MParameter
                .FirstOrDefaultAsync(m => m.id == id);
            if (mParameter == null)
            {
                return NotFound();
            }

            return View(mParameter);
        }

        // POST: MParameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MParameter == null)
            {
                return Problem("Entity set 'ApplicationDBContext.MParameter'  is null.");
            }
            var mParameter = await _context.MParameter.FindAsync(id);
            if (mParameter != null)
            {
                _context.MParameter.Remove(mParameter);
            }
            TempData["ResultOk"] = "Record Delete Successfully !";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MParameterExists(int id)
        {
          return _context.MParameter.Any(e => e.id == id);
        }
    }
}
