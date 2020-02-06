using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheatreTickets.Client.Repos
{
    public interface IRepo<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);

        Task Add(IEnumerable<TEntity> entities);

        Task Update(TEntity entity);

        Task Delete(string id);

        Task Delete(IEnumerable<string> ids);

        Task<TEntity> Get(string id);

        Task<IEnumerable<TEntity>> Get(IEnumerable<string> ids);

        Task<IEnumerable<TEntity>> Get(params string[] ids);

        Task<IEnumerable<TEntity>> Get();

        
    }
}