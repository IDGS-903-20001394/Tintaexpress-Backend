using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TintaExpressBackend.Context;
using TintaExpressBackend.Models;

namespace TintaExpressBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_context.usuario.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<usuario> Registrar([FromBody] usuario usr)
        {
            try
            {
                _context.usuario.Add(usr);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public ActionResult Login([FromBody] string email, string pass)
        {
            try
            {
                var usuario = _context.usuario.Single(x => x.email == email);
                if (usuario != null)
                {
                    if(usuario.password == pass)
                    {
                        return Ok(usuario);
                    }
                    else
                    {
                       return Unauthorized("Contraseña incorrecta");
                    }
                }
                else
                {
                    return Unauthorized("Usuario no encontrado");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
