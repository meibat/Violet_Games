using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VioletGames.Data;
using VioletGames.Data.Helper;
using VioletGames.Data.Repositorio;
using VioletGames.Repositorio;

namespace VioletGames
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //Conectando banco de dados (string de conn no appsettings)
            services.AddEntityFrameworkSqlServer().AddDbContext<BancoContent>(o => o.UseSqlServer(Configuration.GetConnectionString("DataBase")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Toda vez que a interface for chamada, acontecera a injecao de depedencia
            services.AddScoped<IFuncionarioRepositorio, FuncionarioRepositorio>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            services.AddScoped<IConsoleRepositorio, ConsoleRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<ISessionUser, SessionUser>();

            services.AddSession(o => 
            {
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
