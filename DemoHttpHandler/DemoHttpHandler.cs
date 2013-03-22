namespace DemoHttpHandler
{
    using System;
    using System.Collections.Specialized;
    using RestHttpHandler;
    using RestHttpHandler.Attributes;

    public class DemoHttpHandler : RestHttpHandler
    {
        [HttpGet]
        public ActionResult Customer()
        {
            return new ContentResult("<h1>John Doe</h1>");
        }

        [HttpPost]
        public ActionResult Customer(NameValueCollection form)
        {
            return new ContentResult("<span>You did it!</span>");
        }

        [HttpGet]
        public ActionResult SendMeToStackOverflow()
        {
            return new RedirectResult("http://www.stackoverflow.com/");
        }

        /// <summary>
        /// Returns the current Type
        /// </summary>
        protected override Type GetSubType()
        {
            return this.GetType();
        }
    }
}
