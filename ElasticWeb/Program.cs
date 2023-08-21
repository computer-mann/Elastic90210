using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace ElasticWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger =new LoggerConfiguration()
                  .WriteTo.Console()
                  .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200")))
                  .CreateLogger();
                
            try
            {
                Log.Information("Application is starting up.");
                var builder = WebApplication.CreateBuilder(args);
                //builder.Host.;
                var services = builder.Services;
                // Add services to the container.
                services.AddControllersWithViews();
                services.AddRouting(op => op.LowercaseUrls = true);
                services.AddHttpClient();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Run();
            }catch(Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }finally
            {
                Log.CloseAndFlush();
            }

        }
    }
}