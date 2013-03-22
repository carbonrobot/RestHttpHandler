namespace RestHttpHandler
{
    using System;
    using System.Web;

    /// <summary>
    /// Redirect to a new url
    /// </summary>
    public class RedirectResult : ActionResult
    {
        private readonly Uri _uri;

        public RedirectResult(string url)
        {
            Require.IsNotNull(() => url);

            _uri = new Uri(url, UriKind.RelativeOrAbsolute);
        }

        public RedirectResult(Uri uri)
        {
            Require.IsNotNull(() => uri);

            _uri = uri;
        }

        protected override void OnExecute(HttpResponse response)
        {
            response.Redirect(_uri.ToString(), false);
        }
    }
}
