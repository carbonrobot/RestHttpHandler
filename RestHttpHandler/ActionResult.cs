namespace RestHttpHandler
{
    using System.Web;

    /// <summary>
    /// The result of an action method. Override this class to create different results
    /// </summary>
    public class ActionResult
    {
        public ActionResult()
        {
        }

        public void Execute(HttpResponse response)
        {
            this.OnExecute(response);
            response.End();
        }

        protected virtual void OnExecute(HttpResponse response)
        {
        }
    }
}
