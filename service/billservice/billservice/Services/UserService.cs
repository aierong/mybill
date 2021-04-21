using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


            return true;
        }


    }
}
