namespace RestHttpHandler
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Remoting.Contexts;
    using System.Web;
    using Attributes;
using System.Collections.Specialized;

    public abstract class RestHttpHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }
        
        /// <summary>
        /// Process the request
        /// </summary>
        /// <param name="context">The http context for this request</param>
        public void ProcessRequest(HttpContext context)
        {
            // handle the request based on the method
            var action = System.IO.Path.GetFileNameWithoutExtension(context.Request.Url.LocalPath);

            // resolve the action and create a result that should be executed
            ActionResult actionResult = null;
            var method = this.GetActionMethod(context.Request.HttpMethod, action);
            if (method != null)
            {
                var methodParams = method.GetParameters();
                if (methodParams.Length == 0)
                    actionResult = method.Invoke(this, null) as ActionResult;
                else if(methodParams.Length == 1 && methodParams[0].GetType() == typeof(NameValueCollection))
                    actionResult = method.Invoke(this, new object[] { context.Request.Form }) as ActionResult;
            }
            else
            {
                actionResult = new RedirectResult(this.GetBaseUri(context.Request));
            }
            
            // execute the result
            actionResult.Execute(context.Response);
        }

        /// <summary>
        /// Gets the action method by http method and action name.
        /// </summary>
        /// <param name="httpMethod">The httpMethod name</param>
        /// <param name="action">The action name</param>
        private MethodInfo GetActionMethod(string httpMethod, string action)
        {
            switch (httpMethod)
            {
                case HttpMethods.Get:
                    return this.GetActionMethod<HttpGetAttribute>(action);
                case HttpMethods.Post:
                    return this.GetActionMethod<HttpPostAttribute>(action);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the action method by name and requestType.
        /// </summary>
        /// <param name="name">The name.</param>
        private MethodInfo GetActionMethod<T>(string name) where T : HttpMethodAttribute
        {
            // lookup the proper method by name and request type
            var requestType = typeof (T);
            var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            return (from m in this.GetSubType().GetMethods(flags)
                    where m.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)
                          && Attribute.IsDefined(m, requestType)
                    select m).FirstOrDefault();
        }

        /// <summary>
        /// Get the application relative absolute uri without a trailing slash
        /// </summary>
        /// <example>
        /// http://www.widgetfactory.com/en-us
        /// </example>
        private Uri GetBaseUri(HttpRequest request)
        {
            return new Uri(request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath, UriKind.Absolute);
        }

        /// <summary>
        /// Must override this method and return the type of the subclass
        /// </summary>
        protected abstract Type GetSubType();
    }
}
