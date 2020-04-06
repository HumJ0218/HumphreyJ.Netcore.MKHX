using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumphreyJ.NetCore.MKHX.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HumphreyJ.NetCore.MKHX.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        internal static IConfiguration Configuration { get; set; }
        internal static IApplicationBuilder Application { get; set; }
        internal static IHostingEnvironment Environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSession();
            services.AddMvc(options =>
            {
                options.Filters.Add<HttpGlobalExceptionFilter>();
            });
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            Application = app;
            Environment = env;

            app.UseExceptionHandler("/error{ExceptionHandler}/ExceptionHandler");
            app.UseStatusCodePagesWithReExecute("/error{0}/StatusCodePagesWithReExecute");

            //if (env.IsDevelopment())
            //{
            //    app.UseBrowserLink();
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/error{ExceptionHandler}/ExceptionHandler");
            //    app.UseStatusCodePagesWithReExecute("/error{0}/StatusCodePagesWithReExecute");
            //}

            //app.UseSession();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "Error404",
                    template: "*{catchall}",
                    defaults: new
                    {
                        controller = "Error",
                        action = "404",
                    });

            });
        }
    }
}
