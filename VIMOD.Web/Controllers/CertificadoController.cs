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
    public class CertificadoController : Controller
    {
        private readonly VIMODDbContext _context;

        public CertificadoController(VIMODDbContext context)
        {
            _context = context;
        }

        // GET: Certificado
        public async Task<IActionResult> Index()
        {
            return View(await _context.Certificados.ToListAsync());
        }

        // GET: Certificado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificado = await _context.Certificados
                .FirstOrDefaultAsync(m => m.IdCertificado == id);
            if (certificado == null)
            {
                return NotFound();
            }

            return View(certificado);
        }

        // GET: Certificado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Certificado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCertificado,FechaEmision,RutaArchivo,IdCurso,IdUsuario")] Certificado certificado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(certificado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(certificado);
        }

        // GET: Certificado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificado = await _context.Certificados.FindAsync(id);
            if (certificado == null)
            {
                return NotFound();
            }
            return View(certificado);
        }

        // POST: Certificado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCertificado,FechaEmision,RutaArchivo,IdCurso,IdUsuario")] Certificado certificado)
        {
            if (id != certificado.IdCertificado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certificado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificadoExists(certificado.IdCertificado))
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
            return View(certificado);
        }

        // GET: Certificado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificado = await _context.Certificados
                .FirstOrDefaultAsync(m => m.IdCertificado == id);
            if (certificado == null)
            {
                return NotFound();
            }

            return View(certificado);
        }

        // POST: Certificado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certificado = await _context.Certificados.FindAsync(id);
            if (certificado != null)
            {
                _context.Certificados.Remove(certificado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificadoExists(int id)
        {
            return _context.Certificados.Any(e => e.IdCertificado == id);
        }
    }
}
