using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TintaExpressBackend.Context;
using TintaExpressBackend.Models;

namespace TintaExpressBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductoController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_context.producto.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}", Name = "Producto")]
        public ActionResult Get(int id)
        {
            try
            {
                var prod = _context.producto.FirstOrDefault(x => x.id == id);
                return Ok(prod);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<producto> Insert([FromBody] producto prod)
        {
            try
            {
                _context.producto.Add(prod);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] producto prod)
        {
            try
            {
                if (prod.id == id)
                {
                    _context.Entry(prod).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok();
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

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var prod = _context.producto.FirstOrDefault(producto => producto.id == id);

                if (prod != null)
                {
                    prod.Estatus = 0;
                    _context.Entry(prod).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok(id);
                }
                else { return BadRequest(); }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("asignMatProd")]
        public ActionResult<materia_producto> AsignMateriaProduct([FromQuery] int id_Prod, int id_Mat, float cantidad)
        {
            try
            {
               materia_producto matProd = new materia_producto();
               matProd.id_producto = id_Prod;
               matProd.id_materia = id_Mat;
               matProd.cantidad = cantidad;
                
               return Ok(matProd);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("deleteMatProd")]
        public ActionResult DeleteMatProd([FromQuery] int id_Prod, int id_Mat)
        {
            try
            {
                var matProd = _context.materia_producto.FirstOrDefault(materia_producto => materia_producto.id_producto == id_Prod && materia_producto.id_materia == id_Mat);

                if (matProd != null)
                {
                    _context.materia_producto.Remove(matProd);
                    _context.SaveChanges();
                    return Ok(id_Prod);
                }
                else { return BadRequest(); }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getMatProd")]
        public ActionResult GetMatProd([FromQuery] int id_Prod)
        {
            try
            {
                var matProd = _context.materia_producto.Where(materia_producto => materia_producto.id_producto == id_Prod).ToList();

                return Ok(matProd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("newCategoria")]
        public ActionResult InsertCategoria([FromBody] categoria cat)
        {
            try
            {
                _context.categoria.Add(cat);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getCategorias")]
        public ActionResult getCategorias()
        {
            try
            {
                return Ok(_context.categoria.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
