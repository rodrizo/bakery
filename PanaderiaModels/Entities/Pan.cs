using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanaderiaModels.Entities
{
    public class Pan
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? PrecioUnitario { get; set; }
        public string? Descripcion { get; set; }
        public string? TiempoPreparacion { get; set; }
        public string? IsActive { get; set; }
    }
}
