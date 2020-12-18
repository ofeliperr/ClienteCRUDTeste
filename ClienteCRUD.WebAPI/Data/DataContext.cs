using ClienteCRUD.WebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ClienteCRUD.WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}   
        public DbSet<Cliente> Clientes {get; set; }
    }
}