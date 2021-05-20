using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokemon.Models;

namespace Pokemon.BD.Maps
{
    public class EntrenadorMap: IEntityTypeConfiguration<Entrenador>
    {
        public void Configure(EntityTypeBuilder<Entrenador> builder)
        {

            builder.ToTable("Entrenador");
            builder.HasKey(x => x.Id);

        }
    }
}
