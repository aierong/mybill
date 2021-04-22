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
            ���ﲻ���һ��

            new Claim( System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, "��������D2-B49B") 

            ���ַ�ʽע���ֵ�ᱻ���Ĭ��ת��Ϊ:http://schemas.xmlsoap.org/ws/2005/05/identity/claims�������Ĵ洢��ʽ
            
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



            // ���԰ѷ���ע��Ĵ�����ھ�̬��չ�����ʹ�� ConfigureServices ���Ӽ��
            // ���Է�ģ��ֱ�д��ͬ�ľ�̬��չ����
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

            // ������֤�м��
            app.UseAuthentication();//������֤

            app.UseAuthorization();

            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllers();
             } );
        }
    }
}
