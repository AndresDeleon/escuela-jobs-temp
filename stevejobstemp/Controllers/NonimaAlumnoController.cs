using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using stevejobstemp.Data;
using stevejobstemp.Models;

namespace stevejobstemp.Controllers
{
    public class NonimaAlumnoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NonimaAlumnoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NonimaAlumno
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.NonimaAlumno.Include(n => n.Alumno).Include(n => n.Grado);
            ViewBag.gradoList = _context.Grado.Select(c => new SelectListItem { Value = c.GradoId.ToString(), Text = c.Grado }).ToList();
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> filtrarGrado(string GradoSeleccionado)
        {
            var applicationDbContext = _context.NonimaAlumno.Where(c => c.GradoId == Int64.Parse(GradoSeleccionado)).Include(n => n.Alumno).Include(n => n.Grado);
            ViewBag.gradoList = _context.Grado.Select(c => new SelectListItem { Value = c.GradoId.ToString(), Text = c.Grado }).ToList();
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: NonimaAlumno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NonimaAlumno == null)
            {
                return NotFound();
            }

            var nonimaAlumno = await _context.NonimaAlumno
                .Include(n => n.Alumno)
                .Include(n => n.Grado)
                .FirstOrDefaultAsync(m => m.NominaAlumnoId == id);
            if (nonimaAlumno == null)
            {
                return NotFound();
            }

            return View(nonimaAlumno);
        }

        // GET: NonimaAlumno/Create
        public IActionResult Create()
        {
            ViewData["AlumnoId"] = new SelectList(_context.Alumno, "AlumnoId", "AlumnoId");
            ViewData["GradoId"] = new SelectList(_context.Grado, "GradoId", "GradoId");

            ViewBag.alumnoList = _context.Alumno.Select(c => new SelectListItem { Value = c.AlumnoId.ToString(), Text = c.Nombres + " " + c.Apellidos }).ToList();
            ViewBag.gradoList = _context.Grado.Select(c => new SelectListItem { Value = c.GradoId.ToString(), Text = c.Grado }).ToList();
            return View();
        }

        // POST: NonimaAlumno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NominaAlumnoId,AlumnoId,GradoId")] NonimaAlumno nonimaAlumno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nonimaAlumno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlumnoId"] = new SelectList(_context.Alumno, "AlumnoId", "AlumnoId", nonimaAlumno.AlumnoId);
            ViewData["GradoId"] = new SelectList(_context.Grado, "GradoId", "GradoId", nonimaAlumno.GradoId);
            return View(nonimaAlumno);
        }

        // GET: NonimaAlumno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NonimaAlumno == null)
            {
                return NotFound();
            }

            var nonimaAlumno = await _context.NonimaAlumno.FindAsync(id);
            if (nonimaAlumno == null)
            {
                return NotFound();
            }
            ViewData["AlumnoId"] = new SelectList(_context.Alumno, "AlumnoId", "AlumnoId", nonimaAlumno.AlumnoId);
            ViewData["GradoId"] = new SelectList(_context.Grado, "GradoId", "GradoId", nonimaAlumno.GradoId);
            ViewBag.alumnoList = _context.Alumno.Select(c => new SelectListItem { Value = c.AlumnoId.ToString(), Text = c.Nombres + " " + c.Apellidos }).ToList();
            ViewBag.gradoList = _context.Grado.Select(c => new SelectListItem { Value = c.GradoId.ToString(), Text = c.Grado }).ToList();

            return View(nonimaAlumno);
        }

        // POST: NonimaAlumno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NominaAlumnoId,AlumnoId,GradoId")] NonimaAlumno nonimaAlumno)
        {
            if (id != nonimaAlumno.NominaAlumnoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nonimaAlumno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NonimaAlumnoExists(nonimaAlumno.NominaAlumnoId))
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
            ViewData["AlumnoId"] = new SelectList(_context.Alumno, "AlumnoId", "AlumnoId", nonimaAlumno.AlumnoId);
            ViewData["GradoId"] = new SelectList(_context.Grado, "GradoId", "GradoId", nonimaAlumno.GradoId);
            return View(nonimaAlumno);
        }

        // GET: NonimaAlumno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NonimaAlumno == null)
            {
                return NotFound();
            }

            var nonimaAlumno = await _context.NonimaAlumno
                .Include(n => n.Alumno)
                .Include(n => n.Grado)
                .FirstOrDefaultAsync(m => m.NominaAlumnoId == id);
            if (nonimaAlumno == null)
            {
                return NotFound();
            }

            return View(nonimaAlumno);
        }

        // POST: NonimaAlumno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NonimaAlumno == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NonimaAlumno'  is null.");
            }
            var nonimaAlumno = await _context.NonimaAlumno.FindAsync(id);
            if (nonimaAlumno != null)
            {
                _context.NonimaAlumno.Remove(nonimaAlumno);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NonimaAlumnoExists(int id)
        {
          return (_context.NonimaAlumno?.Any(e => e.NominaAlumnoId == id)).GetValueOrDefault();
        }
    }
}
