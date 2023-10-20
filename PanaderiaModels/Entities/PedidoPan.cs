using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanaderiaModels.Entities
{
    public class PedidoPan
    {
        public int Id { get; set; }
        public int PanId { get; set; }
        public int PedidoId { get; set; }
        public double? Cantidad { get; set; }
        public string? Comentarios { get; set; }
        public string? IsActive { get; set; }
    }
}
