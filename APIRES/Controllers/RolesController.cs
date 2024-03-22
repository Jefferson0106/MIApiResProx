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
    public class RolesController : ControllerBase
    {
        public readonly MIAPIContext _dbcontext;

        public RolesController(MIAPIContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            try
            {
                var Rol = _dbcontext.Rols.Select(r => new
                {
                    r.IdRol,
                    r.Nombre,
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Petición realizada exitosamente", response = Rol });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("Obtener/{IdRol:int}")]
        public IActionResult Obtener(int IdRol)
        {
            Rol Rols = _dbcontext.Rols.Find(IdRol);

            if (Rols == null)
            {
                return BadRequest("Rol no encontrado");

            }

            try
            {

                var Rol = _dbcontext.Rols.Select(r => new
                {
                    r.IdRol,
                    r.Nombre,
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Petición realizada exitosamente", response = Rols });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });


            }
        }
        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Rol objeto)
        {
            try
            {
                var nuevoRol = new Rol { Nombre = objeto.Nombre };

                _dbcontext.Rols.Add(nuevoRol);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { savedRole = nuevoRol, mensaje = "Su Rol Se Guardo" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Rol objeto)
        {
            Rol Rols = _dbcontext.Rols.Find(objeto.IdRol);

            if (Rols == null)
            {
                return BadRequest("Rol no encontrado");
            }

            try
            {
                Rols.Nombre = objeto.Nombre is null ? Rols.Nombre : objeto.Nombre;

                _dbcontext.Rols.Update(Rols);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Su Rol Se Actualizo Correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{IdRol:int}")]
        public IActionResult Eliminar(int IdRol)
        {

            Rol Rols = _dbcontext.Rols.Find(IdRol);

            if (Rols == null)
            {
                return BadRequest("Rol no encontrado");

            }

            try
            {

                _dbcontext.Rols.Remove(Rols);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Su Rol Se Elimino Exitosamente" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}

