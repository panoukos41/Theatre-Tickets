using System;

namespace TheatreTickets.Client
{
    public static class Events
    {
        public static EventHandler<Tuple<string, string>> ModalEvent { get; set; }

        public static void ShowModal(object sender, string title, string message)
            => ModalEvent?.Invoke(sender, Tuple.Create(title, message));
    }
}