using TG.Blazor.IndexedDB;
using TheatreTickets.Models;

namespace TheatreTickets.Client.Repos
{
    public class TheatreRepo : Repo<Theatre>, ITheatreRepo
    {
        public override string StoreName => StoreNames.Theatre;

        private readonly IndexedDBManager _indexedDb;

        public TheatreRepo(IndexedDBManager indexedDb) : base(indexedDb)
        {
            _indexedDb = indexedDb;
        }
    }
}