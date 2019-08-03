using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Domain.Identity;

namespace ProAgil.Reposirory
{
    public class ProAgilContext : IdentityDbContext<User, Role, int,
                                                    IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                                    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ProAgilContext (DbContextOptions<ProAgilContext> options) : base (options){}

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<RedeSocial> RedeSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(UserRole=>{
               UserRole.HasKey(ur => new {ur.UserId, ur.RoleId});

               UserRole.HasOne(ur => ur.Role)
                .WithMany(r=>r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                UserRole.HasOne(ur => ur.Role)
                .WithMany(r=>r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });
            // especificando que os dois sao chaves
            modelBuilder.Entity<PalestranteEvento>().HasKey(PE => new { PE.EventoId, PE.PalestranteId });
        }
    }
}