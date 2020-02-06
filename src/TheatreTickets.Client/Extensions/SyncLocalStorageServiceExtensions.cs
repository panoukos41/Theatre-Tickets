using Blazored.LocalStorage;

namespace TheatreTickets.Client
{
    public static class SyncLocalStorageServiceExtensions
    {
        private const string key_firstTimeLaunch = "firstTimeLaunch";
        private const string key_hasDemoData = "hasDemoData";

        public static bool HasFirstTimeLaunchKey(this ISyncLocalStorageService localStorage)
            => localStorage.ContainKey(key_firstTimeLaunch);

        public static void AddHasFirstTimeLaunchKey(this ISyncLocalStorageService localStorage)
            => localStorage.SetItem(key_firstTimeLaunch, false);

        public static bool HasDemoDataKey(this ISyncLocalStorageService localStorage)
            => localStorage.ContainKey(key_hasDemoData);

        public static void AddDemoDataKey(this ISyncLocalStorageService localStorage)
            => localStorage.SetItem(key_hasDemoData, true);
    }
}