using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calltech.Models.Request
{
    public class ProductoRequest
    {
        public int IdProducto { get; set; }
        public string Producto1 { get; set; }
        public string Descripcion { get; set; }
        public int Valor { get; set; }
    }
}
