using System;

namespace Microsoft.AspNetCore.Components
{
    public static class NavigationManagerExtensions
    {
        /// <summary>
        /// Navigates to the specified uri and adds the base uri in the front.
        /// The base uri is found at index.html, it's the href of the base tag.
        /// </summary>
        /// <param name="manager">The navigation manager.</param>
        /// <param name="uri">The destination URI. This can be absolute, or relative to the base URI
        /// (as returned by Microsoft.AspNetCore.Components.NavigationManager.BaseUri).</param>
        /// <param name="forceLoad">If true, bypasses client-side routing and forces the browser to load the new
        /// page from the server, whether or not the URI would normally be handled by the client-side router.</param>
        public static void Navigate(this NavigationManager manager, string uri, bool forceLoad = false)
        {
            if (uri.StartsWith("/")) uri = uri.Remove(0, 1);
            manager.NavigateTo($"{manager.BaseUri}{uri}");
        }
    }
}