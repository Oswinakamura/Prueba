using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calltech.Models;
using Calltech.Models.Response;
using Calltech.Models.Request;
using Microsoft.AspNetCore.Authorization;

namespace Calltech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta respuesta = new Respuesta();
            respuesta.Exito = 0;
            try { 
            
            respuesta.Exito = 0;
            using (TestContext db = new TestContext())
            {
                var lst = db.Productos.ToList();
                 respuesta.Exito = 1;
                 respuesta.Data = lst;

                
            }
            }catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }

            return Ok(respuesta);

        }

        [HttpPost]
        public IActionResult Add(ProductoRequest productRequest)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                using (TestContext db = new TestContext())
                {
                    Producto producto = new Producto();
                    producto.Producto1 = productRequest.Producto1;
                    producto.Descripcion = productRequest.Descripcion;
                    producto.Valor = productRequest.Valor;
                    db.Productos.Add(producto);
                    db.SaveChanges();

                    respuesta.Exito = 1;

                }

            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }

            return Ok(respuesta);
        }


        [HttpPut]
        public IActionResult Edit(ProductoRequest productRequest)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                using (TestContext db = new TestContext())
                {
                    Producto producto = db.Productos.Find(productRequest.IdProducto);
                    producto.Producto1 = productRequest.Producto1;
                    producto.Descripcion = productRequest.Descripcion;
                    producto.Valor = productRequest.Valor;
                    db.Entry(producto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                    respuesta.Exito = 1;

                }

            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }

            return Ok(respuesta);
        }


        [HttpDelete("{IdProducto}")]
        public IActionResult Delete(int IdProducto)
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                using (TestContext db = new TestContext())
                {
                    Producto producto = db.Productos.Find(IdProducto);
                    db.Remove(producto);
                    db.SaveChanges();

                    respuesta.Exito = 1;

                }

            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }

            return Ok(respuesta);
        }

    }
}
