using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using abantu.mvc.Data;
using abantu.mvc.Models;
using Microsoft.AspNetCore.Authorization;

namespace abantu.mvc.Controllers
{
    [Authorize]
    public class AvaliacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AvaliacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Avaliacoes
        public async Task<IActionResult> Index()
        {
            return _context.Avaliacoes != null ?
                        View(await _context.Avaliacoes.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Avaliacoes'  is null.");
        }

        // GET: Avaliacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Avaliacoes == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // GET: Avaliacoes/Create
        public IActionResult Create()
        {
            var funcionarios = _context.Funcionarios.ToList();
            var gerentes = _context.Gerentes.Select(g => (Funcionario)g).ToList();

            ViewBag.Funcionarios = FuncionariosParaLista(funcionarios);
            ViewBag.Gerentes = FuncionariosParaLista(gerentes);

            return View();
        }

        private List<SelectListItem> FuncionariosParaLista(List<Funcionario> funcionarios)
        {
            List<SelectListItem> itens = new List<SelectListItem>();
            foreach (var funcionario in funcionarios)
            {
                itens.Add(new SelectListItem
                {
                    Text = funcionario.Nome,
                    Value = funcionario.Id.ToString()
                });
            }
            return itens;
        }

        // POST: Avaliacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RealizadaEm,Nota,Comentario")] Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avaliacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avaliacao);
        }

        // GET: Avaliacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Avaliacoes == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao == null)
            {
                return NotFound();
            }
            return View(avaliacao);
        }

        // POST: Avaliacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RealizadaEm,Nota,Comentario")] Avaliacao avaliacao)
        {
            if (id != avaliacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avaliacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvaliacaoExists(avaliacao.Id))
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
            return View(avaliacao);
        }

        // GET: Avaliacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Avaliacoes == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // POST: Avaliacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Avaliacoes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Avaliacoes'  is null.");
            }
            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao != null)
            {
                _context.Avaliacoes.Remove(avaliacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvaliacaoExists(int id)
        {
            return (_context.Avaliacoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
