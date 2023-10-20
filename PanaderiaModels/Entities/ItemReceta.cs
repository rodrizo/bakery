using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanaderiaModels.Entities
{
    public class ItemReceta
    {
        public int Id { get; set; }
        public int RecetaId { get; set; }
        public int IngredienteId { get; set; }
        public string? Descripcion { get; set; }
        public string? Cantidad { get; set; }
        public string? IsActive { get; set; }
    }
}
