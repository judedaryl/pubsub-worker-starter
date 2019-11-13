using Microsoft.Extensions.Hosting;

namespace PubSubWorkerStarter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                    .ConfigureServices((context, services) =>
                    {
                        var startup = new Startup(context.Configuration);
                        startup.ConfigureServices(services);
                    });
    }
}
