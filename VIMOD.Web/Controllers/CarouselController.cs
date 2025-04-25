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
    public class CarouselController : Controller
    {
        private readonly VIMODDbContext _context;

        public CarouselController(VIMODDbContext context)
        {
            _context = context;
        }

        // GET: Carousel
        public async Task<IActionResult> Index()
        {
            return View(await _context.Carouseles.ToListAsync());
        }

        // GET: Carousel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carousel = await _context.Carouseles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carousel == null)
            {
                return NotFound();
            }

            return View(carousel);
        }

        // GET: Carousel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carousel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,ImagenUrl")] Carousel carousel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carousel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carousel);
        }

        // GET: Carousel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carousel = await _context.Carouseles.FindAsync(id);
            if (carousel == null)
            {
                return NotFound();
            }
            return View(carousel);
        }

        // POST: Carousel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,ImagenUrl")] Carousel carousel)
        {
            if (id != carousel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carousel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarouselExists(carousel.Id))
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
            return View(carousel);
        }

        // GET: Carousel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carousel = await _context.Carouseles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carousel == null)
            {
                return NotFound();
            }

            return View(carousel);
        }

        // POST: Carousel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carousel = await _context.Carouseles.FindAsync(id);
            if (carousel != null)
            {
                _context.Carouseles.Remove(carousel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarouselExists(int id)
        {
            return _context.Carouseles.Any(e => e.Id == id);
        }
    }
}
