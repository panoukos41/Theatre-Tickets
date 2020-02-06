using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TG.Blazor.IndexedDB;

namespace TheatreTickets.Client.Repos
{
    public abstract class Repo<TEntity> : IRepo<TEntity> where TEntity : class
    {
        private readonly IndexedDBManager _indexedDB;

        public abstract string StoreName { get; }

        protected Repo(IndexedDBManager indexedDB)
        {
            _indexedDB = indexedDB;
        }

        public Task Add(TEntity entity)
            => _indexedDB.AddRecord(new StoreRecord<TEntity> { Data = entity, Storename = StoreName });

        public Task Add(IEnumerable<TEntity> entities)
        {
            var addTasks = new List<Task>();

            foreach (TEntity entity in entities)
            {
                addTasks.Add(Add(entity));
            }

            return Task.WhenAll(addTasks);
        }

        public Task Update(TEntity entity) 
            => _indexedDB.UpdateRecord(new StoreRecord<TEntity> { Data = entity, Storename = StoreName });

        public Task Delete(string id)
            => _indexedDB.DeleteRecord(StoreName, id);

        public Task Delete(IEnumerable<string> ids)
        {
            var deleteTasks = new List<Task>();

            foreach (string id in ids)
            {
                deleteTasks.Add(Delete(id));
            }

            return Task.WhenAll(deleteTasks);
        }

        public Task<TEntity> Get(string id)
            => _indexedDB.GetRecordById<string, TEntity>(StoreName, id);

        public async Task<IEnumerable<TEntity>> Get(IEnumerable<string> ids)
        {
            List<TEntity> entities = new List<TEntity>();
            foreach (var id in ids)
            {
                entities.Add(await Get(id));
            }
            return entities;
        }

        public Task<IEnumerable<TEntity>> Get(params string[] ids) 
            => Get(ids.ToList());

        public async Task<IEnumerable<TEntity>> Get()
        {
            return await _indexedDB.GetRecords<TEntity>(StoreName);
        }
    }
}