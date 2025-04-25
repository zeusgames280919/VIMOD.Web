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
    public class CarroComprasController : Controller
    {
        private readonly VIMODDbContext _context;

        public CarroComprasController(VIMODDbContext context)
        {
            _context = context;
        }

        // GET: CarroCompras
        public async Task<IActionResult> Index()
        {
            var vIMODDbContext = _context.CarritoCompras.Include(c => c.Curso).Include(c => c.Usuario);
            return View(await vIMODDbContext.ToListAsync());
        }

        // GET: CarroCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carroCompras = await _context.CarritoCompras
                .Include(c => c.Curso)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.IdCarroCompras == id);
            if (carroCompras == null)
            {
                return NotFound();
            }

            return View(carroCompras);
        }

        // GET: CarroCompras/Create
        public IActionResult Create()
        {
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "Nombre");
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id");
            return View();
        }

        // POST: CarroCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCarroCompras,Cantidad,UsuarioId,IdCurso")] CarroCompras carroCompras)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carroCompras);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "Nombre", carroCompras.IdCurso);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id", carroCompras.UsuarioId);
            return View(carroCompras);
        }

        // GET: CarroCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carroCompras = await _context.CarritoCompras.FindAsync(id);
            if (carroCompras == null)
            {
                return NotFound();
            }
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "Nombre", carroCompras.IdCurso);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id", carroCompras.UsuarioId);
            return View(carroCompras);
        }

        // POST: CarroCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCarroCompras,Cantidad,UsuarioId,IdCurso")] CarroCompras carroCompras)
        {
            if (id != carroCompras.IdCarroCompras)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carroCompras);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarroComprasExists(carroCompras.IdCarroCompras))
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
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "Nombre", carroCompras.IdCurso);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id", carroCompras.UsuarioId);
            return View(carroCompras);
        }

        // GET: CarroCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carroCompras = await _context.CarritoCompras
                .Include(c => c.Curso)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.IdCarroCompras == id);
            if (carroCompras == null)
            {
                return NotFound();
            }

            return View(carroCompras);
        }

        // POST: CarroCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carroCompras = await _context.CarritoCompras.FindAsync(id);
            if (carroCompras != null)
            {
                _context.CarritoCompras.Remove(carroCompras);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarroComprasExists(int id)
        {
            return _context.CarritoCompras.Any(e => e.IdCarroCompras == id);
        }
    }
}
