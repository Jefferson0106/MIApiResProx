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
    public class UsuariosController : ControllerBase
    {
        public readonly MIAPIContext _dbcontext;

        public UsuariosController(MIAPIContext context) {
            _dbcontext = context;
        }


        [HttpGet]
        [Route("ListaUsuario")]
        public IActionResult ListaUsuario()
        {
            try
            {
                var Usuario = _dbcontext.Usuarios.Select(r => new
                {
                    r.IdUsuario,
                    r.Nombre,
                    r.Apellido,
                    r.Correo,
                    r.Contrasena,
                    r.Cargo,
                    r.Telefono,
                    r.IdRol,
                }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Petición realizada exitosamente Kevin diaz bloqueo a tu contacto ayer 20:00pm rosi amiga de reveca  en bahenhf servidor jeffer.belico ", response = Usuario });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        [Route("Obtener/{IdUsuario:int}")]
        public IActionResult Obtener(int IdUsuario)
        {
            Usuario Usuarios = _dbcontext.Usuarios.Find(IdUsuario);

            if (Usuarios == null)
            {
                return BadRequest("Usuario no encontrado");

            }

            try
            {

                var Usuario = _dbcontext.Usuarios.Select(r => new
                {
                    r.IdUsuario,
                    r.Nombre,
                    r.Apellido,
                    r.Correo,
                    r.Contrasena,
                    r.Cargo,
                    r.Telefono,
                    r.IdRol,
                }).ToList();

                return StatusCode(StatusCodes.Status200OK,  new  { mensaje = "Petición realizada exitosamente", response = Usuarios });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });


            }
        }

        [HttpPost]
        [Route("GuardarUsuario")]
        public IActionResult GuardarUsuario(string nombre, string apellido, string correo, string contrasena, string cargo, string telefono, int idRol)
        {
            try
            {
                var nuevoUsuario = new Usuario
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Correo = correo,
                    Contrasena = contrasena,
                    Cargo = cargo,
                    Telefono = telefono,
                    IdRol = idRol
                };

                _dbcontext.Usuarios.Add(nuevoUsuario);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Su Usuario Se Guardó Correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
        [HttpPut]
        [Route("EditarUsuario")]
        public IActionResult EditarUsuario(int IdUsuario, string nombre, string apellido, string correo, string contrasena, string cargo, string telefono, int idRol)
        {
            try
            {
               
                var usuarioExistente = _dbcontext.Usuarios.FirstOrDefault(u => u.IdUsuario == IdUsuario);
                if (usuarioExistente == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "Usuario no encontrado" });
                }

               
                usuarioExistente.Nombre = nombre;
                usuarioExistente.Apellido = apellido;
                usuarioExistente.Correo = correo;
                usuarioExistente.Contrasena = contrasena;
                usuarioExistente.Cargo = cargo;
                usuarioExistente.Telefono = telefono;
                usuarioExistente.IdRol = idRol;

                
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Su Usuario Se Actualizó Correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{IdUsuario:int}")]
        public IActionResult Eliminar(int IdUsuario)
        {

            Usuario Usuarios = _dbcontext.Usuarios.Find(IdUsuario);

            if (Usuarios == null)
            {
                return BadRequest("Usuario no encontrado");

            }

            try
            {

                _dbcontext.Usuarios.Remove(Usuarios);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Su usuario Se Elimino Exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}

