using TG.Blazor.IndexedDB;
using TheatreTickets.Models;

namespace TheatreTickets.Client.Repos
{
    public class PlayRepo : Repo<Play>, IPlayRepo
    {
        public override string StoreName => StoreNames.Play;

        private readonly IndexedDBManager _indexedDb;

        public PlayRepo(IndexedDBManager indexedDb) : base(indexedDb)
        {
            _indexedDb = indexedDb;
        }
    }
}