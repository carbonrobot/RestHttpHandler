namespace RestHttpHandler.Attributes
{
    using System;

    /// <summary>
    /// Marks a method as handling HttpGet requests
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpGetAttribute : HttpMethodAttribute
    {

    }
}
