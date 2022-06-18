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
    public class CompartimentoController : Controller
    {
        private readonly BeachSysContext _context;

        public CompartimentoController(BeachSysContext context)
        {
            _context = context;
        }

        // GET: Compartimento
        public async Task<IActionResult> Index()
        {
            var beachSysContext = _context.Compartimento.Include(c => c.Armario).Include(c => c.Cadastro);
            return View(await beachSysContext.ToListAsync());
        }

        // GET: Compartimento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compartimento = await _context.Compartimento
                .Include(c => c.Armario)
                .Include(c => c.Cadastro)
                .FirstOrDefaultAsync(m => m.CompartimentoId == id);
            if (compartimento == null)
            {
                return NotFound();
            }

            return View(compartimento);
        }

        // GET: Compartimento/Create
        public IActionResult Create()
        {
            ViewData["ArmarioId"] = new SelectList(_context.Armario, "ArmarioId", "Regiao");
            ViewData["CadastroId"] = new SelectList(_context.Cadastro, "CadastroId", "Nome");
            return View();
        }

        // POST: Compartimento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompartimentoId,Numero,Tamanho,Disponivel,Trancado,CadastroId,ArmarioId,Regiao")] Compartimento compartimento)
        {
            if (ModelState.IsValid)
            {
                
                compartimento.Disponivel = true;
                _context.Add(compartimento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["Armario"] = new SelectList(_context.Armario, "ArmarioId", "Regiao", compartimento.ArmarioId);
            ViewData["ArmarioId"] = new SelectList(_context.Armario, "ArmarioId", "Regiao", compartimento.ArmarioId);
            ViewData["CadastroId"] = new SelectList(_context.Cadastro, "CadastroId", "Nome", compartimento.CadastroId);
            return View(compartimento);
        }

        // GET: Compartimento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compartimento = await _context.Compartimento.FindAsync(id);
            if (compartimento == null)
            {
                return NotFound();
            }
            //ViewData["Armario"] = new SelectList(_context.Armario, "ArmarioId", "Regiao", compartimento.ArmarioId);
            ViewData["ArmarioId"] = new SelectList(_context.Armario, "ArmarioId", "Regiao", compartimento.ArmarioId);
            ViewData["CadastroId"] = new SelectList(_context.Cadastro, "CadastroId", "Nome", compartimento.CadastroId);
            return View(compartimento);
        }

        // POST: Compartimento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompartimentoId,Numero,Tamanho,Disponivel,Trancado,CadastroId,ArmarioId")] Compartimento compartimento)
        {
            if (id != compartimento.CompartimentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compartimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompartimentoExists(compartimento.CompartimentoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("FinalizarCompartimento", "Compartimento");
            }
            ViewData["ArmarioId"] = new SelectList(_context.Armario, "ArmarioId", "Regiao", compartimento.ArmarioId);
            ViewData["CadastroId"] = new SelectList(_context.Cadastro, "CadastroId", "Nome", compartimento.CadastroId);
            return View(compartimento);
        }

        // GET: Compartimento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compartimento = await _context.Compartimento
                .Include(c => c.Armario)
                .Include(c => c.Cadastro)
                .FirstOrDefaultAsync(m => m.CompartimentoId == id);
            if (compartimento == null)
            {
                return NotFound();
            }

            return View(compartimento);
        }

        // POST: Compartimento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compartimento = await _context.Compartimento.FindAsync(id);
            _context.Compartimento.Remove(compartimento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DisponivelCompartimento()
        {
           var compartimentoDisponivelContex = _context.Compartimento.Include(m => m.Cadastro);
           var compartimentoDisponivelContex1 = compartimentoDisponivelContex.Where(m => m.Disponivel == true);
           return View(await compartimentoDisponivelContex1.ToListAsync());    
        }
        public async Task<IActionResult> ConstruirCompartimento(int? id)
       {
            if (id == null)
            {
                return NotFound();
            }
            var compartimento = await _context.Compartimento.FindAsync(id);
            if (compartimento == null)
            {
                return NotFound();
            }
            var compartimentoDisponiveis = _context.Compartimento.Where(m => m.CadastroId == null);
            ViewData["CadastroId"] = new SelectList(_context.Cadastro, "CadastroId", "Nome", compartimento.CadastroId);
            
            //ViewData["Armario"] = new SelectList(_context.Armario, "ArmarioId", "Regiao", compartimento.ArmarioId);
            ViewData["ArmarioId"] = new SelectList(_context.Armario, "ArmarioId", "Regiao", compartimento.ArmarioId);
            return View(compartimento);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConstruirCompartimento(int id, [Bind("CompartimentoId,Numero,Tamanho,Disponivel,Trancado,CadastroId,ArmarioId")] Compartimento compartimento)
        {
            if (id != compartimento.CompartimentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var cadastros = await _context.Cadastro.FindAsync(compartimento.CadastroId);
                    if(cadastros == null)
                    {
                        return NotFound();
                    }
                    
                    compartimento.Disponivel = false;
                    _context.Update(compartimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompartimentoExists(compartimento.CompartimentoId))
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
            ViewData["CadastroId"] = new SelectList(_context.Cadastro, "CadastroId", "Nome", compartimento.CadastroId);
            ViewData["ArmarioId"] = new SelectList(_context.Armario, "ArmarioId", "Regiao", compartimento.ArmarioId);
            
            return View(compartimento);
        }
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConstruirCompartimento(int id, [Bind("CompartimentoId,Numero,Tamanho,Disponivel,Trancado,CadastroId,ArmarioId")] Compartimento compartimento)
        {
            if (id != compartimento.CompartimentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compartimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompartimentoExists(compartimento.CompartimentoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("FinalizarCompartimento", "Compartimento");
            }
            ViewData["ArmarioId"] = new SelectList(_context.Armario, "ArmarioId", "Regiao", compartimento.ArmarioId);
            ViewData["CadastroId"] = new SelectList(_context.Cadastro, "CadastroId", "Nome", compartimento.CadastroId);
            return View(compartimento);
        }*/


        public async Task<IActionResult> FinalizarCompartimento(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compartimento = await _context.Compartimento.FindAsync(id);
            if (compartimento == null)
            {
                return NotFound();
            }

            //ViewData["Armario"] = new SelectList(_context.Armario, "ArmarioId", "Regiao", compartimento.ArmarioId);
            ViewData["ArmarioId"] = new SelectList(_context.Armario, "ArmarioId", "Regiao", compartimento.ArmarioId);
            ViewData["CadastroId"] = new SelectList(_context.Cadastro, "CadastroId", "Nome", compartimento.CadastroId);
            return View(compartimento);
        }
        //Inicio
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizarCompartimento(int id, [Bind("CompartimentoId,Numero,Tamanho,Disponivel,Trancado,CadastroId,ArmarioId")] Compartimento compartimento)
        {
            if (id != compartimento.CompartimentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var cadastros = await _context.Cadastro.FindAsync(compartimento.CadastroId);
                    if(cadastros == null)
                    {
                        return NotFound();
                    }
                    
                    compartimento.Disponivel = true;
                    _context.Update(compartimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompartimentoExists(compartimento.CompartimentoId))
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
            ViewData["CadastroId"] = new SelectList(_context.Cadastro, "CadastroId", "Nome", compartimento.CadastroId);
            ViewData["ArmarioId"] = new SelectList(_context.Armario, "ArmarioId", "Regiao", compartimento.ArmarioId);
            
            return View(compartimento);
        }
        //Final
        
        private bool CompartimentoExists(int id)
        {
            return _context.Compartimento.Any(e => e.CompartimentoId == id);
        }
    }
}
