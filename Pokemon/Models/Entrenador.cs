using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.Models
{
    public class Entrenador
    {
        public int Id { get; set; }
        public string Nombre{ get; set; }
        public string UserName{ get; set; }
        public string Pass{ get; set; }
        //public string Ciudad{ get; set; }
        //public string UserName { get; set; }
        //public string Pass { get; set; }

        public List<Pokemn> Pokemons { get; set; }

    }
}
