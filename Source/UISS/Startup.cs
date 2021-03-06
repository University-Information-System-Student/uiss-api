namespace UISS
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using AutoMapper;

    using Data;
    using Data.Contracts;
    using Services;
    using Services.Contracts;
    using Services.Infrastructure;
    using GlobalConstants.ApplicationSettings;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var applicationSettings = configuration
                .GetSection(nameof(ApplicationSettings));

            services.Configure<ApplicationSettings>(applicationSettings);
            
            services.AddAutoMapper(
                typeof(ServiceMappingProfile).Assembly);

            services.AddSingleton<IIdentityDbContext, IdentityDbContext>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IJwtService, JwtService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
