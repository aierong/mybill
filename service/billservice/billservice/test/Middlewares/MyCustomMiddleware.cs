using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace billservice.test.Middlewares
{
    public class MyCustomMiddleware
    {
        private readonly RequestDelegate _next;
        public MyCustomMiddleware ( RequestDelegate next )
        {
            _next = next;
        }


        //这是 ASP.Net Core 中间件中约定的方法名， Invoke 方法中你可以实现监视或者修改 Request 或 Resposne。
        public async Task Invoke ( HttpContext httpContext )
        {
            if ( !httpContext.Request.Headers.Keys.Contains( "Authentication-Key" ) )
            {
                httpContext.Response.StatusCode = 400;

                await httpContext.Response.WriteAsync( "Authentication key is missing..." );

                return;
            }
            else
            {
                //Write code here to validate the authentication key.
            }

            await _next.Invoke( httpContext );
        }
    }
}
