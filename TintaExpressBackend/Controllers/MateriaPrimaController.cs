using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TintaExpressBackend.Context;
using TintaExpressBackend.Models;

namespace TintaExpressBackend.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
    public class MateriaPrimaController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MateriaPrimaController(AppDbContext context) 
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_context.materia_prima.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "MateriaPrima")]
        public ActionResult Get(int id)
        {
            try
            {
                var mat = _context.materia_prima.FirstOrDefault(x => x.id == id);
                return Ok(mat);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<materia_prima> Insert([FromBody] materia_prima materia)
        {
            try
            {
                _context.materia_prima.Add(materia);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] materia_prima materia)
        {
            try
            {
                if (materia.id == id)
                {
                    _context.Entry(materia).State = EntityState.Modified;
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
                var materia = _context.materia_prima.FirstOrDefault(materia_prima => materia_prima.id == id);
                List<materia_producto> materias_prod = _context.materia_producto.ToList();
                List<materia_proveedor> materias_prov = _context.materia_proveedor.ToList();

                if (materia != null)
                {
                    _context.materia_prima.Remove(materia);
                    foreach(var mat in materias_prod)
                    {
                        if(mat.id_materia == id)
                        {
                            _context.materia_producto.Remove(mat);
                        }
                    }
                    foreach (var mat in materias_prov)
                    {
                        if (mat.id_materia == id)
                        {
                            _context.materia_proveedor.Remove(mat);
                        }
                    }
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

        [HttpPost("asignMatProv")]
        public ActionResult<materia_proveedor> AsignMateriaProveedor([FromBody] materia_proveedor matProv)
        {
            try
            {
                _context.materia_proveedor.Add(matProv);
                _context.SaveChanges();
                return Ok(matProv);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("deleteMatProv")]
        public ActionResult DeleteMatProv([FromQuery] int id_Prov, int id_Mat)
        {
            try
            {
                var matProv = _context.materia_proveedor.FirstOrDefault(materia_proveedor => materia_proveedor.id_proveedor == id_Prov && materia_proveedor.id_materia == id_Mat);

                if (matProv != null)
                {
                    _context.materia_proveedor.Remove(matProv);
                    _context.SaveChanges();
                    return Ok(id_Prov);
                }
                else { return BadRequest(); }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getMatProv")]
        public ActionResult GetMatProv([FromQuery] int id_Prov)
        {
            try
            {
                var matProv = _context.materia_proveedor.Where(materia_proveedor => materia_proveedor.id_proveedor == id_Prov).ToList();

                return Ok(matProv);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("AllMatProv")]
        public ActionResult GetAllMatProv()
        {
            try
            {
                var matProv = _context.materia_proveedor.ToList();

                return Ok(matProv);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
