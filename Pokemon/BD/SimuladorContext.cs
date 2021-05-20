
using Microsoft.EntityFrameworkCore;
using Pokemon.BD.Maps;
using Pokemon.Models;

namespace AppCore.BD
{
    public class SimuladorContext : DbContext
    {
        public DbSet<Pokemn> Pokemons{ get; set; }
        public DbSet<Entrenador> Entrenadores{ get; set; }
        public DbSet<UsuarioPokemon> UsuarioPokemons{ get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Server=DESKTOP-6N8H20N; DataBase=T2DIARS;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EntrenadorMap());
            modelBuilder.ApplyConfiguration(new PokemnMap());
            modelBuilder.ApplyConfiguration(new EntrenadorPokemonMap());
        }
    }
}
