using LogProxyAPI.AirTableService;
using LogProxyAPI.AirTableService.AirTableService.LogMessages;
using LogProxyAPI.Core.LogProxyAPI.Core;
using LogProxyAPI.UserManagement.UserManagement;
using LogProxyAPI_HeidelbergCement.Authentication;
using LogProxyAPI_HeidelbergCement.Configuration;
using LogProxyAPI_HeidelbergCement.Configuration.ApiFilters.ActionFilters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LogProxyAPI_HeidelbergCement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Options pattern for providing access for AirTableService related settings.
            services.AddOptions<AirTableApiSettings>()
                 .Bind(Configuration.GetSection(AirTableApiSettings.SectionName));

            // AirTableLogMessageService is stateless and impements http communication to third party web service.
            services.AddSingleton<IAirTableLogMessageService, AirTableLogMessagesService>();

            // Application layer handler for specific use case scenario. (LogMessages)
            services.AddScoped<LogMessageHandler>();
            // UserService for managing UserAccess 
            services.AddScoped<IUserService, UserService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Basic";
            }).AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic",null);

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder("Basic")
                   .RequireAuthenticatedUser()
                   .Build();
            });

            services.AddControllers();

            services.AddMvc(options =>
            {
                // Filter for Validating the Model
                options.Filters.Add(typeof(ValidateModelStateFilter));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseRouting();

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Use Atribute Routing
                endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}
