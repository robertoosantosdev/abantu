using abantu.mvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace abantu.mvc.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<Gerente> Gerentes { get; set; }
}
