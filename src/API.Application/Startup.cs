using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.CrossCutting.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Application
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

            ConfigureService.ConfigureDependenciesService(services); //adicionado configuracao statica do automapper
            ConfigureRepository.ConfigureDependenciesRepository(services);

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Web API - DDD",
                    Description = "Implementaçao Web API com padrao DDD",
                    TermsOfService = @"https://www.maximizi.com.br/terms",
                    Contact = new Contact
                    {
                        Name = "Luis Augusto Ferreira",
                        Email = "blackbarth@outlook.com",
                        Url = @"https://www.maximizi.com.br",
                    },
                    License = new License
                    {
                        Name = "Usar sobre LICX",
                        Url = @"https://www.maximizi.com.br/licence",
                    }
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //swagger
            //habilitar middleware
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Implementaçao webapi com padrao DDD");
            });

            //Redireciona o link para o Swagger, quando acessar a rota principal
            var Option = new RewriteOptions();
            Option.AddRedirect("^$", "swagger");
            app.UseRewriter(Option);

            app.UseMvc();
        }
    }
}
