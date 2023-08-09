using TintaExpressBackend.Context;
using Microsoft.EntityFrameworkCore;

namespace TintaExpressBackend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                var frontendURL = Configuration.GetValue<string>("frontend_url");
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                });

            options.AddPolicy("AllowFlutterApp",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });

            });
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("conexion"))
            );
        }
        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            app.UseRouting(); // Agregar UseRouting antes de UseEndpoints

            app.UseCors("AllowFlutterApp");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
     }
}
