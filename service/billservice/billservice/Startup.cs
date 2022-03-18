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



        public void ConfigureServices ( IServiceCollection services )
        {
            services.AddControllers();

            // ���԰ѷ���ע��Ĵ�����ھ�̬��չ�����ʹ�� ConfigureServices ���Ӽ��
            // ���Է�ģ��ֱ�д��ͬ�ľ�̬��չ����
            services.AddDBService( Configuration );
            services.AddAutoMapperService( Configuration );
            services.AddFluentValidationService( Configuration );
            services.AddTokenService( Configuration );
            services.AddCorsService( Configuration );
            services.AddCacheService( Configuration );
            services.AddOtherService( Configuration );

        }



        public void Configure ( IApplicationBuilder app , IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            // ȫ���쳣����
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

                        var result = new ServiceResult<string>() { Result = string.Empty };
                        result.IsFailed( errormsg );

                        string jsonerror = System.Text.Json.JsonSerializer.Serialize( result );

                        // �������ͺ�״̬��
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync( jsonerror );
                    }
                } );
            } );

            CorsConfigData _CorsConfigData = this.Configuration.GetSection( "CorsInfo" ).Get<CorsConfigData>();
            //��������
            string PolicyName = _CorsConfigData.name;
            app.UseCors( PolicyName );

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
