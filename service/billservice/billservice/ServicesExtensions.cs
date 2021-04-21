using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Helpers.Validator;
using billservice.Models.Dto;
using FluentValidation;
using FluentValidation.AspNetCore;
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




            return services;
        }
    }
}
