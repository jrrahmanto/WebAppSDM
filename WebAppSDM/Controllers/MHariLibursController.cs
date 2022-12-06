using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IronXL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{
    public class MHariLibursController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MHariLibursController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: MHariLiburs
        public async Task<IActionResult> Index()
        {
            return View(await _context.MHariLibur.ToListAsync());
        }

        // GET: MHariLiburs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MHariLibur == null)
            {
                return NotFound();
            }

            var mHariLibur = await _context.MHariLibur
                .FirstOrDefaultAsync(m => m.id == id);
            if (mHariLibur == null)
            {
                return NotFound();
            }

            return View(mHariLibur);
        }

        // GET: MHariLiburs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MHariLiburs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,tanggal,keterangan,isdelete")] MHariLibur mHariLibur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mHariLibur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mHariLibur);
        }

        // GET: MHariLiburs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MHariLibur == null)
            {
                return NotFound();
            }

            var mHariLibur = await _context.MHariLibur.FindAsync(id);
            if (mHariLibur == null)
            {
                return NotFound();
            }
            return View(mHariLibur);
        }

        // POST: MHariLiburs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,tanggal,keterangan,isdelete")] MHariLibur mHariLibur)
        {
            if (id != mHariLibur.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mHariLibur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MHariLiburExists(mHariLibur.id))
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
            return View(mHariLibur);
        }

        // GET: MHariLiburs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MHariLibur == null)
            {
                return NotFound();
            }

            var mHariLibur = await _context.MHariLibur
                .FirstOrDefaultAsync(m => m.id == id);
            if (mHariLibur == null)
            {
                return NotFound();
            }

            return View(mHariLibur);
        }

        // POST: MHariLiburs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MHariLibur == null)
            {
                return Problem("Entity set 'ApplicationDBContext.MHariLibur'  is null.");
            }
            var mHariLibur = await _context.MHariLibur.FindAsync(id);
            if (mHariLibur != null)
            {
                _context.MHariLibur.Remove(mHariLibur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MHariLiburExists(int id)
        {
            return _context.MHariLibur.Any(e => e.id == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> upload(IFormFile file)
        {
            string filePath = Path.GetFullPath("wwwroot/dokumen/uploadharilibur.xlsx");
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            IronXL.License.LicenseKey = "IRONXL.PTKLIRINGBERJANGKAINDONESIA.IRO211213.9250.23127.312112-E8D7155B28-DDUBZAO2CK6SZS6-NAGUHRNBVLNI-FJUITVDBUOBQ-3XCIXF7ITTXJ-7W7ND3MR2RG5-K24FCU-LNCDCWWFX2WIEA-PROFESSIONAL.SUB-2GOTUI.RENEW.SUPPORT.13.DEC.2022";
            WorkBook workbook = WorkBook.Load(filePath);
            WorkSheet sheet = workbook.WorkSheets.First();
            string[] data = sheet["A:B"].ToString().Split("\r\n");
            for (int i = 1; i < data.Length; i++)
            {
                var newdata = data[i].Split("\t");
                if (newdata[0] == "")
                {
                    break;
                }
                var mHariLibur = _context.MHariLibur.Where(x => x.tanggal == Convert.ToDateTime(newdata[0])).FirstOrDefault();
                if (mHariLibur == null)
                {
                    var varHariLibur = new MHariLibur();
                    varHariLibur.tanggal = Convert.ToDateTime(newdata[0]);
                    varHariLibur.keterangan = newdata[1];
                    varHariLibur.isdelete = 0;
                    _context.Add(varHariLibur);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
