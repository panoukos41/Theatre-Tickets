using TG.Blazor.IndexedDB;
using TheatreTickets.Models;

namespace TheatreTickets.Client.Repos
{
    public class HallRepo : Repo<Hall>, IHallRepo
    {
        public override string StoreName => StoreNames.Hall;

        private readonly IndexedDBManager _indexedDb;

        public HallRepo(IndexedDBManager indexedDb) : base(indexedDb)
        {
            _indexedDb = indexedDb;
        }
    }
}