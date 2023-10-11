using Microsoft.EntityFrameworkCore;

namespace Panaderia.Config
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext (DbContextOptions options) : base(options) { }
    }
}
