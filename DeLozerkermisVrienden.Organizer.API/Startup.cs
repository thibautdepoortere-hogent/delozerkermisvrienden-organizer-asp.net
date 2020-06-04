using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DeLozerkermisVrienden.Organizer.API.DbContexts;
using DeLozerkermisVrienden.Organizer.API.Helpers;
using DeLozerkermisVrienden.Organizer.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace DeLozerkermisVrienden.Organizer.API
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = environment == Environments.Development;

            if (isDevelopment)
            {
                services.AddCors(options => { options.AddPolicy(name: MyAllowSpecificOrigins, builder => { builder.WithOrigins("http://localhost:3000", "http://localhost:65000").AllowAnyHeader().AllowAnyMethod(); }); });
            };
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            })
            .AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            })
            .AddXmlDataContractSerializerFormatters()
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    // Create a problem details object
                    var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
                    var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);

                    // Add additional info not added by default
                    problemDetails.Detail = "Bekijk de error-eigenschap voor details.";
                    problemDetails.Instance = context.HttpContext.Request.Path;

                    // Find out which status code to use
                    var actionExecutingContext = context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                    // if there are modelstate errors & all arguments were correctly
                    // found/parsed we're dealing with validation errors
                    if ((context.ModelState.ErrorCount > 0) && (actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                    {
                        problemDetails.Type = "https://delozerkermisvrienden.com/modelvalidationproblem";
                        problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                        problemDetails.Title = "Een of meer validatiefouten hebben zich voorgedaan.";

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };

                    // if one of the arguments wasn't correctly found / couldn't be parsed
                    // we're dealing with null/unparseable input
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Een of meer fouten bij het invoeren hebben zich voorgedaan.";
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

            //--- START JWT Authentication
            var key = Encoding.ASCII.GetBytes(Configuration["JWTAuthenticationSecret"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireSignedTokens = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    LifetimeValidator = LifetimeValidator,
                };
            });
            //--- END JWT Authentication

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IAppSettings, AppSettings>();
            services.AddScoped<IBetaalmethodeRepository, BetaalmethodeRepository>();
            services.AddScoped<IInschrijvingsstatusRepository, InschrijvingsstatusRepository>();
            services.AddScoped<IEvenementCategorieRepository, EvenementCategorieRepository>();
            services.AddScoped<IEvenementRepository, EvenementRepository>();
            services.AddScoped<ILidRepository, LidRepository>();
            services.AddScoped<IInschrijvingRepository, InschrijvingRepository>();
            services.AddScoped<IBetaaltransactieRepository, BetaaltransactieRepository>();
            services.AddScoped<ICheckInRepository, CheckInRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<INieuwsbriefRepository, NieuwsbriefRepository>();
            services.AddScoped<IAuthenticatieRepository, AuthenticatieRepository>();
            services.AddScoped<IMailing, Mailing>();
            services.AddScoped<IFabrieksInstellingRepository, FabrieksInstellingRepository>();
            services.AddDbContext<OrganizerContext>(options =>
            {
                if (isDevelopment)
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DevelopConnection"));
                } else
                {
                    options.UseSqlServer(Configuration["ConnectionStringDatabase"]);
                }
                //options.UseSqlServer(Configuration["ConnectionStringDatabase"]);
            });
        }

        private bool LifetimeValidator(DateTime? notBefore,DateTime? expires,SecurityToken securityToken,TokenValidationParameters validationParameters)
        {
            if (expires.HasValue)
            {
                if (DateTime.UtcNow > expires)
                {
                    return false;
                }
            }

            if (notBefore.HasValue)
            {
                if (DateTime.UtcNow < notBefore)
                {
                    return false;
                }
            }

            return true;
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
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Er is iets fout gelopen, probeer het later opnieuw");
                    });
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            if (env.IsDevelopment()) {
                app.UseCors(MyAllowSpecificOrigins);
            }
            

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
