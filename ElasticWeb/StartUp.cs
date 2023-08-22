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
            var elsticCon = new ConnectionSettings(new Uri("http://localhost"));
            elsticCon.DefaultIndex("placeholder-posts");
            var client=new ElasticClient(elsticCon);
            services.AddSingleton<IElasticClient>(client);

        }
    }
}
