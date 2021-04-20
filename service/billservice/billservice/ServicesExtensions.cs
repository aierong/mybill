using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace billservice
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddService ( this IServiceCollection services , IConfiguration configuration )
        {
            #region db

            SqlSugarClient db = new SqlSugarClient( new ConnectionConfig()
            {
                ConnectionString = "Server=192.168.8.156;Database=ERPDATANEW;User Id=erpadmin;Password=voionit;" ,//连接符字串
                DbType = DbType.SqlServer ,
                IsAutoCloseConnection = true
            } );

            services.AddSingleton<SqlSugarClient>( db );

            #endregion



            return services;
        }
    }
}
