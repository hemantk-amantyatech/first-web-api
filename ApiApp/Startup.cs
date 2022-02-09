using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ApiApp
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
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            //services.AddAuthentication(opt =>
            //{
            //    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(jwt => {
            //    jwt.RequireHttpsMetadata = false;
            //    jwt.SaveToken = true;
            //    jwt.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidateAudience = true,
            //        ValidIssuer = "",
            //        ValidAudience = "",
            //        //for secret key
            //        IssuerSigningKeys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretkey@123"))



            //    };
            //});

           

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                //opt.DefaultChallengeScheme = JwtBearerDefaults.


            })
.AddJwtBearer(optiom =>
{
    optiom.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,



        ValidIssuer = "https://localhost:5001",
        ValidAudience = "https://localhost:5001",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Key@1234"))
    };
});




            services.AddControllers();

            services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseCors("MyPolicy");
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseHttpsRedirection();

            //app.UseRouting();


            //app.UseAuthentication();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
            app.UseRouting();



            app.UseAuthentication();
            app.UseAuthorization();



            //app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
