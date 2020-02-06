using Blazored.LocalStorage;
using LoremNET;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using MiniGuids;
using System;
using System.Collections.Generic;
using System.Linq;
using TheatreTickets.Client.Repos;
using TheatreTickets.Models;

#pragma warning disable IDE1006 // Naming Styles

namespace TheatreTickets.Client.Utilities
{
    public static class SampleData
    {
        private static IServiceProvider ServiceProvider;

        private static IJSRuntime JSRuntime;

        public static void AddSampleData(this IServiceProvider services)
        {
            ISyncLocalStorageService localStorage = services.GetService<ISyncLocalStorageService>();
            if (localStorage.HasDemoDataKey())
                return;

            ServiceProvider = services;
            ShowAlert();

            ConsoleLog("- Starting to generate demo data! -");

            List<Theatre> theatres = createTheaters();
            List<Hall> halls = new List<Hall>();
            List<Play> plays = new List<Play>();
            List<PlayDateTime> playDateTimes;
            List<Ticket> tickets;
            List<User> users = new List<User>();

            ConsoleLog("Generated Theatres:", theatres);

            foreach (var theatre in theatres)
            {
                halls.AddRange(createHalls(theatre, 4));
                plays.AddRange(createPlays(theatre, 4));
            }
            ConsoleLog("Generated Halls:", halls);
            ConsoleLog("Generated Plays:", plays);

            playDateTimes = createPlayDateTimes(halls, plays);
            ConsoleLog("Generated PlayDateTimes:", playDateTimes);

            tickets = createTickets(playDateTimes, halls);
            ConsoleLog("Generated Tickets:", tickets);

            users.Add(createUser(Lorem.Words(2), Lorem.Email(), UserType.user));
            ConsoleLog("Generated Users:", users);

            ConsoleLog("Writting to DB!");

            WriteToDb(theatres, halls, plays, playDateTimes, tickets, users);
            localStorage.AddDemoDataKey();

            ConsoleLog("- Finished!! -");
        }

        private static List<Theatre> createTheaters()
        {
            var theatres = new List<Theatre>();

            theatres.Add(new Theatre
            {
                Id = "0",
                Name = "Theatre of Cosmos!",
                Location = new Location("St. SIGGROU FIX", 37.962870, 23.725762),
                Directions = $"We are near ST.SIGGROU FIX metro station, just follow kalirois avenue to the National Art Museum!",
                Contact = "You can contact us at \"+30 210 2340 242\" or at \"randomemail@email.com\"",
                Info = Lorem.Paragraph(5, 5),
                // Halls
                HallsIds = new List<string>(),
                // Plays
                PlaysIds = new List<string>(),
                ImagesUrls = new string[]
                {
                    "https://picsum.photos/id/20/1920/1440",
                    "https://picsum.photos/id/21/1920/1440",
                    "https://picsum.photos/id/22/1920/1440",
                    "https://picsum.photos/id/23/1920/1440"
                },
                Published = true
            });

            return theatres;
        }

        private static List<Hall> createHalls(Theatre theatre, int num)
        {
            var halls = new List<Hall>();

            var seats = new List<Seat>(100);
            for (int r = 1; r <= 5; r++)
            {
                for (int n = 1; n <= 20; n++)
                {
                    Seat seat = new Seat
                    {
                        Row = r,
                        Number = n
                    };
                    seats.Add(seat);
                }
            }

            int imageSeed = 50;
            for (int i = 0; i < num; i++)
            {
                var images = GetImages(imageSeed, 5);
                imageSeed += 10;

                int id = ((i + 3) ^ 3);
                halls.Add(new Hall
                {
                    Id = id.ToString(),
                    Name = Number2String(id),
                    TheatreId = theatre.Id,
                    Seats = seats,
                    ImagesUrls = images,
                    Published = true
                });

                theatre.HallsIds.Add(id.ToString());
            }
            return halls;
        }

        private static List<Play> createPlays(Theatre theatre, int num)
        {
            var plays = new List<Play>();
            int imageSeed = 160;
            for (int i = 0; i < num; i++)
            {
                string id = ((i + 7) ^ 7).ToString();
                plays.Add(new Play
                {
                    Id = id,
                    Name = Lorem.Words(1, 4),
                    Description = Lorem.Paragraph(2, 4),
                    Duration = new Random().Next(1, 3),
                    ImagesUrls = GetImages(imageSeed, 4),
                    TheatreId = theatre.Id,
                    //PlayDateTimes
                    PlayDateTimesIds = new List<string>(),
                    Published = true
                });
                theatre.PlaysIds.Add(id);
                imageSeed += 20;
            }

            return plays;
        }

        private static List<PlayDateTime> createPlayDateTimes(IEnumerable<Hall> halls, IEnumerable<Play> plays)
        {
            var playDateTimes = new List<PlayDateTime>();

            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(now.Year, now.Month, now.Day, 21, 0, 0);
            DateTime endDate = startDate.AddDays(14);

            for (var day = startDate; day <= endDate; day = day.AddDays(1))
            {
                for (int i = 0; i < halls.Count(); i++)
                {                    
                    Hall hall = halls.ElementAt(i);
                    Play play = plays.ElementAt(i);

                    string id = new MiniGuid(Guid.NewGuid());
                    playDateTimes.Add(new PlayDateTime
                    {
                        Id = id,
                        PlayId = play.Id,
                        HallId = hall.Id,
                        DateTime = day,
                        SeatsTaken = new List<Seat>(),
                        Published = true
                    });
                    play.PlayDateTimesIds.Add(id);
                }
            }
            return playDateTimes;
        }

        private static List<Ticket> createTickets(IEnumerable<PlayDateTime> dateTimes, IEnumerable<Hall> halls)
        {
            var tickets = new List<Ticket>();
            var random = new Random();
            var randomSeatsNum = random.Next(25, 90);

            foreach (var dateTime in dateTimes)
            {
                Hall hall = halls.First(x => x.Id.Equals(dateTime.HallId));

                List<Seat> seatsForDate = new List<Seat>();

                for (int i = 0; i < randomSeatsNum; i++)
                {
                    Seat seat = hall.Seats.ElementAt(random.Next(0, 99));

                    if (seatsForDate.Contains(seat))
                        continue;
                    seatsForDate.Add(seat);
                    tickets.Add(new Ticket
                    {
                        Id = new MiniGuid(Guid.NewGuid()),
                        ForDateId = dateTime.Id,
                        Seat = seat
                    });
                    dateTime.SeatsTaken.Add(seat);
                }
            }

            return tickets;
        }

        private static User createUser(string name, string email, UserType type)
            => new User
            {
                Id = MiniGuid.NewGuid(),
                Name = name,
                Email = email,
                UserType = type
            };

        private static void WriteToDb(
            IEnumerable<Theatre> theatres = null,
            IEnumerable<Hall> halls = null,
            IEnumerable<Play> plays = null,
            IEnumerable<PlayDateTime> playDateTimes = null,
            IEnumerable<Ticket> tickets = null,
            IEnumerable<User> users = null)
        {
            if (theatres != null)
                ServiceProvider.GetService<ITheatreRepo>() //done
                    .Add(theatres);

            if (halls != null)
                ServiceProvider.GetService<IHallRepo>() //done
                    .Add(halls);

            if (plays != null)
                ServiceProvider.GetService<IPlayRepo>() //done
                    .Add(plays);

            if (playDateTimes != null)
                ServiceProvider.GetService<IPlayDateTimeRepo>() //done
                    .Add(playDateTimes);

            if (tickets != null)
                ServiceProvider.GetService<ITicketRepo>()
                    .Add(tickets);

            if (users != null)
                ServiceProvider.GetService<IUserRepo>() //done
                    .Add(users);
        }

        #region Helpers

        private static List<string> GetImages(int start, int end)
        {
            var images = new List<string>(end);

            for (int i = 0; i < end; i++)
            {
                int imgId = start + i;
                images.Add($"https://picsum.photos/id/{imgId}/1920/1440");
            }

            return images;
        }

        private static string Number2String(int number, bool isLower = false)
        {
            string returnVal = "";
            char c = isLower ? 'a' : 'A';
            while (number >= 0)
            {
                returnVal = (char)(c + number % 26) + returnVal;
                number /= 26;
                number--;
            }

            return returnVal;
        }

        private static void ConsoleLog(string title, object obj = null)
        {
            JSRuntime ??= ServiceProvider.GetService<IJSRuntime>();

            if (obj != null)
                JSRuntime.InvokeVoidAsync("console.log", title, "\n", obj);
            else
                JSRuntime.InvokeVoidAsync("console.log", title);
        }

        private static void ShowAlert()
        {
            JSRuntime ??= ServiceProvider.GetService<IJSRuntime>();
            JSRuntime.InvokeVoidAsync("alert", "The app is generating demo data and will be available in a moment!");
        }
        #endregion
    }
}