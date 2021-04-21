using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Models;
using SqlSugar;

namespace billservice.Services
{


    public class UserService : IUser
    {
        SqlSugarClient db;

        public UserService ( SqlSugarClient db )
        {
            this.db = db;
        }



        public bool IsExistUser ( string mobile )
        {
            //var getFirst = db.Queryable<users>().First( it => it.mobile == mobile );

            //if ( getFirst == null )
            //{
            //    return false;
            //}

            var isAny = db.Queryable<users>().Where( it => it.mobile == mobile ).Any();

            if ( isAny )
            {
                return true;
            }

            return false;
        }


    }
}
