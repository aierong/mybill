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

           

            // ���԰ѷ���ע��Ĵ�����ھ�̬��չ�����ʹ�� ConfigureServices ���Ӽ��
            // ���Է�ģ��ֱ�д��ͬ�ľ�̬��չ����
            services.AddService( Configuration );

            services.AddMvc().AddJsonOptions( ( options ) =>
            {
                options.JsonSerializerOptions.Converters.Add( new DatetimeJsonConverter() );

                //.net core3.1�з������������ֶ�����ĸ��Ĭ��Сд(�շ��ʽ)
                // ��ֵָ�����ڽ��������������ת��Ϊ������ʽ������ camel ��Сд���Ĳ��ԣ���Ϊ null���򱣳��������Ʋ��䡣
                options.JsonSerializerOptions.PropertyNamingPolicy = null;

                // ����� �Ƿ��ʽ�����
                // ��ֵ����JSON�Ƿ�Ӧʹ�������ӡ�� Ĭ������£���ʹ���κζ���Ŀհ������л�JSON��
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
