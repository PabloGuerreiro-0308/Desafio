using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DESAFIO.Models;

namespace DESAFIOO.Controllers
{
    public class NotasController : Controller
    {
        private readonly DesafioContext _context;

        public NotasController(DesafioContext context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            var desafioContext = _context.notas.Include(n => n.estudante);
            return View(await desafioContext.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notas = await _context.notas
                .Include(n => n.estudante)
                .FirstOrDefaultAsync(m => m.codDisciplina == id);
            if (notas == null)
            {
                return NotFound();
            }

            return View(notas);
        }
        
        // GET: Notas/Create
        public IActionResult Create()
        {
            ViewData["matricula"] = new SelectList(_context.estudantes, "matricula", "nome");
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("codDisciplina,nomeDisciplina,av1,av2,av3,media,matricula")] notas notas)
        {   
             // Validação para impedir que o mesmo aluno receba a mesma nota duas vezes.
            if (_context.notas.Any(c => c.matricula == notas.matricula))
            {
                ModelState.AddModelError("matricula.Invalida", $"Este aluno já recebeu uma nota!.");
                
            }      
            
            if (ModelState.IsValid)
            {   notas.media =(notas.av1 + notas.av2 + notas.av3)/3;
                notas.nomeDisciplina = "Matemática";
                _context.Add(notas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["matricula"] = new SelectList(_context.estudantes, "matricula", "nome");
            return View(notas);
        }
        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notas = await _context.notas.FindAsync(id);
            if (notas == null)
            {
                return NotFound();
            }
            ViewData["matricula"] = new SelectList(_context.estudantes, "matricula", "nome", notas.matricula);
            return View(notas);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("codDisciplina,nomeDisciplina,av1,av2,av3,media,matricula")] notas notas)
        {
            if (id != notas.codDisciplina)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!notasExists(notas.codDisciplina))
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
            ViewData["matricula"] = new SelectList(_context.estudantes, "matricula", "nome", notas.matricula);
            return View(notas);
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notas = await _context.notas
                .Include(n => n.estudante)
                .FirstOrDefaultAsync(m => m.codDisciplina == id);
            if (notas == null)
            {
                return NotFound();
            }

            return View(notas);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notas = await _context.notas.FindAsync(id);
            _context.notas.Remove(notas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool notasExists(int id)
        {
            return _context.notas.Any(e => e.codDisciplina == id);
        }
    }
}
