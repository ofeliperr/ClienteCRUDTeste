using System.Threading.Tasks;
using ClienteCRUD.Domain;

namespace ClienteCRUD.Repository
{
    public interface IClienteCRUDRepository
    {
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChangesAsync();
         
         // Clientes
         Task<Cliente[]> GetAllClienteAsync();
         Task<Cliente> GetClienteByIdAsync(int ClienteId);
    }
}