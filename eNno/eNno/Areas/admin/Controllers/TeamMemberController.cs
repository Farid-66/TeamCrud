using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eNno.Data;
using eNno.Models;
using System.IO;

namespace eNno.Areas.admin.Controllers
{
    [Area("admin")]
    public class TeamMemberController : Controller
    {
        private readonly AppDbContext _context;

        public TeamMemberController(AppDbContext context)
        {
            _context = context;
        }

        // GET: admin/TeamMember
        public async Task<IActionResult> Index()
        {
            return View(await _context.Members.ToListAsync());
        }

        // GET: admin/TeamMember/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamMember = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamMember == null)
            {
                return NotFound();
            }

            return View(teamMember);
        }

        // GET: admin/TeamMember/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/TeamMember/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamMember teamMember)
        {
            if (ModelState.IsValid)
            {

                if (teamMember.ImageFile != null)
                {
                    string fileName = Guid.NewGuid() + "-" + teamMember.ImageFile.FileName;
                    string filePath = Path.Combine("wwwroot", "Uploads", fileName);

                    using(var stream=new FileStream(filePath,FileMode.Create))
                    {
                        teamMember.ImageFile.CopyTo(stream);
                    }

                    teamMember.Image = fileName;
                    _context.Add(teamMember);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                
                
            }
            return View(teamMember);
        }

        // GET: admin/TeamMember/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamMember = await _context.Members.FindAsync(id);
            if (teamMember == null)
            {
                return NotFound();
            }
            return View(teamMember);
        }

        // POST: admin/TeamMember/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeamMember teamMember)
        {

            if (ModelState.IsValid)
            {
                string oldImagePath = Path.Combine("wwwroot", "Uploads", teamMember.Image);

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                if (teamMember.ImageFile != null)
                {
                    string fileName = Guid.NewGuid() + "-" + teamMember.ImageFile.FileName;
                    string filePath = Path.Combine("wwwroot", "Uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        teamMember.ImageFile.CopyTo(stream);
                    }

                    teamMember.Image = fileName;
                    _context.Update(teamMember);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(teamMember);
        }

        // GET: admin/TeamMember/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamMember = await _context.Members
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamMember == null)
            {
                return NotFound();
            }

            return View(teamMember);
        }

        // POST: admin/TeamMember/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamMember = await _context.Members.FindAsync(id);
            _context.Members.Remove(teamMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamMemberExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }
    }
}
