using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.BD.Maps
{
    public class PokemnMap : IEntityTypeConfiguration<Pokemn>
    {
       
            public void Configure(EntityTypeBuilder<Pokemn> builder)
            {
                builder.ToTable("Pokemn");
                builder.HasKey(x => x.Id);

            builder.HasOne(o => o.Entrenador)
                .WithMany(o => o.Pokemons)
                .HasForeignKey(o => o.EntrenadorId);
        }
    }
}
