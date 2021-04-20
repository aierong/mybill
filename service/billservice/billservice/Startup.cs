using System;
using System.Collections.Generic;
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
            services.AddService( Configuration );

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure ( IApplicationBuilder app , IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllers();
             } );
        }
    }
}
