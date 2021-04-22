using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using billservice.Helpers.Result;
using billservice.Helpers.Validator;
using billservice.Models.ConfigData;
using billservice.Models.Dto;
using billservice.Services;
using billservice.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SqlSugar;

namespace billservice
{
    public static class ServicesExtensions
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

            return services;
        }



        /// <summary>
        /// 数据库的相关服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDBService ( this IServiceCollection services , IConfiguration configuration )
        {
            #region 数据库

            ////连接符字串
            string constr = configuration["DbConnectionString"];

            SqlSugarClient db = new SqlSugarClient( new ConnectionConfig()
            {
                
                ConnectionString = constr  ,//连接符字串
                DbType = DbType.SqlServer ,
                IsAutoCloseConnection = true

            } );

            services.AddSingleton<SqlSugarClient>( db );

            #endregion

            services.AddSingleton<IUser , UserService>();
            services.AddSingleton<IBillType , BillTypeService>();
            services.AddSingleton<IBill , BillService>();

            return services;
        }



        /// <summary>
        /// FluentValidation的相关服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddFluentValidationService ( this IServiceCollection services , IConfiguration configuration )
        {
            

            services.AddMvc().AddFluentValidation();

            services.AddTransient<IValidator<UserDto> , UserDtoValidator>();
            services.AddTransient<IValidator<BillTypeDto> , BillTypeDtoValidator>();


            return services;
        }



        /// <summary>
        /// AutoMapper的相关服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapperService ( this IServiceCollection services , IConfiguration configuration )
        {
             
            services.AddAutoMapper( AppDomain.CurrentDomain.GetAssemblies() );          

            return services;
        }




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
                    //ValidateIssuerSigningKey = true ,
                    //IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( "1234567890123456key" ) ) ,

                    //ValidateIssuer = true ,
                    //ValidIssuer = "http://localhost:5000" ,

                    //ValidateAudience = true ,
                    //ValidAudience = "http://localhost:5001" ,

                    //ValidateLifetime = true ,

                    //ClockSkew = TimeSpan.FromMinutes( 5 )

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



    }
}
