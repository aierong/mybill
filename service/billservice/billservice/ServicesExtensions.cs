using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using billservice.Helpers;
using billservice.Helpers.ConfigData;
using billservice.Helpers.Result;
using billservice.Helpers.Validator;
using billservice.models.Dto;
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

                ConnectionString = constr ,//连接符字串
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
