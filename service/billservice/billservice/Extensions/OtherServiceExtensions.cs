using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Helpers;
using billservice.Helpers.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace billservice.Extensions
{
    public static class OtherServiceExtensions
    {
        /// <summary>
        /// 其它的相关服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddOtherService ( this IServiceCollection services , IConfiguration configuration )
        {

            #region 接收模型参数验证失败,自定义错误格式

            services.Configure<ApiBehaviorOptions>( options =>
            {
                options.InvalidModelStateResponseFactory = ( context ) =>
                {
                    string errormsg = string.Empty;

                    foreach ( var item in context.ModelState )
                    {
                        var state = item.Value;
                        var message = state.Errors.FirstOrDefault( p => !string.IsNullOrWhiteSpace( p.ErrorMessage ) )?.ErrorMessage;

                        if ( message != null )
                        {
                            errormsg = message;

                            // 可能会有多个错误,这里,我们取第一个,就返回了
                            break;
                        }

                    }

                    var result = new ServiceResult();
                    result.IsFailed( errormsg );

                    return new JsonResult( result );

                };
            } );

            #endregion



            //注册cache服务
            services.AddMemoryCache();


            services.AddMvc().AddJsonOptions( ( options ) =>
            {
                options.JsonSerializerOptions.Converters.Add( new DatetimeJsonConverter() );

                //.net core3.1中返回数据属性字段首字母是默认小写(驼峰格式)
                // 该值指定用于将对象的属性名称转换为其他格式（例如 camel 大小写）的策略；若为 null，则保持属性名称不变。
                options.JsonSerializerOptions.PropertyNamingPolicy = null;

                // 这个是 是否格式化输出
                // 该值定义JSON是否应使用整齐打印。 默认情况下，不使用任何额外的空白来序列化JSON。
                options.JsonSerializerOptions.WriteIndented = true;

            } );

            return services;
        }
    }
}
