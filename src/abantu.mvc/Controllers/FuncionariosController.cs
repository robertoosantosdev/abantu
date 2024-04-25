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
    public class FuncionariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FuncionariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
            return _context.Funcionarios != null ?
                        View(await _context.Funcionarios.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Funcionarios'  is null.");
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        [Authorize(Roles = "GERENTES")]
        public IActionResult Create()
        {
            var cargos = _context.Cargos.Select(c => new SelectListItem
            {
                Text = c.Nome,
                Value = c.Id.ToString()
            }).ToList();

            ViewBag.Cargos = cargos;

            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GERENTES")]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Ativo,Salario")] Funcionario funcionario, string Cargos)
        {
            // Limpa as validações padrões
            ModelState.Clear();
            // Consulta o cargo no banco de dados
            var cargo = _context.Cargos.Single(c => c.Id == int.Parse(Cargos));
            // Preenche o cargo do funcionário
            funcionario.Cargo = cargo;
            // Atualiza a validação do modelo
            var validacao = TryValidateModel(funcionario);
            // Se for válida, seguimos com o cadastro
            if (validacao)
            {
                var gerente = _context.Gerentes.Single(g => g.Email == User.Identity.Name);
                gerente.Contratar(funcionario);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Cargos = _context.Cargos.Select(c => new SelectListItem
            {
                Text = c.Nome,
                Value = c.Id.ToString()
            }).ToList();

            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Ativo,Salario")] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            var gerente = _context.Gerentes.Single(g => g.Email == User.Identity.Name);
            try
            {
                gerente.AumentarSalario(funcionario, funcionario.Salario);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Funcionarios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Funcionarios'  is null.");
            }
            var funcionario = await _context.Funcionarios.Include(f => f.Avaliacoes).FirstOrDefaultAsync(f => f.Id == id);
            if (funcionario != null)
            {
                var gerente = _context.Gerentes.Single(g => g.Email == User.Identity.Name);
                gerente.Demitir(funcionario);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return (_context.Funcionarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
