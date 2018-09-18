using System.Threading;
using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.VIewModel;
using Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApp;

namespace NoteApp
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

	        services.AddDbContext<NoteAppDbContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=NoteAppV1;Username=postgres;Password=348275723", builder => builder.MigrationsAssembly("NoteApp")));
			services.AddScoped(a =>
			{
				var accessor = a.GetService<IHttpContextAccessor>();
				if (accessor.HttpContext == null)
					return new CancellationTokenSource();
				return CancellationTokenSource.CreateLinkedTokenSource(
					accessor.HttpContext.RequestAborted);
			});
			services.AddScoped<NotesService>();
	        services.AddScoped<IconHelper>();
	        services.AddScoped<GetExcelWithNotes>();
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
				options =>
				{
					options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
				});

	        services.AddMvc();

		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			

			app.UseStaticFiles();

	        app.UseAuthentication();

	        app.UseMvc(routes =>
	        {
		        routes.MapRoute(
			        name: "default",
			        template: "{controller=Home}/{action=Index}/{id?}");
	        });
		}
    }
}
