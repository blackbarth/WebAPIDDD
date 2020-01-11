using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.CrossCutting.DependencyInjection;
using API.Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using API.CrossCutting.Mappings;
using AutoMapper;

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

            #region Configuracao AutoMapper
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);

            #endregion


            #region Configuracao Jwt
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);
            #endregion

            #region Autenticaçao
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
            });

            #endregion

            //swagger  framework 2.2
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new Info
            //     {
            //         Version = "v1",
            //         Title = "Web API - DDD",
            //         Description = "Implementaçao Web API com padrao DDD",
            //         TermsOfService = @"https://www.maximizi.com.br/terms",
            //         Contact = new Contact
            //         {
            //             Name = "Luis Augusto Ferreira",
            //             Email = "blackbarth@outlook.com",
            //             Url = @"https://www.maximizi.com.br",
            //         },
            //         License = new License
            //         {
            //             Name = "Usar sobre LICX",
            //             Url = @"https://www.maximizi.com.br/licence",
            //         }
            //     });

            //     //habilitar entrada de token no swagger
            //     c.AddSecurityDefinition("Bearer", new ApiKeyScheme
            //     {
            //         In = "header",
            //         Description = "Entre com o token JWT",
            //         Name = "Authorization",
            //         Type = "apiKey"
            //     });

            //     c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>{
            //         {"Bearer", Enumerable.Empty<string>()}
            //     });

            // });

            //swagger  framework 3.1
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Web API - DDD aspnetcore 3.1",
                    Description = "Implementaçao Web API com padrao DDD aspnetcore 3.1",
                    TermsOfService = new Uri(@"https://www.maximizi.com.br/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Luis Augusto Ferreira",
                        Email = "blackbarth@outlook.com",
                        Url = new Uri(@"https://www.maximizi.com.br"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Usar sobre LICX",
                        Url = new Uri(@"https://www.maximizi.com.br/licence"),
                    }
                });

                //habilitar entrada de token no swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Entre com o token JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference{
                        Id = "Bearer",
                                 Type = ReferenceType.SecurityScheme
                             }
                         }, new List<string>()
                     }
                 });

            });


            // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); //framework 2.2
            //services.AddMvc(Options => { Options.EnableEndpointRouting = false; });
            services.AddControllers()
                    .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)  //IHostingEnvironment framework 2.2
        {
            if (env.IsDevelopment()) //no framework 3.0 funciona com:  using Microsoft.Extensions.Hosting;
            {
                app.UseDeveloperExceptionPage();
            }


            //swagger
            //habilitar middleware
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Implementaçao webapi com padrao DDD aspnetcore 3.1");
            });

            //Redireciona o link para o Swagger, quando acessar a rota principal
            var Option = new RewriteOptions();
            Option.AddRedirect("^$", "swagger");
            app.UseRewriter(Option);


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseMvc();
        }
    }
}
