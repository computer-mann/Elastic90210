using Nest;

namespace ElasticWeb
{
    public class StartUp
    {
        private readonly IConfiguration _configuration;

        public static void ConfigureServices(IServiceCollection services)
        {
            //services.UseSerilog();
        }

        public static void AddElasticSearchServices(IServiceCollection services) 
        {
            var elsticCon = new ConnectionSettings(new Uri("http://localhost:9200"));
            elsticCon.DefaultIndex("placeholder-posts");
            elsticCon.DisableDirectStreaming(true);
            var client=new ElasticClient(elsticCon);
            services.AddSingleton<IElasticClient>(client);

        }
    }
}
