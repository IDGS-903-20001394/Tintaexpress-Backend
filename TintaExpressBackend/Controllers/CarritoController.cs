using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TintaExpressBackend.Context;
using TintaExpressBackend.Models;

namespace TintaExpressBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CarritoController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_context.carrito.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{idUser}", Name = "Carrito")]
        public ActionResult GetFromUser(int idUser)
        {
            try
            {
                List<carrito> cart = _context.carrito.Where(x => x.id_usuario == idUser).ToList();
                return Ok(cart);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] carrito cart)
        {
            try
            {
                _context.carrito.Add(cart);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            try
            {
                var cart = _context.carrito.FirstOrDefault(x => x.id == id);
                if (cart != null)
                {
                    _context.carrito.Remove(cart);
                    _context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
