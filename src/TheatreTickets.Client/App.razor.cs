using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using TheatreTickets.Client.Components.Shared;

namespace TheatreTickets.Client
{
    public partial class App : ComponentBase
    {
        [Inject] protected ISyncLocalStorageService LocalStorage { get; set; }

        private ModalComponent Modal { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender) Events.ModalEvent += ShowModal;

            bool firstTimeLaunch = !LocalStorage.HasFirstTimeLaunchKey();

            if (firstTimeLaunch)
            {
                LocalStorage.AddHasFirstTimeLaunchKey();
                Events.ShowModal(this, "Welcome!", "This is a demo app for buying theatre tickets and no data is real. You can read more at about and github!");
            }
        }

        private void ShowModal(object sender, Tuple<string, string> e)
            => Modal.Show(e.Item1, e.Item2);
    }
}