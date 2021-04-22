using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using billservice.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace billservice
{
    public class Startup
    {
        public Startup ( IConfiguration configuration )
        {
            Configuration = configuration;

            /**
            这里不清除一下

            new Claim( System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, "主题来了D2-B49B") 

            这种方式注册的值会被框架默认转换为:http://schemas.xmlsoap.org/ws/2005/05/identity/claims，这样的存储方式
            
            */
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }



        public IConfiguration Configuration
        {
            get;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices ( IServiceCollection services )
        {
            services.AddControllers();



            // 可以把服务注册的代码放在静态扩展方法里，使得 ConfigureServices 更加简洁
            // 可以分模块分别写不同的静态扩展方法
            services.AddDBService( Configuration );
            services.AddAutoMapperService( Configuration );
            services.AddFluentValidationService( Configuration );
            services.AddTokenService( Configuration );
            services.AddOtherService( Configuration );

            
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure ( IApplicationBuilder app , IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // 开启验证中间件
            app.UseAuthentication();//启用验证

            app.UseAuthorization();

            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllers();
             } );
        }
    }
}
