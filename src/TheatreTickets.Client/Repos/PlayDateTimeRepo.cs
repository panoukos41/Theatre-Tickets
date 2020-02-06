using TG.Blazor.IndexedDB;
using TheatreTickets.Models;

namespace TheatreTickets.Client.Repos
{
    public class PlayDateTimeRepo : Repo<PlayDateTime>, IPlayDateTimeRepo
    {
        public override string StoreName => StoreNames.PlayDateTime;

        private readonly IndexedDBManager _indexedDb;

        public PlayDateTimeRepo(IndexedDBManager indexedDb) : base(indexedDb)
        {
            _indexedDb = indexedDb;
        }
    }
}