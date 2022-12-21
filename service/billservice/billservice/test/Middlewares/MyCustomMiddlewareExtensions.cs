using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace billservice.test.Middlewares
{
    public static class MyCustomMiddlewareExtensions
    {
        //为刚才创建的 自定义中间件 创建一个扩展方法，这么做的目的就是可以方便将 中间件 注入到 pipeline 中，官方的推荐做法就是在 IApplicationBuilder 接口上进行扩展
        public static IApplicationBuilder UseMyCustomMiddleware ( this IApplicationBuilder builder )
        {
            return builder.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
