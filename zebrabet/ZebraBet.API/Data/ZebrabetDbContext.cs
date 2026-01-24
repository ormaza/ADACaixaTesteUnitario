using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ZebraBet.API.Models;

namespace ZebraBet.API.Data
{
    public class ZebrabetContext : DbContext
    {
        public ZebrabetContext(DbContextOptions<ZebrabetContext> options) : base(options) { }

        public DbSet<Estado> Estados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<Aposta> Apostas { get; set; }
    }
}
