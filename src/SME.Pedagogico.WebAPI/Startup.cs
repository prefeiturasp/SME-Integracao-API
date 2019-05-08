using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SME.Pedagogico.Interface;
using SME.Pedagogico.Interface.Settings;
using SME.Pedagogico.IoC;
using SME.Pedagogico.Repository.Context;
using Swashbuckle.AspNetCore.Swagger;
using static System.Text.Encoding;

namespace SME.Pedagogico.WebAPI
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
            RegisterDependency.Register(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ApiContext>(o =>
                o.UseNpgsql(Configuration.GetConnectionString("SgpConnection")));

            var jwtConfiguration = Configuration
                .GetSection(nameof(JwtTokenSettings));
            services.Configure<JwtTokenSettings>(jwtConfiguration);
            var jwtTokenSettings = jwtConfiguration
                .Get<JwtTokenSettings>();

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidAudience = jwtTokenSettings.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenSettings.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(UTF8
                        .GetBytes(jwtTokenSettings.IssuerSigningKey))
                };
            });

            services.Configure<ConnectionStrings>(Configuration
                .GetSection(nameof(ConnectionStrings)));

            services.Configure<PaginacaoSettings>(Configuration
                .GetSection(nameof(PaginacaoSettings)));

            services.Configure<AutorizacaoSettings>(Configuration
                .GetSection(nameof(AutorizacaoSettings)));

            // CORS (Cross-Origin Requests)
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                      //.WithOrigins("https://origem1.com", "http://origem2.com.br")
                        .AllowAnyMethod() 
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .Build());
            });

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new Info
                {
                    Title = "SME - API Pedagógico",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SME.PEdagogico.API");
            });
        }
    }
}
