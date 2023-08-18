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
                var usr = _context.usuario.FirstOrDefault(u => u.email == email && u.password == pass);
                if (usr == null)
                {
                    return BadRequest("Usuario o contraseña incorrectos");
                }
                return Ok(usr);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Roles")]
        public ActionResult GetRoles()
        {
            try
            {
                return Ok(_context.rol.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
