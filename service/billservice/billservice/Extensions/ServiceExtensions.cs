using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace billservice.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// AutoMapper的相关服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapperService ( this IServiceCollection services )
        {

            services.AddAutoMapper( AppDomain.CurrentDomain.GetAssemblies() );

            return services;
        }
    }
}
