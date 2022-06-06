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
    public class NonimaDocenteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NonimaDocenteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NonimaDocente
        public async Task<IActionResult> Index()
        {
            IEnumerable<NonimaDocente> datosNonimaDocente = from nd in _context.NonimaDocente
                                                             join d in _context.Docente on nd.DocenteId equals d.DocenteId
                                                             join m in _context.Materia on nd.MateriaId equals m.MateriaId
                                                             join g in _context.Grado on nd.GradoId equals g.GradoId
                                                             select new NonimaDocente
                                                             {
                                                                 NominaDocenteId = nd.NominaDocenteId,
                                                                 DocenteId = nd.DocenteId,
                                                                 MateriaId = nd.MateriaId,
                                                                 GradoId = nd.GradoId,
                                                                 Nombres = d.Nombres,
                                                                 Apellidos = d.Apellidos,
                                                                 Materia = m.Materia,
                                                                 Grado = g.Grado,

                                                             };
              return _context.NonimaDocente != null ? 
                          View(datosNonimaDocente) :
                          Problem("Entity set 'ApplicationDbContext.NonimaDocente'  is null.");
        }

        // GET: NonimaDocente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NonimaDocente == null)
            {
                return NotFound();
            }

            var nonimaDocente = await _context.NonimaDocente
                .FirstOrDefaultAsync(m => m.NominaDocenteId == id);
            if (nonimaDocente == null)
            {
                return NotFound();
            }

            return View(nonimaDocente);
        }

        // GET: NonimaDocente/Create
        public IActionResult Create()
        {

            ViewBag.docenteList = _context.Docente.Select(c => new SelectListItem { Value = c.DocenteId.ToString(), Text = c.Nombres + " " + c.Apellidos }).ToList();
            ViewBag.materiaList = _context.Materia.Select(c => new SelectListItem { Value = c.MateriaId.ToString(), Text = c.Materia }).ToList();
            ViewBag.gradoList = _context.Grado.Select(c => new SelectListItem { Value = c.GradoId.ToString(), Text = c.Grado }).ToList();

            return View();
        }

        // POST: NonimaDocente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NominaDocenteId,DocenteId,MateriaId,GradoId")] NonimaDocente nonimaDocente)
        {

            if (ModelState.IsValid)
            {
                _context.Add(nonimaDocente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nonimaDocente);
        }

        // GET: NonimaDocente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NonimaDocente == null)
            {
                return NotFound();
            }

            var nonimaDocente = await _context.NonimaDocente.FindAsync(id);
            if (nonimaDocente == null)
            {
                return NotFound();
            }

            ViewBag.docenteList = _context.Docente.Select(c => new SelectListItem { Value = c.DocenteId.ToString(), Text = c.Nombres + " " + c.Apellidos }).ToList();

            ViewBag.materiaList = _context.Materia.Select(c => new SelectListItem { Value = c.MateriaId.ToString(), Text = c.Materia }).ToList();

            ViewBag.gradoList = _context.Grado.Select(c => new SelectListItem { Value = c.GradoId.ToString(), Text = c.Grado }).ToList();

            /*IEnumerable<Docente> datosDocente = from d in _context.Docente
                                                  select d;
            List<SelectListItem> ComboDocentesValores = new List<SelectListItem>();
            foreach (Docente docente in datosDocente)
            {
                SelectListItem miOpcion = new SelectListItem
                {
                    Text = docente.Nombres + " " + docente.Apellidos,
                    Value = docente.DocenteId.ToString()
                };
                ComboDocentesValores.Add(miOpcion);
            }

            ViewBag.ComboDocentesValores = ComboDocentesValores;*/

            return View(nonimaDocente);
        }

        public IEnumerable<NonimaDocente> getReg(int? id)
        {
            IEnumerable<NonimaDocente> datosNonimaDocente = from nd in _context.NonimaDocente
                                                            join d in _context.Docente on nd.DocenteId equals d.DocenteId
                                                            join m in _context.Materia on nd.MateriaId equals m.MateriaId
                                                            join g in _context.Grado on nd.GradoId equals g.GradoId
                                                            where nd.NominaDocenteId == id
                                                            select new NonimaDocente
                                                            {
                                                                NominaDocenteId = nd.NominaDocenteId,
                                                                DocenteId = nd.DocenteId,
                                                                MateriaId = nd.MateriaId,
                                                                GradoId = nd.GradoId,
                                                                Nombres = d.Nombres,
                                                                Apellidos = d.Apellidos,
                                                                Materia = m.Materia,
                                                                Grado = g.Grado,

                                                            };
            return datosNonimaDocente;
        }

        // POST: NonimaDocente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NominaDocenteId,DocenteId,MateriaId,GradoId")] NonimaDocente nonimaDocente)
        {
            if (id != nonimaDocente.NominaDocenteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nonimaDocente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NonimaDocenteExists(nonimaDocente.NominaDocenteId))
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
            return View(nonimaDocente);
        }

        // GET: NonimaDocente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NonimaDocente == null)
            {
                return NotFound();
            }

            var nonimaDocente = await _context.NonimaDocente
                .FirstOrDefaultAsync(m => m.NominaDocenteId == id);
            if (nonimaDocente == null)
            {
                return NotFound();
            }

            return View(nonimaDocente);
        }

        // POST: NonimaDocente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NonimaDocente == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NonimaDocente'  is null.");
            }
            var nonimaDocente = await _context.NonimaDocente.FindAsync(id);
            if (nonimaDocente != null)
            {
                _context.NonimaDocente.Remove(nonimaDocente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NonimaDocenteExists(int id)
        {
          return (_context.NonimaDocente?.Any(e => e.NominaDocenteId == id)).GetValueOrDefault();
        }
    }
}
