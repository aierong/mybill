using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using billservice.Extensions;
using billservice.Helpers;
using billservice.Helpers.ConfigData;
using billservice.Helpers.Result;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EasyCaching.InMemory;



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



        public void ConfigureServices ( IServiceCollection services )
        {
            services.AddControllers();

            // 可以把服务注册的代码放在静态扩展方法里，使得 ConfigureServices 更加简洁
            // 可以分模块分别写不同的静态扩展方法
            services.AddDBService( Configuration );
            services.AddAutoMapperService();
            services.AddFluentValidationService();
            services.AddTokenService( Configuration );
            services.AddCorsService( Configuration );
            services.AddCacheService();
            services.AddOtherService();

        }



        public void Configure ( IApplicationBuilder app , IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            // 全局异常处理
            app.UseExceptionHandler( config =>
            {
                config.Run( async context =>
                {
                    var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();

                    if ( error != null )
                    {
                        var ex = error.Error;
                        string errormsg = ex.Message;
                        string errorStackTrace = ex.StackTrace != null ? ex.StackTrace : string.Empty;
                        string errpath = context.Request.Path;

                        var result = new ServiceResult<string>()
                        {
                            Result = string.Empty
                        };
                        result.IsFailed( errormsg );

                        string jsonerror = System.Text.Json.JsonSerializer.Serialize( result );

                        // 设置类型和状态码
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync( jsonerror );
                    }
                } );
            } );

            CorsConfigData _CorsConfigData = this.Configuration.GetSection( "CorsInfo" ).Get<CorsConfigData>();
            //定义名称
            string PolicyName = _CorsConfigData.name;
            app.UseCors( PolicyName );

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
