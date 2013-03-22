namespace RestHttpHandler
{
    /// <summary>
    /// String based content such as html
    /// </summary>
    public class ContentResult : ActionResult
    {
        private readonly string _content;

        public ContentResult(string content)
        {
            Require.IsNotNull(() => content);

            _content = content;
        }

        protected override void OnExecute(System.Web.HttpResponse response)
        {
            response.Write(_content);
        }

    }
}
