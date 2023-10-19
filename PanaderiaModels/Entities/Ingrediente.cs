using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanaderiaModels.Entities
{
    public class Ingrediente
    {
        public int IngredienteId { get; set; }
        public string? Nombre { get; set; }
        public string? Proveedor { get; set; }
        public double? CostoUnitario { get; set; }
        public DateTime? FechaCompra { get; set; }
        public string? IsActive { get; set; }
    }
}
