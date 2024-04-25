using abantu.mvc.Data;
using abantu.mvc.Models;
using Microsoft.AspNetCore.Identity;

namespace abantu.mvc
{
    public class ConfiguraAdministradorSistema
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Cargo _cargoAdmin = new Cargo
        {
            Nome = "SysAdmin",
            Nivel = 2
        };
        private readonly IdentityRole _perfilGerente = new IdentityRole("GERENTES");
        private readonly Gerente _admin = new Gerente(null)
        {
            Nome = "Administrador do Sistema",
            Salario = 0,
            Email = "admin@admin.com.br"
        };

        public ConfiguraAdministradorSistema(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Configurar()
        {
            try
            {
                ConfigurarBancoDeDados();
                await ConfigurarAutenticacao();
            }
            catch (System.Exception exception)
            {
                Console.Write(exception);
            }
        }

        private async Task<bool> ConfigurarAutenticacao()
        {
            IdentityResult usuarioConfigurado;

            var user = new IdentityUser
            {
                UserName = _admin.Email,
                Email = _admin.Email,
                EmailConfirmed = true
            };

            var userAdmin = await _userManager.FindByEmailAsync(user.Email);

            if (userAdmin == null)
            {
                usuarioConfigurado = await _userManager.CreateAsync(user, "Sys@dm1n");
                if (!usuarioConfigurado.Succeeded)
                    return false;
            }
            else
            {
                user = userAdmin;
            }

            var perfilGerentes = await _roleManager.FindByNameAsync(_perfilGerente.Name);

            if (perfilGerentes == null)
            {
                usuarioConfigurado = await _roleManager.CreateAsync(_perfilGerente);
                if (!usuarioConfigurado.Succeeded)
                    return false;
            }

            if (!await _userManager.IsInRoleAsync(user, _perfilGerente.Name))
            {
                usuarioConfigurado = await _userManager.AddToRoleAsync(user, _perfilGerente.Name);
                if (!usuarioConfigurado.Succeeded)
                    return false;
            }
            
            return true;
        }

        private int ConfigurarBancoDeDados()
        {
            var admin = _db.Gerentes.SingleOrDefault(g => g.Email == _admin.Email);
            var quantidadeGerentes = _db.Gerentes.Where(g => g.Ativo).Count();
            if (admin == null)
            {
                var cargoAdmin = _db.Cargos.SingleOrDefault(c => c.Nome == _cargoAdmin.Nome);
                admin = _admin;
                admin.Cargo = cargoAdmin == null ? _cargoAdmin : cargoAdmin;
                admin.Ativo = quantidadeGerentes == 0;
                _db.Add(admin);
            }
            else
            {
                if (quantidadeGerentes > 1)
                {
                    admin.Ativo = false;
                }
                else
                {
                    admin.Ativo = true;
                }
            }
            return _db.SaveChanges();
        }
    }
}