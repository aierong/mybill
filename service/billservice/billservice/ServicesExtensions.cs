using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Helpers.Result;
using billservice.Helpers.Validator;
using billservice.Models.Dto;
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
        public static IServiceCollection AddService ( this IServiceCollection services , IConfiguration configuration )
        {
            #region 数据库

            SqlSugarClient db = new SqlSugarClient( new ConnectionConfig()
            {
                ConnectionString = "Server=192.168.8.156;Database=ERPDATANEW;User Id=erpadmin;Password=voionit;" ,//连接符字串
                DbType = DbType.SqlServer ,
                IsAutoCloseConnection = true
            } );

            services.AddSingleton<SqlSugarClient>( db );

            #endregion


            #region  FluentValidation

            services.AddMvc().AddFluentValidation();

            services.AddTransient<IValidator<UserDto> , UserDtoValidator>();

            #endregion



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
    }
}
