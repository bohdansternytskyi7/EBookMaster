using System.Text;

namespace EBookMaster.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                LogErrorToFile(ex);
                throw;
            }
        }

        private void LogErrorToFile(Exception ex)
        {
            var logMessage = $"{DateTime.Now} - Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}{new string('-', 30)}{Environment.NewLine}";
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");
            File.AppendAllText(logFilePath, logMessage, Encoding.UTF8);
        }
    }
}
