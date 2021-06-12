using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Helpers.ConfigData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace billservice.Extensions
{
    public static class CorsServiceExtensions
    {
        /// <summary>
        /// 跨域的相关服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsService ( this IServiceCollection services , IConfiguration configuration )
        {
            CorsConfigData _CorsConfigData = configuration.GetSection( "CorsInfo" ).Get<CorsConfigData>();

            //定义名称
            string PolicyName = _CorsConfigData.name;
            string[] urls = _CorsConfigData.urls.ToArray();

            //添加cors 服务 配置跨域处理            
            services.AddCors( options => options.AddPolicy( PolicyName ,
             builder =>
             {

                 builder.WithOrigins( urls )
                                 .AllowAnyMethod()
                                 .AllowAnyHeader()
                                 .AllowCredentials();

             } ) );

            return services;
        }
    }
}
