using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using APIRES.Models;


namespace APIRES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("myCorsConfig")] // Aquí especifica el nombre de tu directiva CORS

    public class ModulosController : ControllerBase
    {
        public readonly MIAPIContext _dbcontext;

        public ModulosController(MIAPIContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("ListaModulo")]
        public IActionResult ListaModulo()
        {
            try
            {
                var Modulo = _dbcontext.Modulos.Select(r => new
                {
                    r.IdModulo,
                    r.Nombre,
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Petición realizada exitosamente", response = Modulo});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("Obtener/{IdModulo:int}")]
        public IActionResult Obtener(int IdModulo)
        {
               Modulo Modulos = _dbcontext.Modulos.Find(IdModulo);

            if (Modulos == null)
            {
                return BadRequest("Modulos no encontrado");

            }

            try
            {

                var Modulo = _dbcontext.Modulos.Select(r => new
                {
                    r.IdModulo,
                    r.Nombre,
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Petición realizada exitosamente", response = Modulos});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });


            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Modulo objeto)
        {
            try
            {
                var nuevoModulo = new Modulo { Nombre = objeto.Nombre };

                _dbcontext.Modulos.Add(nuevoModulo);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Su Modulo Se Guardo Correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Modulo objeto)
        {
            Modulo Modulos = _dbcontext.Modulos.Find(objeto.IdModulo);

            if (Modulos == null)
            {
                return BadRequest("Modulo no encontrado");
            }

            try
            {
                Modulos.Nombre = objeto.Nombre is null ? Modulos.Nombre : objeto.Nombre;

                _dbcontext.Modulos.Update(Modulos);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Su Modulo Se Actualizo Correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{IdModulo:int}")]
        public IActionResult Eliminar(int IdModulo)
        {

            Modulo Modulos = _dbcontext.Modulos.Find(IdModulo);

            if (Modulos == null)
            {
                return BadRequest("Modulo no encontrado");

            }

            try
            {

                _dbcontext.Modulos.Remove(Modulos);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Su Modulo Se Elimino Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
