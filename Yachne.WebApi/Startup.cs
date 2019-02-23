using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Yachne.Application.Account;
using Yachne.Authentication;
using Yachne.Common.Encrypt;
using Yachne.Core;
using Yachne.EntityFrameworkCore;
using Yachne.Terminal;
using Yachne.WebApi.Attributes;
using Yachne.WebApi.Filters;
using Yachne.WebApi.Models;

namespace Yachne.WebApi
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
            string connectionString = Configuration["DB:DefaultConnectionString"];
            services.AddDbContext<YachneDBContext>(options =>
            {
                //options.UseSqlServer(EncryptProvider.RSADecrypt(connectionString, YachneConsts.privateKey));
                options.UseSqlServer(connectionString);
            });
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = YachneAuthDefaults.User,
                    ValidAudience = YachneAuthDefaults.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecurityKey"])),

                    // 设置Token缓冲过期时间  不设置的话默认为5分钟， 所以其实token的有效时间为  clockSkew + expTime
                    ClockSkew = TimeSpan.FromSeconds(0)
                };
            });
            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddMemoryCache();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<YachneContext>();
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<ITerminalProvider, TerminalProvider>();
            services.AddSingleton<AuthManager>();
            services.AddMvc(options =>
            {
                options.Filters.Add<YachneAuthorizationFilter>();
                options.Filters.Add<BasicResultAttribute>();
                options.Filters.Add<GlobalExceptionFilter>();

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            ServiceLocator.Instance = app.ApplicationServices;
            
            app.UseSession();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
