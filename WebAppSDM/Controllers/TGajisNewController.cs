using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Reporting.WebForms;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{
    [Models.Authorize]
    public class TGajisNewController : Controller
    {
        private readonly ApplicationDBContext _context;

        public TGajisNewController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: TGajisNew
        public async Task<IActionResult> Index()
        {
            TempData["activeTransaksi"] = "active";
            return View(await _context.ViewGaji.ToListAsync());
        }

        // GET: TGajisNew/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TGajis == null)
            {
                return NotFound();
            }

            var tGajis = await _context.TGajis
                .FirstOrDefaultAsync(m => m.id == id);

            var tGajiss = _context.TGajis.Where(m => m.id == id).ToList();

            ViewData["listDetail"] = tGajiss;
            if (tGajis == null)
            {
                return NotFound();
            }
            TempData["activeTransaksi"] = "active";
            return View(tGajis);
        }


        // GET: TGajisNew/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TGajis == null)
            {
                return NotFound();
            }

            var tGajis = await _context.TGajis.FindAsync(id);
            if (tGajis == null)
            {
                return NotFound();
            }
            TempData["activeTransaksi"] = "active";
            return View(tGajis);
        }

        // POST: TGajisNew/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,periode_start,periode_end,gapok,tunj_tetap,tunj_komunikasi,tunj_konjungtur,tunj_harian,tunj_structural,tunj_perumahan,tunj_penyesuaian,tunj_kinerja,tunj_sc,total,createddate,nip,Tunj_lain,pot_koperasi,pot_parkiran,pot_dim,pot_lain,tunj_pph,tunj_jamsostek,tunj_pensiun,tunj_bpjs,pot_jamsostek,pot_bpjs,pot_dplk,jumlah_masuk,jumlah_terlambat,pot_tvs")] TGajis tGajis)
        {
            if (id != tGajis.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tGajis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TGajisExists(tGajis.id))
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
            TempData["activeTransaksi"] = "active";
            return View(tGajis);
        }

        // GET: TGajisNew/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TGajis == null)
            {
                return NotFound();
            }

            var tGajis = await _context.TGajis
                .FirstOrDefaultAsync(m => m.id == id);
            if (tGajis == null)
            {
                return NotFound();
            }
            TempData["activeTransaksi"] = "active";
            return View(tGajis);
        }

        // POST: TGajisNew/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TGajis == null)
            {
                return Problem("Entity set 'ApplicationDBContext.TGajis'  is null.");
            }
            var tGajis = await _context.TGajis.FindAsync(id);
            if (tGajis != null)
            {
                _context.TGajis.Remove(tGajis);
            }
            TempData["activeTransaksi"] = "active";
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TGajisExists(int id)
        {
            return _context.TGajis.Any(e => e.id == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("periodestart,periodeend")] SPGenerateGaji tGaji)
        {
            TempData["activelink"] = "active";

            var loginname = HttpContext.Session.GetString("username_nip");
            try
            {
                IEnumerable<MEmployee> objCatlist = _context.MEmployee.Where(x => x.isdelete == 0 && x.emp_aktif == "t").OrderBy(x=>x.nama).ToList();
                foreach (var item in objCatlist)
                {
                    IEnumerable<TGajis> objGajilist = _context.TGajis.Where(x => x.nip == item.nip && x.periode_start == tGaji.periodestart && x.periode_end == tGaji.periodeend).ToList();

                    if (objGajilist.Count() == 0)
                    {
                        List<SqlParameter> parms = new List<SqlParameter>
                        { 
                            // Create parameters    
                            new SqlParameter { ParameterName = "@nip", Value = item.nip },
                            new SqlParameter { ParameterName = "@periode_start", Value = tGaji.periodestart},
                            new SqlParameter { ParameterName = "@periode_end", Value = tGaji.periodeend },
                            new SqlParameter { ParameterName = "@createby", Value = loginname }
                        };
                        _context.Database.ExecuteSqlRaw("EXEC TGenerateGaji @nip,@periode_start,@periode_end,@createby", parms.ToArray());

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            TempData["activeTransaksi"] = "active";
            return RedirectToAction("Index", "TGajisNew");
        }

        [HttpPost]
        public ActionResult ReportGaji([Bind("periodestart,periodeend")] SPGenerateGaji tGaji)
        {
            string url = "http://182.253.222.68:33677/ReportServer?/SDMInternalReports/Rpt_TGaji/&rs:Command=Render&rs:Format=EXCELOPENXML&rc:OutputFormat=XLS&start_date=" + tGaji.periodestart.ToString("yyyy-MM-dd")+ "&end_date="+tGaji.periodeend.ToString("yyyy-MM-dd") + "";

            System.Net.HttpWebRequest Req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            Req.Credentials = new NetworkCredential(@"PTKBI\admin", @"Jakarta2020");
            Req.Method = "GET";

            System.Net.WebResponse objResponse = Req.GetResponse();
            System.IO.Stream stream = objResponse.GetResponseStream();

            var net = new System.Net.WebClient();
            var content = stream;

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report gaji "+DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }
    }
}
