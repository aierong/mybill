using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Helpers.Result;
using billservice.Helpers.Validator;
using billservice.Models.Dto;
using billservice.Services;
using billservice.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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



    }
}
