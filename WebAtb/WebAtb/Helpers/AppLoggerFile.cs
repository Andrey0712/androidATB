namespace WebAtb.Helpers
{
    public static class AppLoggerFile
    {
        public static void UseLoggerFile(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var path = Path.Combine(System.Environment.CurrentDirectory, "Logs");//создаем папку куда будем писать логи
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fileLog = Path.Combine(path, "log-{Date}.txt");//создаем фаил куда будем писать логи
                var services = scope.ServiceProvider;//создаем обьект 
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();//будет создавать логи
                loggerFactory.AddFile(fileLog);//записівать логи в фаил
            }
        }
    }
}
