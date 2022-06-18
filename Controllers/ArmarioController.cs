using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeachSys.Models;

namespace BeachSys.Controllers
{
    public class ArmarioController : Controller
    {
        private readonly BeachSysContext _context;

        public ArmarioController(BeachSysContext context)
        {
            _context = context;
        }

        // GET: Armario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Armario.ToListAsync());
        }

        // GET: Armario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armario = await _context.Armario
                .FirstOrDefaultAsync(m => m.ArmarioId == id);
            if (armario == null)
            {
                return NotFound();
            }

            return View(armario);
        }

        // GET: Armario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Armario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArmarioId,Regiao,PontoX,PontoY")] Armario armario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(armario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Compartimento");;
            }
            return View(armario);
        }

        // GET: Armario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armario = await _context.Armario.FindAsync(id);
            if (armario == null)
            {
                return NotFound();
            }
            return RedirectToAction("DisponivelCompartimento", "Compartimento");
        }

        // POST: Armario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArmarioId,Regiao,PontoX,PontoY")] Armario armario)
        {
            if (id != armario.ArmarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(armario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArmarioExists(armario.ArmarioId))
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
            return View(armario);
        }

        // GET: Armario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armario = await _context.Armario
                .FirstOrDefaultAsync(m => m.ArmarioId == id);
            if (armario == null)
            {
                return NotFound();
            }

            return View(armario);
        }
        

        // POST: Armario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var armario = await _context.Armario.FindAsync(id);
            _context.Armario.Remove(armario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArmarioExists(int id)
        {
            return _context.Armario.Any(e => e.ArmarioId == id);
        }
    }
}
