using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanaderiaModels.Entities
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public DateTime? FechaPedido { get; set; }
        public string? Ruta { get; set; }
        public string? Estado { get; set; }
        public string? Comentarios { get; set; }
        public string? IsActive { get; set; }
        public int SucursalId { get; set; }
    }
}
