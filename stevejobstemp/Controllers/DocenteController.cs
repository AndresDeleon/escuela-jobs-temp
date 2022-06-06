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
    public class DocenteController : Controller
    {
        private readonly ApplicationDbContext _context;


        public DocenteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Docente
        public async Task<IActionResult> Index()
        {
              return _context.Docente != null ? 
                          View(await _context.Docente.Include(g => g.Grado).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Docente'  is null.");
        }

        // GET: Docente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Docente == null)
            {
                return NotFound();
            }

            var docente = await _context.Docente
                .FirstOrDefaultAsync(m => m.DocenteId == id);
            if (docente == null)
            {
                return NotFound();
            }
            ViewBag.gradoList = _context.Grado.Select(c => new SelectListItem { Value = c.GradoId.ToString(), Text = c.Grado }).ToList();
            return View(docente);
        }

        // GET: Docente/Create
        public IActionResult Create()
        {
            ViewBag.gradoList = _context.Grado.Select(c => new SelectListItem { Value = c.GradoId.ToString(), Text = c.Grado }).ToList();
            return View();
        }

        // POST: Docente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocenteId,Nombres,Apellidos,Sexo,FecNac,GradoId")] Docente docente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(docente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(docente);
        }

        // GET: Docente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Docente == null)
            {
                return NotFound();
            }

            var docente = await _context.Docente.FindAsync(id);
            if (docente == null)
            {
                return NotFound();
            }
            ViewBag.gradoList = _context.Grado.Select(c => new SelectListItem { Value = c.GradoId.ToString(), Text = c.Grado }).ToList();
            return View(docente);
        }

        // POST: Docente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocenteId,Nombres,Apellidos,Sexo,FecNac,GradoId")] Docente docente)
        {
            if (id != docente.DocenteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(docente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocenteExists(docente.DocenteId))
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
            return View(docente);
        }

        // GET: Docente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Docente == null)
            {
                return NotFound();
            }

            var docente = await _context.Docente
                .FirstOrDefaultAsync(m => m.DocenteId == id);
            if (docente == null)
            {
                return NotFound();
            }

            return View(docente);
        }

        // POST: Docente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Docente == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Docente'  is null.");
            }
            var docente = await _context.Docente.FindAsync(id);
            if (docente != null)
            {
                _context.Docente.Remove(docente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocenteExists(int id)
        {
          return (_context.Docente?.Any(e => e.DocenteId == id)).GetValueOrDefault();
        }
    }
}
