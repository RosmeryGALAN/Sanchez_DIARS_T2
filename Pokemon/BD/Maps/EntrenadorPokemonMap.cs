using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.BD.Maps
{
    public class EntrenadorPokemonMap : IEntityTypeConfiguration<UsuarioPokemon>
    {
       
            public void Configure(EntityTypeBuilder<UsuarioPokemon> builder)
            {
                builder.ToTable("UsuarioPokemon");
                builder.HasKey(x => x.Id);

        }
    }
}
