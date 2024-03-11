using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.Marshalling;

using Microsoft.EntityFrameworkCore;
using APIRES.Models;


namespace APIRES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelacionesController : ControllerBase
    {

        public readonly MIAPIContext _dbcontext;

        public RelacionesController(MIAPIContext _context)
        {
            _dbcontext = _context;
            {
            }
        }

        [HttpGet]
        [Route("ListaRelacione")]
        public IActionResult ListaRelacione()
        {
            List<Relacione> lista = new List<Relacione>();

            try
            {
                lista = _dbcontext.Relaciones
                    .Include(c => c.IdRols)
                    .Include(c=> c.IdModulos)
                    .ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });

            }
        }


        [HttpGet]
        [Route("Obtener/{IdRelacione:int}")]
        public IActionResult Obtener(int IdRelacione)
        {
            Relacione Relaciones = _dbcontext.Relaciones.Find(IdRelacione);

            if (Relaciones == null)
            {
                return BadRequest("Rol no encontrado");

            }

            try
            {

                var Relacione = _dbcontext.Relaciones.Select(r => new
                {
                    r.IdRelaciones,
                    r.IdModulo,
                    r.IdRol
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Petición realizada exitosamente", response = Relacione });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });


            }
        }

        [HttpPost]
        [Route("GuardarRelaciones")]
        public IActionResult GuardarRelaciones(int IdModolo, int idRol)
        {
            try
            {
                var nuevoRelacione = new Relacione
                {
                    IdRol = idRol,
                    IdModulo = IdModolo
                };

                _dbcontext.Relaciones.Add(nuevoRelacione);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Su Rls Se Guardó Correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarRelaciones")]
        public IActionResult EditarRelaciones(int IdRol,int IdModulo)
        {
            try
            {

                var RelacionesExistente = _dbcontext.Relaciones.FirstOrDefault(u => u.IdRelaciones == u.IdRelaciones);
                if (RelacionesExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Relacion no encontrado" });
                }


                RelacionesExistente.IdRol = IdRol;
                RelacionesExistente.IdModulo = IdModulo;
                


                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Su Relacion Se Actualizó Correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }



    }

    }

