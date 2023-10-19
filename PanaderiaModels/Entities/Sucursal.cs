using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanaderiaModels.Entities
{
    public class Sucursal
    {
        public int SucursalId { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? NumeroTelefono { get; set; }
        public string? GerenteSucursal { get; set; }
        public string? HorarioOperacion { get; set; }
        public string? IsActive { get; set; }
    }
}
