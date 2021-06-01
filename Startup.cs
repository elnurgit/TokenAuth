using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TokenAuthLogin.Models;
using TokenAuthLogin.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;

namespace TokenAuthLogin
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
            services.AddControllers();
            var tokenKey = Configuration.GetValue<string>("TokenKey");
            var key = Encoding.ASCII.GetBytes(tokenKey);
            
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
                    ValidateAudience = false
                };
            });

            services.AddSingleton<IJWTAuthenticationManager>(new JWTAuthenticationManager(tokenKey));

            // var jwtSettings = new JwtSettings();
            // Configuration.Bind(nameof(jwtSettings), jwtSettings);
            // services.AddSingleton(jwtSettings);
           
            //services.AddDbContext<EmployeeContext>(opt=>opt.UseInMemoryDatabase("AuthLogin"));
            var connectionString = Configuration.GetConnectionString("EmployeeCon");
            services.AddDbContext<EmployeeContext>(opt=>opt.UseSqlServer(connectionString));

            // services.AddAuthentication(x=>
            // {
            //     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            // })
            // .AddJwtBearer(x=>{
            //     x.SaveToken = true;
            //     x.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuerSigningKey = true,
            //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
            //         ValidateIssuer = false,
            //         ValidateAudience = false,
            //         RequireExpirationTime = false,
            //         ValidateLifetime  =true
            //     };
            // });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TokenAuthLogin", Version = "v1" });

                // var security = new Dictionary<string, IEnumerable<string>>
                // {
                //     {"Bearer", new string[0]}
                // };

                // c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                // {
                //     Description = "JWT Authorization header using the bearer scheme",
                //     Name="Authorization",
                //     In=ParameterLocation.Header,
                //     Type=SecuritySchemeType.ApiKey
                // });
                // //c.AddSecurityRequirement(security);
                // c.AddSecurityRequirement(new OpenApiSecurityRequirement
                // {
                //     {new OpenApiSecurityScheme{Reference = new OpenApiReference
                //     {
                //         Id = "Bearer",
                //         Type = ReferenceType.SecurityScheme
                //     }}, new List<string>()}
                // });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TokenAuthLogin v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
