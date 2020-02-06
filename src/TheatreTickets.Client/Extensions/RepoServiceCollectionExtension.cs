using Microsoft.Extensions.DependencyInjection;
using TG.Blazor.IndexedDB;
using TheatreTickets.Client.Repos;
using TheatreTickets.Models;

namespace TheatreTickets.Client
{
    public static class RepoServiceCollectionExtension
    {
        public static void UseRepos(this IServiceCollection services)
        {
            services.AddIndexedDB(DbStoreOptions);
            services.AddScoped<ITheatreRepo, TheatreRepo>();
            services.AddScoped<IHallRepo, HallRepo>();
            services.AddScoped<IPlayRepo, PlayRepo>();
            services.AddScoped<IPlayDateTimeRepo, PlayDateTimeRepo>();
            services.AddScoped<ITicketRepo, TicketRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
        }

        private static void DbStoreOptions(DbStore dbStore)
        {
            dbStore.DbName = "theatreTicketsDb";
            dbStore.Version = 1;

            dbStore.Stores.Add(TheaterSchema);
            dbStore.Stores.Add(HallSchema);
            dbStore.Stores.Add(PlaySchema);
            dbStore.Stores.Add(PlayDateTimeSchema);
            dbStore.Stores.Add(TicketSchema);
            dbStore.Stores.Add(UserSchema);
        }

        private static StoreSchema TheaterSchema => NewSchema<Theatre>()
            .SetName(StoreNames.Theatre)
            .SetKey(x => x.Id, false, true)
            .AddSpec(x => x.Name)
            .AddSpec(x => x.Location)
            .AddSpec(x => x.Directions)
            .AddSpec(x => x.Contact)
            .AddSpec(x => x.Info)
            //.AddSpec(x => x.Halls)
            .AddSpec(x => x.HallsIds)
            //.AddSpec(x => x.Plays)
            .AddSpec(x => x.PlaysIds)
            .AddSpec(x => x.ImagesUrls)
            .AddSpec(x => x.Published);

        private static StoreSchema HallSchema => NewSchema<Hall>()
            .SetName(StoreNames.Hall)
            .SetKey(x => x.Id, false, true)
            .AddSpec(x => x.Name)
            //.AddSpec(x => x.Theatre)
            .AddSpec(x => x.TheatreId)
            .AddSpec(x => x.Seats)
            .AddSpec(x => x.ImagesUrls)
            .AddSpec(x => x.Published);

        private static StoreSchema PlaySchema => NewSchema<Play>()
            .SetName(StoreNames.Play)
            .SetKey(x => x.Id, false, true)
            .AddSpec(x => x.Name)
            .AddSpec(x => x.Description)
            .AddSpec(x => x.Duration)
            .AddSpec(x => x.ImagesUrls)
            //.AddSpec(x => x.Theatre)
            .AddSpec(x => x.TheatreId)
            //.AddSpec(x => x.PlayDateTimes)
            .AddSpec(x => x.PlayDateTimesIds)
            .AddSpec(x => x.Published);

        private static StoreSchema PlayDateTimeSchema => NewSchema<PlayDateTime>()
            .SetName(StoreNames.PlayDateTime)
            .SetKey(x => x.Id, false, true)
            //.AddSpec(x => x.Play)
            .AddSpec(x => x.PlayId)
            //.AddSpec(x => x.Hall)
            .AddSpec(x => x.HallId)
            .AddSpec(x => x.DateTime)
            .AddSpec(x => x.SeatsTaken)
            .AddSpec(x => x.Published);

        private static StoreSchema TicketSchema => NewSchema<Ticket>()
            .SetName(StoreNames.Ticket)
            .SetKey(x => x.Id, false, true)
            .AddSpec(x => x.Seat)
            //.AddSpec(x => x.ForDate)
            .AddSpec(x => x.ForDateId);

        private static StoreSchema UserSchema => NewSchema<User>()
            .SetName(StoreNames.User)
            .SetKey(x => x.Id, false, true)
            .AddSpec(x => x.Name)
            .AddSpec(x => x.Email)
            .AddSpec(x => x.UserType)
            //.AddSpec(x => x.PlaysWatched)
            .AddSpec(x => x.PlaysWatchedIds)
            //.AddSpec(x => x.Tickets)
            .AddSpec(x => x.TicketsIds);

        private static StoreSchema<T> NewSchema<T>() where T : class
            => new StoreSchema<T>();
    }
}