using Microsoft.EntityFrameworkCore;
using ZebraBet.API.Data;
using ZebraBet.API.Repositories;
using ZebraBet.API.Repositories.Interfaces;
using ZebraBet.API.Services;
using ZebraBet.API.Services.Interfaces;

namespace ZebraBet.API
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
            services.AddDbContext<ZebrabetContext>(options =>
                options.UseInMemoryDatabase("ZebraBetDB"));

            services.AddScoped<IEstadoRepository, EstadoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEquipeRepository, EquipeRepository>();
            services.AddScoped<IPartidaRepository, PartidaRepository>();
            services.AddScoped<IApostaRepository, ApostaRepository>();

            services.AddScoped<IEstadoService, EstadoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEquipeService, EquipeService>();
            services.AddScoped<IPartidaService, PartidaService>();
            services.AddScoped<IApostaService, ApostaService>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ZebrabetContext>();
                context.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    context.Response.Redirect("/swagger");
                });
            });
        }
    }
}
