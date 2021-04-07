using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Application.Cities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.Generation.AspNetCore;
using AutoMapper;
using MediatR;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Decapolis
{
    public class Startup
    {
        protected readonly IConfiguration Configuration;
        protected readonly IWebHostEnvironment Environment;
        
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureControllers();
            services.ConfigureNSwag();
            services.ConfigureAutoMapper();
            services.ConfigureMediatR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.EnableNSwag();
            app.UseEndpoints(builder =>
            {
                builder.MapControllers();
            });
        }
    }
   
    public static class ControllerConfiguration
    {
        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddRouting((Action<RouteOptions>) (options => options.LowercaseUrls = true));
            IEnumerable<Type> mvcFilters = ControllerConfiguration.FindFilters(Assembly.GetExecutingAssembly()).Union<Type>(ControllerConfiguration.FindFilters(Assembly.GetCallingAssembly()));
            services.AddMvc((Action<MvcOptions>) (options =>
            {
                options.EnableEndpointRouting = true;
                options.Filters.Add((IFilterMetadata) new ProducesResponseTypeAttribute(400));
                options.Filters.Add((IFilterMetadata) new ProducesResponseTypeAttribute(401));
                options.Filters.Add((IFilterMetadata) new ProducesResponseTypeAttribute(403));
                options.Filters.Add((IFilterMetadata) new ProducesResponseTypeAttribute(500));
                foreach (Type filterType in mvcFilters)
                    options.Filters.Add(filterType);
            })).SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        private static IEnumerable<Type> FindFilters(Assembly assembly) => ((IEnumerable<Type>) assembly.GetTypes()).Where<Type>((Func<Type, bool>) (p => typeof (IFilterMetadata).IsAssignableFrom(p) && !typeof (ControllerBase).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract));
    }
    
    public static class NSwagConfiguration
    { 
        public static void ConfigureNSwag(this IServiceCollection services)
        {

            NSwagServiceCollectionExtensions.AddSwaggerDocument(services, (Action<AspNetCoreOpenApiDocumentGeneratorSettings>) (settings => settings.PostProcess = (Action<OpenApiDocument>) (document =>
            {
                document.Info.Version = "v1";
                document.Info.Title = Assembly.GetCallingAssembly().GetName().Name + " API";
                document.Info.Description = "REST API for " + Assembly.GetCallingAssembly().GetName().Name + ".";
            })));
        }

        public static void EnableNSwag(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            RewriteOptions options = new RewriteOptions().AddRedirect("^$", "swagger");
            app.UseRewriter(options);
        }
    }

    public static class MediatRConfiguration
    {
        public static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(CitiesMappingProfile).Assembly);
        }
    }

    public static class AutoMapperConfiguration
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApiMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc();
        }
    }
}
