

using Microsoft.EntityFrameworkCore;

namespace crudLoginReact.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Usuario> Usuario {get;set;}
    }
}
