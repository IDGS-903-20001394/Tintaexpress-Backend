using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TintaExpressBackend.Context;
using TintaExpressBackend.Models;

namespace TintaExpressBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvisionController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProvisionController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_context.provision.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}", Name = "Provision")]
        public ActionResult Get(int id)
        {
            try
            {
                var prod = _context.provision.FirstOrDefault(x => x.id == id);
                return Ok(prod);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Insert([FromBody] provision prov)
        {
            try
            {
                _context.provision.Add(prov);
                _context.SaveChanges();
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult UpdateState(int id)
        {
            try
            {
                var prov = _context.provision.FirstOrDefault(x => x.id == id);
                if (prov == null)
                {
                    return NotFound();
                }
                if(prov.estatus == "Pendiente")
                {
                    prov.estatus = "Enviada";
                } else if(prov.estatus == "Enviada")
                {
                    var materia = _context.materia_prima.FirstOrDefault(x => x.id == prov.id_materia);
                    materia.inventario = materia.inventario + prov.cantidad;
                    _context.materia_prima.Update(materia);
                    prov.estatus = "Completada";
                }
                _context.provision.Update(prov);
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)    
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
