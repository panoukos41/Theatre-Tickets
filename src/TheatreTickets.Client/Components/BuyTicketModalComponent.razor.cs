using BlazorStrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatreTickets.Client.Repos;
using TheatreTickets.Models;

namespace TheatreTickets.Client.Components
{
    public partial class BuyTicketModalComponent : ComponentBase
    {
        [Inject] private ITicketRepo TicketRepo { get; set; }

        [Inject] private IPlayDateTimeRepo PlayDateTimeRepo { get; set; }

        [Parameter] public EventCallback OnClosed { get; set; }

        private PlayDateTime PlayDateTime = null;

        private IEnumerable<IGrouping<int, Seat>> Rows;

        private readonly List<SeatComponent> choosenSeats = new List<SeatComponent>();

        private bool bought = false;

        private bool loading = false;

        private BSModal modal;

        private int seatsRemaining
            => PlayDateTime.Hall.Seats.Count - PlayDateTime.SeatsTaken.Count;

        public void Show(PlayDateTime playDateTime)
        {
            PlayDateTime = playDateTime;
            Rows = PlayDateTime.Hall.Seats.GroupBy(x => x.Row);

            StateHasChanged();
            modal.Show();
        }

        private void HandleSeatClick(SeatComponent seatComponent)
        {
            if (seatComponent.IsTaken)
            {
                return;
            }
            else if (choosenSeats.Contains(seatComponent))
            {
                seatComponent.SetSelected(false);
                choosenSeats.Remove(seatComponent);
            }
            else
            {
                seatComponent.SetSelected(true);
                choosenSeats.Add(seatComponent);
            }
        }

        private async void HandleBuy()
        {
            List<Ticket> tickets = new List<Ticket>();

            bought = true;
            loading = true;

            foreach (var seat in choosenSeats)
            {
                tickets.Add(new Ticket
                {
                    Id = MiniGuids.MiniGuid.NewGuid(),
                    ForDateId = PlayDateTime.Id,
                    Seat = seat.Seat
                });

                PlayDateTime.SeatsTaken.Add(seat.Seat);
            }

            Console.WriteLine($"Update PlayDatetime: {PlayDateTime.Id}");
            await PlayDateTimeRepo.Update(PlayDateTime);

            Console.WriteLine($"Add tickets:");
            await TicketRepo.Add(tickets);
            tickets.ForEach(x => Console.WriteLine($"---- Id: {x.Id}"));

            await Task.Delay(2000);

            loading = false;
            StateHasChanged();
            _ = OnClosed.InvokeAsync(this);
        }

        private void HandleDownload()
        {
        }

        private void Close(MouseEventArgs args)
        {
            foreach (var item in choosenSeats)
            {
                item.SetSelected(false);
            }
            choosenSeats.Clear();
            bought = false;
            loading = false;

            modal.Hide();
        }
    }
}