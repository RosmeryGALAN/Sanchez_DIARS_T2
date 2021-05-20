using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AppCore.BD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Pokemon.Extensions;
using Pokemon.Models;

namespace Pokemon.Controllers
{
    [Authorize]
    public class PokemonController : Controller
    {
        public SimuladorContext context = new SimuladorContext();

        private IHostingEnvironment env;
        public PokemonController(IHostingEnvironment env)
        {

            this.env = env;
        }

        //public IActionResult Eliminar( int id) {
        //    Pokemn poke= context.Pokemons.Where(x => x.Id == id).First();
        //    context.Pokemons.Remove(poke);
        //    context.SaveChanges();
        //    return RedirectToAction("Capturas");
        //}

        public IActionResult Capturas() {
            var usuario = HttpContext.Session.Get<Entrenador>("sessionUser");

          
            /* var capturas = context.Pokemons.Where(a => a.EntrenadorId == usuario.Id);*/
            var captures = context.UsuarioPokemons.Where(a=>a.IdEntrenador==usuario.Id).ToList();
            
            ViewBag.user = usuario;


            return View(captures);
        }

        
        public IActionResult CapturarPokemon(int IdPokemon)
        {
            var usuario = HttpContext.Session.Get<Entrenador>("sessionUser");
            var a = new UsuarioPokemon();
            a.IdEntrenador = usuario.Id;
            a.IdPokemon = IdPokemon;
            context.Add(a);
            context.SaveChanges();
            return RedirectToAction("Index", "Pokemon", new { id = usuario.Id });
        }
        public IActionResult EliminarPokemon(int IdPokemon)
        {
            var usuario = HttpContext.Session.Get<Entrenador>("sessionUser");
            var a = context.UsuarioPokemons.Where(a=>a.IdPokemon==IdPokemon).FirstOrDefault();
            context.Remove(a);
            context.UsuarioPokemons.Remove(a);
            context.SaveChanges();
            return RedirectToAction("Index", "Pokemon", new { id = usuario.Id });
        }
        public IActionResult Index()
        {
            var usuario = HttpContext.Session.Get<Entrenador>("sessionUser");
            var captures = context.UsuarioPokemons.Where(a => a.IdEntrenador == usuario.Id).ToList();
            ViewBag.pokemones = context.Pokemons.ToList();
            ViewBag.user = usuario.Id;
            return View(captures);

        }
           
            
        
        public ActionResult Buscar(string query)
        {
            var datos = new List<Pokemn>();
            datos = null;
            if (query == null || query == "")
            {
                datos = context.Pokemons.ToList();
            }
            else
            {
                datos = context.Pokemons.Where(o => o.Nombre.Contains(query)).ToList();
            }
            ViewBag.datos = query;
            return View(datos);
        }
        public IActionResult IndexPokemon(string query)
        {
          
            var pokemns= context.Pokemons;
            
                return View(pokemns.ToList());
            

        }
        
        [HttpGet]
        public IActionResult CrearPokemon()
        {
            List<string> tipos = new List<string>();
            tipos.Add("Agua");
            tipos.Add("Fuego");
            tipos.Add("Planta");
            tipos.Add("Electrico");
            tipos.Add("Roca");

            ViewBag.tipos = tipos;
            return View( new Pokemn());
        }
        [HttpPost]
        public IActionResult CrearPokemon(Pokemn pokemn,IFormFile photo)
        {
            List<string> tipos = new List<string>();
            tipos.Add("Agua");
            tipos.Add("Fuego");
            tipos.Add("Planta");
            tipos.Add("Electrico");
            tipos.Add("Roca");

            ViewBag.tipos = tipos;
            validarPokemon(pokemn);
            if (photo==null)
            {
                ModelState.AddModelError("Image", "Ingrese una Imagen");
            }
            if (ModelState.IsValid)  {
                if (photo.Length > 0)
                {
                    var filePath = Path.Combine(env.WebRootPath, "Images", photo.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }
                }
                pokemn.Image = photo.FileName;
                context.Pokemons.Add(pokemn);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pokemn);
        }

        public void validarEntrenador(Entrenador entrenador) {
           
            if (entrenador.Nombre == null || entrenador.Nombre == "")
            {
                ModelState.AddModelError("Nombre", "El campo Nombre es requerido");
            }
            
        }

        public void validarPokemon(Pokemn pokemn)
        {


            if (pokemn.Nombre == null || pokemn.Nombre == "")
            {
                ModelState.AddModelError("Nombre", "El campo Nombre es requerido");
            }
            else {
            
                    var obj = context.Pokemons.Where(a => a.Nombre.Equals(pokemn.Nombre)).FirstOrDefault();


                if (obj!=null)
                {
                    if (pokemn.Nombre == obj.Nombre.ToString())
                    {
                        ModelState.AddModelError("Nombre1", "Ese nombre de Pokemon ya existe");
                    }
                }
            }
            if (pokemn.Tipo== null || pokemn.Tipo == "")
            {
                ModelState.AddModelError("Tipo", "El campo Tipo es requerido");
            }
            

        }
        


    }
}