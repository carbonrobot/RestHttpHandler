namespace RestHttpHandler
{
    using System;
    using System.Linq.Expressions;

    public static class Require
    {
        /// <summary>
        /// Requires an argument to be not null or empty
        /// </summary>
        public static void IsNotNull(Expression<Func<object>> selector, string message = "")
        {
            var paramValue = selector.Compile()();
            
            // special case check for null or empty strings
            if(paramValue is string)
                if(!string.IsNullOrEmpty((string)paramValue))
                    return;

            // check for null
            if (paramValue != null)
                return;

            var param = selector.Body as MemberExpression;
            throw new ArgumentNullException(param.Member.Name, message);
        }
    }
}
