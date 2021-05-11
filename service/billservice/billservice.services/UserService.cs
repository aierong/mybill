using System;
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



        public async Task<users> GetUserAsync ( string mobile )
        {
            var model = await db.Queryable<users>().FirstAsync( it => it.mobile == mobile );

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



        public async Task<bool> SaveAsync ( users user )
        {
            var ids = await db.Insertable( user ).ExecuteReturnIdentityAsync();

            return ids > 0;
        }



        public bool UpdateAvatar ( string mobile , string avatar )
        {
            //var result = db.Updateable( updateObj ).UpdateColumns( it => new { it.Name , it.CreateTime } ).ExecuteCommand();

            var result = db.Updateable<users>()
                    .SetColumns( it => new users() { avatar = avatar , updatedate = DateTime.Now } )
                    .Where( it => it.mobile == mobile )
                    .ExecuteCommand();

            return result > 0;
        }



        public async Task<bool> UpdateAvatarAsync ( string mobile , string avatar )
        {
            //var result = db.Updateable( updateObj ).UpdateColumns( it => new { it.Name , it.CreateTime } ).ExecuteCommand();

            var result = await db.Updateable<users>()
                    .SetColumns( it => new users() { avatar = avatar , updatedate = DateTime.Now } )
                    .Where( it => it.mobile == mobile )
                    .ExecuteCommandAsync();

            return result > 0;
        }



        public bool UpdateLoginInfo ( string mobile , int logintimes )
        {
            var result = db.Updateable<users>()
                    .SetColumns( it => new users() { logintimes = logintimes , lastlogindate = DateTime.Now } )
                    .Where( it => it.mobile == mobile )
                    .ExecuteCommand();

            return result > 0;
        }



        public async Task<bool> UpdateLoginInfoAsync ( string mobile , int logintimes )
        {
            var result = await db.Updateable<users>()
                    .SetColumns( it => new users() { logintimes = logintimes , lastlogindate = DateTime.Now } )
                    .Where( it => it.mobile == mobile )
                    .ExecuteCommandAsync();

            return result > 0;
        }



        public bool UpdatePassWord ( string mobile , string pwd )
        {
            var result = db.Updateable<users>()
                    .SetColumns( it => new users() { password = pwd , updatedate = DateTime.Now } )
                    .Where( it => it.mobile == mobile )
                    .ExecuteCommand();

            return result > 0;
        }


        public async Task<bool> UpdatePassWordAsync ( string mobile , string pwd )
        {
            var result = await db.Updateable<users>()
                    .SetColumns( it => new users() { password = pwd , updatedate = DateTime.Now } )
                    .Where( it => it.mobile == mobile )
                    .ExecuteCommandAsync();

            return result > 0;
        }


    }
}

