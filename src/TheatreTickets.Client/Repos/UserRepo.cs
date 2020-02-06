using TG.Blazor.IndexedDB;
using TheatreTickets.Models;

namespace TheatreTickets.Client.Repos
{
    public class UserRepo : Repo<User>, IUserRepo
    {
        public override string StoreName => StoreNames.User;

        private readonly IndexedDBManager _indexedDb;

        public UserRepo(IndexedDBManager indexedDb) : base(indexedDb)
        {
            _indexedDb = indexedDb;
        }
    }
}