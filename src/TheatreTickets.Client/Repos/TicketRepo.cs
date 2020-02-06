using TG.Blazor.IndexedDB;
using TheatreTickets.Models;

namespace TheatreTickets.Client.Repos
{
    public class TicketRepo : Repo<Ticket>, ITicketRepo
    {
        public override string StoreName => StoreNames.Ticket;

        private readonly IndexedDBManager _indexedDb;

        public TicketRepo(IndexedDBManager indexedDb) : base(indexedDb)
        {
            _indexedDb = indexedDb;
        }
    }
}