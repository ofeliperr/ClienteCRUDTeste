using System.Linq;
using System.Threading.Tasks;
using ClienteCRUD.Domain;
using Microsoft.EntityFrameworkCore;

namespace ClienteCRUD.Repository
{
    public class ClienteCRUDRepository : IClienteCRUDRepository
    {
        private ClienteCRUDContext _context { get; }

        public ClienteCRUDRepository(ClienteCRUDContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // GERAIS
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }     

        // CLIENTE  
        public async Task<Cliente[]> GetAllClienteAsync()
        {
            IQueryable<Cliente> query = _context.Clientes;

            query.OrderByDescending(c => c.Nome);

            return await query.ToArrayAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int ClienteId)
        {
            IQueryable<Cliente> query = _context.Clientes;

            query.OrderByDescending(c => c.Nome)
                    .Where(c => c.Id == ClienteId);

            return await query.FirstOrDefaultAsync();
        }
     
    }
}