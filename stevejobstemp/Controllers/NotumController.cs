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
    public class NotumController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotumController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Notum
        public async Task<IActionResult> Index()
        {
            ViewBag.materiaList = _context.Materia.Select(c => new SelectListItem { Value = c.MateriaId.ToString(), Text = c.Materia }).ToList();
            ViewBag.gradoList = _context.Grado.Select(c => new SelectListItem { Value = c.GradoId.ToString(), Text = c.Grado }).ToList();

            ViewBag.grado = 1;

            return _context.Nota != null ? 
                          View(await _context.Nota.Where(n => n.Alumno.GradoId == 1).Where(n => n.MateriaId == 1).Include(a => a.Alumno).Include(m => m.Materia).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Nota'  is null.");
            
        }

        public async Task<IActionResult> filtrarNotas(string MateriaSeleccionada, string GradoSeleccionado)
        {
            ViewBag.materiaList = _context.Materia.Select(c => new SelectListItem { Value = c.MateriaId.ToString(), Text = c.Materia }).ToList();
            ViewBag.gradoList = _context.Grado.Select(c => new SelectListItem { Value = c.GradoId.ToString(), Text = c.Grado }).ToList();

            ViewBag.grado = Int64.Parse(GradoSeleccionado);
            return _context.Nota != null ?
                          View(await _context.Nota.Where(n => n.Alumno.GradoId == Int64.Parse(GradoSeleccionado)).Where(n => n.MateriaId == Int64.Parse(MateriaSeleccionada)).Include(n => n.Alumno).Include(n => n.Materia).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Nota'  is null.");
        }

        // GET: Notum/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nota == null)
            {
                return NotFound();
            }

            var notum = await _context.Nota.Include(n => n.Alumno).Include(n => n.Materia)
                .FirstOrDefaultAsync(m => m.NotaId == id);
            if (notum == null)
            {
                return NotFound();
            }

            return View(notum);
        }

        // GET: Notum/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotaId,AlumnoId,MateriaId,CalificacionUno,CalificacionDos,CalificacionTres,CalificacionCuatro,CalificacionTotal")] Notum notum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notum);
        }

        // GET: Notum/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nota == null)
            {
                return NotFound();
            }

            var notum = await _context.Nota.FindAsync(id);
            if (notum == null)
            {
                return NotFound();
            }
            return View(notum);
        }

        // POST: Notum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotaId,AlumnoId,MateriaId,,CalificacionUno,CalificacionDos,CalificacionTres,CalificacionCuatro,CalificacionTotal")] Notum notum)
        {
            if (id != notum.NotaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotumExists(notum.NotaId))
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
            return View(notum);
        }

        // GET: Notum/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nota == null)
            {
                return NotFound();
            }

            var notum = await _context.Nota
                .FirstOrDefaultAsync(m => m.NotaId == id);
            if (notum == null)
            {
                return NotFound();
            }

            return View(notum);
        }

        // POST: Notum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nota == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Nota'  is null.");
            }
            var notum = await _context.Nota.FindAsync(id);
            if (notum != null)
            {
                _context.Nota.Remove(notum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotumExists(int id)
        {
          return (_context.Nota?.Any(e => e.NotaId == id)).GetValueOrDefault();
        }
    }
}
