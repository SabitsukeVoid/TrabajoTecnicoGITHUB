using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrabajoTecnico.Models;

namespace TrabajoTecnico.Controllers
{

    public class DetalleProductoController : Controller
    {
        
        private readonly PostgresqlContext _context;

        public DetalleProductoController(PostgresqlContext context)
        {
            _context = context;
        }
        // GET: DetalleProducto
        public async Task<IActionResult> Index()
        {
            var postgresqlContext = _context.DetalleProductos.Include(d => d.IdProductoNavigation);
            return View(await postgresqlContext.ToListAsync());
        }

        // GET: DetalleProducto/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DetalleProductos == null)
            {
                return NotFound();
            }

            var detalleProducto = await _context.DetalleProductos
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleProducto == id);
            if (detalleProducto == null)
            {
                return NotFound();
            }

            return View(detalleProducto);
        }

        // GET: DetalleProducto/Create
        public IActionResult Create()
        {


            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            return View();
        }

        // POST: DetalleProducto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalleProducto,IdProducto,Cantidad,ValorTotal,ValorIva")] DetalleProducto detalleProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleProducto.IdProducto);
            return View(detalleProducto);
        }

        // GET: DetalleProducto/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DetalleProductos == null)
            {
                return NotFound();
            }

            var detalleProducto = await _context.DetalleProductos.FindAsync(id);
            if (detalleProducto == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleProducto.IdProducto);
            return View(detalleProducto);
        }

        // POST: DetalleProducto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdDetalleProducto,IdProducto,Cantidad,ValorTotal,ValorIva")] DetalleProducto detalleProducto)
        {
            if (id != detalleProducto.IdDetalleProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleProductoExists(detalleProducto.IdDetalleProducto))
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
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleProducto.IdProducto);
            return View(detalleProducto);
        }

        // GET: DetalleProducto/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DetalleProductos == null)
            {
                return NotFound();
            }

            var detalleProducto = await _context.DetalleProductos
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleProducto == id);
            if (detalleProducto == null)
            {
                return NotFound();
            }

            return View(detalleProducto);
        }

        // POST: DetalleProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DetalleProductos == null)
            {
                return Problem("Entity set 'PostgresqlContext.DetalleProductos'  is null.");
            }
            var detalleProducto = await _context.DetalleProductos.FindAsync(id);
            if (detalleProducto != null)
            {
                _context.DetalleProductos.Remove(detalleProducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleProductoExists(string id)
        {
          return _context.DetalleProductos.Any(e => e.IdDetalleProducto == id);
        }

        

    }
}
