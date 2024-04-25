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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Avaliacao>()
            .HasOne(e => e.Avaliado)
            .WithMany(e => e.Avaliacoes)
            .HasForeignKey(e => e.IdAvaliado)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Avaliacao>()
            .HasOne(e => e.Avaliador)
            .WithMany()
            .HasForeignKey(e => e.IdAvaliador)
            .HasPrincipalKey(e => e.Id);
    }
}
