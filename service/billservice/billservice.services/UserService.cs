﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.interfaces;
using billservice.models;
 
using SqlSugar;

namespace billservice.services
{


    public class UserService : IUser
    {
        readonly SqlSugarClient db;

        public UserService ( SqlSugarClient db )
        {
            this.db = db;
        }



        public users GetUser ( string mobile )
        {
            var model = db.Queryable<users>().First( it => it.mobile == mobile );

            return model;
        }



        public bool IsExistEmail ( string email )
        {
            var isAny = db.Queryable<users>().Where( it => it.email == email ).Any();

            if ( isAny )
            {
                return true;
            }

            return false;
        }



        public bool IsExistUser ( string mobile )
        {

            var isAny = db.Queryable<users>().Where( it => it.mobile == mobile ).Any();

            if ( isAny )
            {
                return true;
            }

            return false;
        }



        public bool Save ( users user )
        {
            var ids = db.Insertable( user ).ExecuteReturnIdentity();

            return ids > 0;
        }



    }
}