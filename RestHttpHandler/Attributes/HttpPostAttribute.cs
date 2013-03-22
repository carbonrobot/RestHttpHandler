namespace RestHttpHandler.Attributes
{
    using System;

    /// <summary>
    /// Marks a method as handling HttpPost requests
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpPostAttribute : HttpMethodAttribute
    {

    }
}
