using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanaderiaModels.Entities
{
    public class Stock
    {
        public int StockId { get; set; }
        public int PanId { get; set; }
        public double? Cantidad { get; set; }
        public string? Notas { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? IsActive { get; set; }
    }
}
