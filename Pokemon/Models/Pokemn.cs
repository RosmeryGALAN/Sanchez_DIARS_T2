using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.Models
{
    public class Pokemn
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Image{ get; set; }
        public Entrenador Entrenador{ get; set; }
        public int EntrenadorId { get; set; }
    }
}
