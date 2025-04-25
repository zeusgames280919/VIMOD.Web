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
    public class PedidoDetalleController : Controller
    {
        private readonly VIMODDbContext _context;

        public PedidoDetalleController(VIMODDbContext context)
        {
            _context = context;
        }

        // GET: PedidoDetalle
        public async Task<IActionResult> Index()
        {
            var vIMODDbContext = _context.PedidoDetalles.Include(p => p.Curso).Include(p => p.Pedido);
            return View(await vIMODDbContext.ToListAsync());
        }

        // GET: PedidoDetalle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoDetalle = await _context.PedidoDetalles
                .Include(p => p.Curso)
                .Include(p => p.Pedido)
                .FirstOrDefaultAsync(m => m.IdDetallePedido == id);
            if (pedidoDetalle == null)
            {
                return NotFound();
            }

            return View(pedidoDetalle);
        }

        // GET: PedidoDetalle/Create
        public IActionResult Create()
        {
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "Nombre");
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "CodigoPostal");
            return View();
        }

        // POST: PedidoDetalle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetallePedido,Cantidad,PrecioIndividual,IdPedido,IdCurso")] PedidoDetalle pedidoDetalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "Nombre", pedidoDetalle.IdCurso);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "CodigoPostal", pedidoDetalle.IdPedido);
            return View(pedidoDetalle);
        }

        // GET: PedidoDetalle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoDetalle = await _context.PedidoDetalles.FindAsync(id);
            if (pedidoDetalle == null)
            {
                return NotFound();
            }
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "Nombre", pedidoDetalle.IdCurso);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "CodigoPostal", pedidoDetalle.IdPedido);
            return View(pedidoDetalle);
        }

        // POST: PedidoDetalle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetallePedido,Cantidad,PrecioIndividual,IdPedido,IdCurso")] PedidoDetalle pedidoDetalle)
        {
            if (id != pedidoDetalle.IdDetallePedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidoDetalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoDetalleExists(pedidoDetalle.IdDetallePedido))
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
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "IdCurso", "Nombre", pedidoDetalle.IdCurso);
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "CodigoPostal", pedidoDetalle.IdPedido);
            return View(pedidoDetalle);
        }

        // GET: PedidoDetalle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoDetalle = await _context.PedidoDetalles
                .Include(p => p.Curso)
                .Include(p => p.Pedido)
                .FirstOrDefaultAsync(m => m.IdDetallePedido == id);
            if (pedidoDetalle == null)
            {
                return NotFound();
            }

            return View(pedidoDetalle);
        }

        // POST: PedidoDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedidoDetalle = await _context.PedidoDetalles.FindAsync(id);
            if (pedidoDetalle != null)
            {
                _context.PedidoDetalles.Remove(pedidoDetalle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoDetalleExists(int id)
        {
            return _context.PedidoDetalles.Any(e => e.IdDetallePedido == id);
        }
    }
}
