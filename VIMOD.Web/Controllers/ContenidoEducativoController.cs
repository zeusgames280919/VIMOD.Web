using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VIMOD.Domain.Models;
using VIMOD.Infrastructure.Context;

namespace VIMOD.Web.Controllers
{
    public class ContenidoEducativoController : Controller
    {
        private readonly VIMODDbContext _context;

        public ContenidoEducativoController(VIMODDbContext context)
        {
            _context = context;
        }

        // GET: ContenidoEducativo
        public async Task<IActionResult> Index()
        {
            var vIMODDbContext = _context.ContenidosEducativos.Include(c => c.Curso);
            return View(await vIMODDbContext.ToListAsync());
        }

        // GET: ContenidoEducativo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contenidoEducativo = await _context.ContenidosEducativos
                .Include(c => c.Curso)
                .FirstOrDefaultAsync(m => m.ContenidoId == id);
            if (contenidoEducativo == null)
            {
                return NotFound();
            }

            return View(contenidoEducativo);
        }

        // GET: ContenidoEducativo/Create
        public IActionResult Create()
        {
            ViewData["CursoId"] = new SelectList(_context.Cursos, "IdCurso", "Nombre");
            return View();
        }

        // POST: ContenidoEducativo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContenidoId,Titulo,Descripcion,UrlContenido,CursoId,FechaPublicacion,EsActivo")] ContenidoEducativo contenidoEducativo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contenidoEducativo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "IdCurso", "Nombre", contenidoEducativo.CursoId);
            return View(contenidoEducativo);
        }

        // GET: ContenidoEducativo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contenidoEducativo = await _context.ContenidosEducativos.FindAsync(id);
            if (contenidoEducativo == null)
            {
                return NotFound();
            }
            ViewData["CursoId"] = new SelectList(_context.Cursos, "IdCurso", "Nombre", contenidoEducativo.CursoId);
            return View(contenidoEducativo);
        }

        // POST: ContenidoEducativo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContenidoId,Titulo,Descripcion,UrlContenido,CursoId,FechaPublicacion,EsActivo")] ContenidoEducativo contenidoEducativo)
        {
            if (id != contenidoEducativo.ContenidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contenidoEducativo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContenidoEducativoExists(contenidoEducativo.ContenidoId))
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
            ViewData["CursoId"] = new SelectList(_context.Cursos, "IdCurso", "Nombre", contenidoEducativo.CursoId);
            return View(contenidoEducativo);
        }

        // GET: ContenidoEducativo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contenidoEducativo = await _context.ContenidosEducativos
                .Include(c => c.Curso)
                .FirstOrDefaultAsync(m => m.ContenidoId == id);
            if (contenidoEducativo == null)
            {
                return NotFound();
            }

            return View(contenidoEducativo);
        }

        // POST: ContenidoEducativo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contenidoEducativo = await _context.ContenidosEducativos.FindAsync(id);
            if (contenidoEducativo != null)
            {
                _context.ContenidosEducativos.Remove(contenidoEducativo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContenidoEducativoExists(int id)
        {
            return _context.ContenidosEducativos.Any(e => e.ContenidoId == id);
        }
    }
}
