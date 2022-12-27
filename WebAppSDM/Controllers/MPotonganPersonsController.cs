using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using IronXL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppSDM.Data;
using WebAppSDM.Models;
using static System.Formats.Asn1.AsnWriter;

namespace WebAppSDM.Controllers
{
    [Models.Authorize]
    public class MPotonganPersonsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MPotonganPersonsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: MPotonganPersons
        public async Task<IActionResult> Index()
        {
            TempData["activePotongan"] = "active";
            return View(await _context.ViewPotonganPerson.ToListAsync());
        }

        // GET: MPotonganPersons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MPotonganPerson == null)
            {
                return NotFound();
            }

            var mPotonganPerson = await _context.MPotonganPerson
                .FirstOrDefaultAsync(m => m.id == id);
            if (mPotonganPerson == null)
            {
                return NotFound();
            }

            return View(mPotonganPerson);
        }

        // GET: MPotonganPersons/Create
        public IActionResult Create()
        {
            TempData["activePotongan"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            return View();
        }

        // POST: MPotonganPersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(bool dplk,IFormCollection form)
        {
            var mPotonganPerson = new MPotonganPerson();
            TempData["activePotongan"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            var MPotonganPerson = _context.MPotonganPerson.Where(x => x.isdelete == 0 && x.periode == form["periode"].ToString() && x.nip == Convert.ToInt32(form["nip"].ToString())).FirstOrDefault();
            if (MPotonganPerson == null)
            {
                var checkparametertvs = _context.MParameter.Where(x => x.parameter_name.ToLower() == form["tvs"].ToString()).FirstOrDefault();
                mPotonganPerson.isdelete = 0;
                mPotonganPerson.nip = Convert.ToInt32(form["nip"].ToString());
                mPotonganPerson.tvs = Convert.ToDecimal(checkparametertvs.value) / 100;
                mPotonganPerson.potongan_parkiran = Convert.ToDecimal(form["potongan_parkiran"].ToString());
                mPotonganPerson.potongan_dim = Convert.ToDecimal(form["potongan_dim"].ToString());
                mPotonganPerson.potongan_lain = Convert.ToDecimal(form["potongan_lain"].ToString());
                mPotonganPerson.dplk = dplk;
                mPotonganPerson.periode = (form["periode"].ToString());
                mPotonganPerson.flag_execute = 0;
                mPotonganPerson.potongan_koperasi = Convert.ToDecimal(form["potongan_koperasi"].ToString());

                if (ModelState.IsValid)
                {
                    _context.Add(mPotonganPerson);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Proses Berhasil !";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                TempData["Message"] = "Data Sudah Ada !";
                return RedirectToAction(nameof(Index));
            }
                
            return View(mPotonganPerson);
        }

        // GET: MPotonganPersons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["activePotongan"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            ViewData["KaryawanList"] = KaryawanList;
          
            if (id == null || _context.MPotonganPerson == null)
            {
                return NotFound();
            }

            var mPotonganPerson = await _context.MPotonganPerson.FindAsync(id);
           
            if (mPotonganPerson == null)
            {
                return NotFound();
            }
            return View(mPotonganPerson);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, bool dplk, IFormCollection form)
        {
            var mPotonganPerson = new MPotonganPerson();
            TempData["activePotongan"] = "active";
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.OrderBy(x => x.nama).ToList();
            ViewData["KaryawanList"] = KaryawanList;
            var checkparametertvs = _context.MParameter.Where(x => x.parameter_name.ToLower() == form["tvs"].ToString()).FirstOrDefault();
            mPotonganPerson.isdelete = 0;
            mPotonganPerson.nip = Convert.ToInt32(form["nip"].ToString());
            mPotonganPerson.tvs = Convert.ToDecimal(checkparametertvs.value) / 100;
            mPotonganPerson.potongan_parkiran = Convert.ToDecimal(form["potongan_parkiran"].ToString());
            mPotonganPerson.potongan_dim = Convert.ToDecimal(form["potongan_dim"].ToString());
            mPotonganPerson.potongan_lain = Convert.ToDecimal(form["potongan_lain"].ToString());
            mPotonganPerson.dplk = dplk;
            mPotonganPerson.periode = (form["periode"].ToString());
            mPotonganPerson.flag_execute = 0;
            mPotonganPerson.potongan_koperasi = Convert.ToDecimal(form["potongan_koperasi"].ToString());

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mPotonganPerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MPotonganPersonExists(mPotonganPerson.id))
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
            return View(mPotonganPerson);
        }

        // GET: MPotonganPersons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MPotonganPerson == null)
            {
                return NotFound();
            }

            var mPotonganPerson = await _context.MPotonganPerson
                .FirstOrDefaultAsync(m => m.id == id);
            if (mPotonganPerson == null)
            {
                return NotFound();
            }

            return View(mPotonganPerson);
        }

        // POST: MPotonganPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MPotonganPerson == null)
            {
                return Problem("Entity set 'ApplicationDBContext.MPotonganPerson'  is null.");
            }
            var mPotonganPerson = await _context.MPotonganPerson.FindAsync(id);
            if (mPotonganPerson != null)
            {
                mPotonganPerson.isdelete = 1;
                _context.Update(mPotonganPerson);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MPotonganPersonExists(int id)
        {
          return _context.MPotonganPerson.Any(e => e.id == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> upload(IFormFile file)
        {
            var msg = "";
            try
            {
                string filePath = Path.GetFullPath("wwwroot/dokumen/uploadpotongan.xlsx");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                IronXL.License.LicenseKey = "IRONXL.PTKLIRINGBERJANGKAINDONESIA.IRO211213.9250.23127.312112-E8D7155B28-DDUBZAO2CK6SZS6-NAGUHRNBVLNI-FJUITVDBUOBQ-3XCIXF7ITTXJ-7W7ND3MR2RG5-K24FCU-LNCDCWWFX2WIEA-PROFESSIONAL.SUB-2GOTUI.RENEW.SUPPORT.13.DEC.2022";
                WorkBook workbook = WorkBook.Load(filePath);
                WorkSheet sheet = workbook.WorkSheets.First();
                string[] data = sheet["A:I"].ToString().Split("\r\n");
                for (int i = 1; i < data.Length; i++)
                {
                    var newdata = data[i].Split("\t");
                    if (newdata[0] == "")
                    {
                        break;
                    }
                    var MPotonganPerson = _context.MPotonganPerson.Where(x => x.isdelete == 0 && x.periode == newdata[8] && x.nip == Convert.ToInt32(newdata[1])).FirstOrDefault();
                    if (MPotonganPerson == null)
                    {
                        var checknip = _context.MEmployee.Where(x => x.nip == Convert.ToInt32(newdata[1])).FirstOrDefault();
                        if (checknip != null)
                        {
                                var varMPotonganPerson = new MPotonganPerson();
                                varMPotonganPerson.nip = Convert.ToInt32(newdata[1]);
                                var checkparametertvs = _context.MParameter.Where(x => x.parameter_name.ToLower() == (newdata[2]).ToLower()).FirstOrDefault();
                                if (checkparametertvs != null)
                                {
                                    varMPotonganPerson.tvs = Convert.ToDecimal(checkparametertvs.value) / 100;
                                }
                                else
                                {
                                    varMPotonganPerson.tvs = 0;
                                }
                                if ((newdata[5]).ToLower() == "mobil")
                                {
                                    var checkparametermobil = _context.MParameter.Where(x => x.parameter_name.ToLower() == "parkir_mobil").FirstOrDefault();
                                    varMPotonganPerson.potongan_parkiran = Convert.ToDecimal(checkparametermobil.value);
                                }
                                else if ((newdata[5]).ToLower() == "motor")
                                {
                                    var checkparametermobil = _context.MParameter.Where(x => x.parameter_name.ToLower() == "parkir_motor").FirstOrDefault();
                                    varMPotonganPerson.potongan_parkiran = Convert.ToDecimal(checkparametermobil.value);

                                }
                                else
                                {
                                    varMPotonganPerson.potongan_parkiran = 0;
                                }
                                varMPotonganPerson.potongan_dim = Convert.ToDecimal(newdata[3]);
                                varMPotonganPerson.potongan_lain = Convert.ToDecimal(newdata[4]);
                                if ((newdata[6]).ToLower() == "ya")
                                {
                                    varMPotonganPerson.dplk = true;
                                }
                                else
                                {
                                    varMPotonganPerson.dplk = false;
                                }
                                varMPotonganPerson.flag_execute = 0;
                                varMPotonganPerson.isdelete = 0;
                                varMPotonganPerson.potongan_koperasi = Convert.ToDecimal(newdata[7]);
                                varMPotonganPerson.periode = (newdata[8]).ToLower();
                                _context.Add(varMPotonganPerson);
                                await _context.SaveChangesAsync();
                        }
                        else
                        {
                            msg = "NIP : " + newdata[1] + " Tidak Ditemukan\n";
                        }
                    }
                }
                msg += "\nProses Selesai";
                TempData["Message"] = msg;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception x)
            {
                msg += x.Message;
                TempData["Message"] = msg;
                return RedirectToAction(nameof(Index));
            }
            
        }
    }
}
