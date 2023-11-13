using billservice.models.Dto;
using billservice.Validator;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using billservice.Helpers.ConfigData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace billservice.Extensions
{
    public static class ServiceExtensions
    {

        /// <summary>
        /// AutoMapper的相关服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapperService ( this IServiceCollection services )
        {

            services.AddAutoMapper( AppDomain.CurrentDomain.GetAssemblies() );

            return services;
        }


        /// <summary>
        /// FluentValidation的相关服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddFluentValidationService ( this IServiceCollection services )
        {

            //services.AddMvc().AddFluentValidation();
            services.AddFluentValidationAutoValidation( config =>
            {
                //执行FluentValidation后，任何其他验证器提供程序也将有机会执行。这意味着您可以将FluentValidation自动验证与DataAnnotations属性（或任何其他ASP.NET ModelValidatorProvider实现）混合使用。
                //以便FluentValidation是唯一执行的验证库
                config.DisableDataAnnotationsValidation = true;

                //config.ImplicitlyValidateChildProperties = true;
                //config.ImplicitlyValidateRootCollectionElements = true;
            } );  //新版本注册


            services.AddTransient<IValidator<UserAddDto> , UserAddDtoValidator>();
            services.AddTransient<IValidator<UserLoginDto> , UserLoginDtoValidator>();
            services.AddTransient<IValidator<BillTypeDto> , BillTypeDtoValidator>();
            services.AddTransient<IValidator<BillDto> , BillDtoValidator>();

            return services;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddTokenService ( this IServiceCollection services , IConfiguration configuration )
        {
            const string sectionname = "TokenInfo";

            services.Configure<TokenConfigData>( configuration.GetSection( sectionname ) );


            //把配置文件中，配置信息加载回来
            TokenConfigData _tokenconfigdata = configuration.GetSection( sectionname ).Get<TokenConfigData>();


            // 注册JwtBearer服务
            services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme ).AddJwtBearer( options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true ,  //是否验证Issuer
                    ValidateAudience = true ,//是否验证Audience
                    ValidateLifetime = true ,//是否验证失效时间
                    ValidateIssuerSigningKey = true ,//是否验证SecurityKey

                    ValidAudience = _tokenconfigdata.audience , // "http://localhost:5001" ,//Audience
                    ValidIssuer = _tokenconfigdata.issuer , // "http://localhost:5000" ,//Issuer，这两项和前面签发jwt的设置一致
                                                            //IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( "1234567890123456key" ) )//拿到SecurityKey
                    IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( _tokenconfigdata.key ) )//拿到SecurityKey

                };
            } );

            return services;
        }



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



        /// <summary>
        /// 注册cache服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCacheService ( this IServiceCollection services )
        {

            //注册cache服务
            services.AddMemoryCache();

            return services;
        }


    }
}
