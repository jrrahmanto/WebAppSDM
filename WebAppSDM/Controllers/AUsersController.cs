using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using WebAppSDM.Data;
using WebAppSDM.Models;

namespace WebAppSDM.Controllers
{

    [Models.Authorize]
    public class AUsersController : Controller
    {
        private readonly ApplicationDBContext _context;

        public AUsersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: AUsers
        public async Task<IActionResult> Index()
        {
              return View(await _context.AUser.OrderBy(x=>x.isactive).ToListAsync());
        }

        // GET: AUsers/Create
        public IActionResult Create()
        {
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;

            List<ARole> RoleList = _context.ARole.Where(x => x.isdelete == 0).ToList();
            ViewData["RoleList"] = RoleList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,username,password,email,role_id,isactive,nip")] AUser aUser)
        {
            try
            {
                var pwdori = aUser.password;
                var pwd = Login.Encrypt(aUser.password);
                List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
                ViewData["KaryawanList"] = KaryawanList;

                List<ARole> RoleList = _context.ARole.Where(x => x.isdelete == 0).ToList();
                ViewData["RoleList"] = RoleList;

                var existing = _context.AUser.Where(x => x.username == aUser.username).FirstOrDefault();
                if (existing == null)
                {
                    if (ModelState.IsValid)
                    {
                        aUser.password = pwd;
                        _context.Add(aUser);
                        await _context.SaveChangesAsync();
                        string text = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\html\\TemplateEmail.cshtml"));
                        text = text.Replace("#body#", "Email ini di daftarkan untuk account website SDM, berikut adalah akun untuk login ke website SDM\nhttps://sdm-ptkbi.azurewebsites.net/,");
                        text = text.Replace("#username#", aUser.username);
                        text = text.Replace("#password#", pwdori);
                        text = text.Replace("#name#", aUser.username);

                        SendEmail.sendEmail(aUser.email, "Pembuatan Account SDM Webapp", text);
                        TempData["ResultOk"] = "Record Added Successfully !";
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    TempData["ResultOk"] = "Username sudah digunakan , silahkan gunakan username lain !";
                }
            }
            catch (Exception x)
            {
                TempData["ResultOk"] = x;
            }
            return View(aUser);
        }

        // GET: AUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
            ViewData["KaryawanList"] = KaryawanList;

            List<ARole> RoleList = _context.ARole.Where(x=>x.isdelete == 0).ToList();
            ViewData["RoleList"] = RoleList;

            if (id == null || _context.AUser == null)
            {
                return NotFound();
            }

            var aUser = await _context.AUser.FindAsync(id);
            if (aUser == null)
            {
                return NotFound();
            }
            return View(aUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,username,password,email,role_id,isactive,nip")] AUser aUser)
        {
            try
            {
                List<DropdownList.KaryawanList> KaryawanList = _context.KaryawanLists.ToList();
                ViewData["KaryawanList"] = KaryawanList;

                List<ARole> RoleList = _context.ARole.Where(x => x.isdelete == 0).ToList();
                ViewData["RoleList"] = RoleList;

                var pwdori = aUser.password;
                var existing = _context.AUser.Where(x => x.username == aUser.username && x.id != aUser.id).FirstOrDefault();
                if (existing == null)
                {
                    aUser.password = Login.Encrypt(aUser.password);
                    if (id != aUser.id)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(aUser);
                            await _context.SaveChangesAsync();

                            string text = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\html\\TemplateEmail.cshtml"));
                            text = text.Replace("#body#", "Email ini di daftarkan untuk account website SDM, berikut adalah akun untuk login ke website SDM,");
                            text = text.Replace("#username#", aUser.username);
                            text = text.Replace("#password#", pwdori);
                            text = text.Replace("#name#", aUser.username);

                            SendEmail.sendEmail(aUser.email, "Update Account SDM Webapp", text);

                            TempData["ResultOk"] = "Record Update Successfully !";
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!AUserExists(aUser.id))
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
                }
                else
                {
                    TempData["ResultOk"] = "Username sudah digunakan , silahkan gunakan username lain !";
                }
            }
            catch (Exception x)
            {
                TempData["ResultOk"] = x;
            }
          
           
            return View(aUser);
        }

        // GET: AUsers/Delete/5
        
        private bool AUserExists(int id)
        {
          return _context.AUser.Any(e => e.id == id);
        }
    }
}
