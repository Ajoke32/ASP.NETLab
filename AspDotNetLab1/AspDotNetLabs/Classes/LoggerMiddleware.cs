namespace AspDotNetLabs.Classes
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _path = Directory.GetCurrentDirectory() + "/access.txt";
        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.Headers.RequestId = Guid.NewGuid().ToString();
            File.AppendAllText(_path,$"\n---------------------------------------------\n" +
                $"Request Id: {context.Request.Headers.RequestId}\nAgent:{context.Request.Headers.UserAgent}\nMethod: {context.Request.Method}" +
                $"\nPath {context.Request.Path}\nDate: {DateTime.Now}" +
                $"\n---------------------------------------------\n");
            await _next(context);
        }
    }
}
