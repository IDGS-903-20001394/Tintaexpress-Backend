using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TintaExpressBackend.Context;
using TintaExpressBackend.Models;

namespace TintaExpressBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PedidosController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_context.pedido.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{idUsuario}")]
        public ActionResult GetFromUser(int idUsuario)
        {
            try
            {
                List<pedido> pedidos = _context.pedido.Where(x => x.id_usuario == idUsuario).ToList();
                return Ok(pedidos);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreatePedido([FromBody] pedido pedido)
        {
            try
            {
                _context.pedido.Add(pedido);
                _context.SaveChanges();

                List<carrito> cart = _context.carrito.Where(x => x.id_usuario == pedido.id_usuario).ToList();
                foreach (carrito item in cart)
                {
                    var prodPed = new producto_pedido();
                    prodPed.id_producto = item.id_producto;
                    prodPed.id_pedido = pedido.id;
                    prodPed.cantidad = item.cantidad;
                    prodPed.total = item.total;
                    if (item.imagen != null)
                    {
                        prodPed.imagen = item.imagen;
                    }
                    _context.producto_pedido.Add(prodPed);
                    _context.carrito.Remove(item);
                }
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("AllprodsPed")]
        public ActionResult GetAllProdsPed(int idProd)
        {
            try
            {
                return Ok(_context.producto_pedido.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("prodPed/{idProd}")]
        public ActionResult GetProdsPed(int idProd)
        {
            try
            {
                return Ok(_context.producto_pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult updateEstatus(int id)
        {
            try
            {
                var pedido = _context.pedido.FirstOrDefault(x => x.id == id);
                if (pedido == null)
                {
                    return NotFound();
                }
                if(pedido.estatus == "Pendiente")
                {
                    pedido.estatus = "Procesando";
                    produccion_pedido produccion = new produccion_pedido();
                    produccion.id_pedido = pedido.id;
                    produccion.estatus = "Pendiente";
                    _context.produccion_pedido.Add(produccion);
                }

                else if(pedido.estatus == "Procesado")
                {
                    pedido.estatus = "Enviado";
                }else if(pedido.estatus == "Enviado")
                {
                    pedido.estatus = "Completado";
                }
                _context.pedido.Update(pedido);
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult CancelPedido(int id)
        {
            try
            {
                var pedido = _context.pedido.FirstOrDefault(x => x.id == id);
                if (pedido == null)
                {
                    return NotFound();
                }
                pedido.estatus = "Cancelado";
                _context.pedido.Update(pedido);

                var produccion = _context.produccion_pedido.FirstOrDefault(x => x.id_pedido == id);
                if (produccion != null)
                {
                    produccion.estatus = "Cancelado";
                    _context.produccion_pedido.Update(produccion);
                }

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateProduccion/{id}")]
        public ActionResult UpdateProduccionState(int id)
        {
            try
            {
                var producc = _context.produccion_pedido.FirstOrDefault(x => x.id == id);
                if (producc == null)
                {
                    return NotFound();
                }
                if (producc.estatus == "Pendiente")
                {
                    bool isPosible = true;
                    List<producto_pedido> prodPed = _context.producto_pedido.Where(x => x.id_pedido == producc.id_pedido).ToList();
                    List<producto> productos = _context.producto.ToList();
                    List<materia_prima> materias = _context.materia_prima.ToList();
                    List<materia_producto> materiasProd = _context.materia_producto.ToList();

                    foreach (producto_pedido item in prodPed)
                    {
                        var prod = productos.FirstOrDefault(x => x.id == item.id_producto);
                        if (prod != null)
                        {
                            foreach (materia_producto matProd in materiasProd)
                            {
                                if (matProd.id_producto == prod.id)
                                {
                                    var mat = materias.FirstOrDefault(x => x.id == matProd.id_materia);
                                    if (mat != null)
                                    {
                                        if (mat.inventario < matProd.cantidad * item.cantidad)
                                        {
                                            isPosible = false;
                                        }
                                        else
                                        {
                                            mat.inventario -= matProd.cantidad * item.cantidad;
                                        }
                                    }
                                }
                            }
                        }
                    }   

                    if (isPosible){
                        producc.estatus = "En Proceso";
                        _context.materia_prima.UpdateRange(materias);
                    }
                    else
                    {
                        return BadRequest("No hay suficiente material para producir el pedido");
                    }
                }
                else if (producc.estatus == "En Proceso")
                {
                    producc.estatus = "Terminado";
                    var pedido = _context.pedido.FirstOrDefault(x => x.id == producc.id_pedido);
                    pedido!.estatus = "Procesado";
                    _context.pedido.Update(pedido);
                }
                _context.produccion_pedido.Update(producc);
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
