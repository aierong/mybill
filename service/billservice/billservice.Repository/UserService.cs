﻿using System;
using System.Threading.Tasks;
using billservice.IRepository;
using billservice.models;



namespace billservice.Repository
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserService : IUser
    {
        readonly IFreeSql fsql;

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="fsql"></param>
        public UserService ( IFreeSql fsql )
        {
            this.fsql = fsql;
        }



        public users GetUser ( string mobile )
        {
            var model = fsql.Select<users>().Where( it => it.mobile == mobile ).ToOne();

            return model;
        }



        public async Task<users> GetUserAsync ( string mobile )
        {
            var model = await fsql.Select<users>().Where( it => it.mobile == mobile ).ToOneAsync();

            return model;
        }



        public bool IsExistEmail ( string email )
        {
            var isAny = fsql.Select<users>().Where( it => it.email == email ).Any();

            return isAny;
        }



        public bool IsExistUser ( string mobile )
        {
            var isAny = fsql.Select<users>().Where( it => it.mobile == mobile ).Any();

            return isAny;
        }



        public bool Save ( users user )
        {
            var ids = fsql.Insert( user ).ExecuteAffrows();

            return ids > 0;
        }



        public async Task<bool> SaveAsync ( users user )
        {
            var ids = await fsql.Insert( user ).ExecuteAffrowsAsync();

            return ids > 0;
        }



        public bool UpdateAvatar ( string mobile , string avatar )
        {
            var result = fsql.Update<users>()
                          .Set( a => a.avatar , avatar )
                          .Set( a => a.updatedate , DateTime.Now )
                          .Where( it => it.mobile == mobile )
                          .ExecuteAffrows();

            return result > 0;
        }



        public async Task<bool> UpdateAvatarAsync ( string mobile , string avatar )
        {
            var result = await fsql.Update<users>()
                          .Set( a => a.avatar , avatar )
                          .Set( a => a.updatedate , DateTime.Now )
                          .Where( it => it.mobile == mobile )
                          .ExecuteAffrowsAsync();

            return result > 0;
        }



        public bool UpdateLoginInfo ( string mobile , int logintimes )
        {
            var result = fsql.Update<users>()
                          .Set( a => a.logintimes , logintimes )
                          .Set( a => a.lastlogindate , DateTime.Now )
                          .Where( it => it.mobile == mobile )
                          .ExecuteAffrows();

            return result > 0;
        }



        public async Task<bool> UpdateLoginInfoAsync ( string mobile , int logintimes )
        {
            var result = await fsql.Update<users>()
                          .Set( a => a.logintimes , logintimes )
                          .Set( a => a.lastlogindate , DateTime.Now )
                          .Where( it => it.mobile == mobile )
                          .ExecuteAffrowsAsync();

            return result > 0;
        }



        public bool UpdatePassWord ( string mobile , string pwd )
        {
            var result = fsql.Update<users>()
                          .Set( a => a.password , pwd )
                          .Set( a => a.updatedate , DateTime.Now )
                          .Where( it => it.mobile == mobile )
                          .ExecuteAffrows();

            return result > 0;
        }



        public async Task<bool> UpdatePassWordAsync ( string mobile , string pwd )
        {
            var result = await fsql.Update<users>()
                          .Set( a => a.password , pwd )
                          .Set( a => a.updatedate , DateTime.Now )
                          .Where( it => it.mobile == mobile )
                          .ExecuteAffrowsAsync();

            return result > 0;
        }


    }
}

