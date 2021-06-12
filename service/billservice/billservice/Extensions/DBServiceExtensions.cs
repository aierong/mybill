using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.interfaces;
using billservice.services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace billservice.Extensions
{
    public static class DBServiceExtensions
    {
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



            IFreeSql fsql = new FreeSql.FreeSqlBuilder()
                       .UseConnectionString( FreeSql.DataType.SqlServer , constr )
                       //.UseAutoSyncStructure( true ) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
                       .Build(); //请务必定义成 Singleton 单例模式

            services.AddSingleton<IFreeSql>( fsql );

            #endregion



            services.AddSingleton<IUser , UserService>();
            services.AddSingleton<IBillType , BillTypeService>();
            services.AddSingleton<IBill , BillService>();

            return services;
        }
    }
}
