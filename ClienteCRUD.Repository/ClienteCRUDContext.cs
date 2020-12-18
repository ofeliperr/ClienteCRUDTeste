using Microsoft.EntityFrameworkCore;
using ClienteCRUD.Domain;

namespace ClienteCRUD.Repository
{
    public class ClienteCRUDContext : DbContext
    {
        public ClienteCRUDContext(DbContextOptions<ClienteCRUDContext> options) : base (options) {}   
        public DbSet<Cliente> Clientes {get; set; }
        
    }
}