using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.interfaces;
using billservice.models;
 

namespace billservice.services
{

    public class BillTypeService : IBillType
    {
        readonly IFreeSql fsql;

        public BillTypeService ( IFreeSql fsql )
        {
            this.fsql = fsql;
        }



        public bool IsExistName ( string typename , string mobile )
        {
            //// 先判断自己的类型是否重名
            //var isAny = db.Queryable<billtype>().Where( it => it.typename == typename
            //                                                                    && it.mobile == mobile
            //                                                                    && it.issystemtype == false ).Any();

            //if ( isAny )
            //{
            //    return true;
            //}

            //// 再判断 是否于系统的名称是否重名
            //isAny = db.Queryable<billtype>().Where( it => it.typename == typename
            //                                                                    && it.issystemtype == true ).Any();

            //if ( isAny )
            //{
            //    return true;
            //}

            //return false;






            // 先判断自己的类型是否重名
            var isAny = fsql.Select<billtype>().Where( it => it.typename == typename
                                                                                && it.mobile == mobile
                                                                                && it.issystemtype == false ).Any();

            if ( isAny )
            {
                return true;
            }

            // 再判断 是否于系统的名称是否重名
            isAny = fsql.Select<billtype>().Where( it => it.typename == typename
                                                                                && it.issystemtype == true ).Any();

            return isAny;
        }



        public bool IsExistType ( int billtypeid , bool isout )
        {
            //var isAny = db.Queryable<billtype>().Where( it => it.ids == billtypeid
            //                                                                    && it.isout == isout ).Any();


            //return isAny;

            var isAny = fsql.Select<billtype>().Where( it => it.ids == billtypeid
                                                                                && it.isout == isout ).Any();

            return isAny;
        }



        public bool IsExistUserType ( int billtypeid , string mobile )
        {
            //var isAny = db.Queryable<billtype>().Where( it => it.ids == billtypeid
            //                                                                    && it.issystemtype == false
            //                                                                    && it.mobile == mobile ).Any();

            //return isAny;


            var isAny = fsql.Select<billtype>().Where( it => it.ids == billtypeid
                                                                                && it.issystemtype == false
                                                                                && it.mobile == mobile ).Any();

            return isAny;
        }



        public bool IsExistSystemType ( int billtypeid )
        {
            //var isAny = db.Queryable<billtype>().Where( it => it.ids == billtypeid
            //                                                                    && it.issystemtype == true ).Any();

            //return isAny;

            var isAny = fsql.Select<billtype>().Where( it => it.ids == billtypeid
                                                                                && it.issystemtype == true ).Any();

            return isAny;
        }



        public bool Save ( billtype _billtype )
        {
            //var ids = db.Insertable( _billtype ).ExecuteReturnIdentity();

            //return ids > 0;

            var ids = fsql.Insert( _billtype ).ExecuteAffrows();

            return ids > 0;
        }



        public async Task<bool> SaveAsync ( billtype _billtype )
        {
            //var ids = await db.Insertable( _billtype ).ExecuteReturnIdentityAsync();

            //return ids > 0;

            var ids = await fsql.Insert( _billtype ).ExecuteAffrowsAsync();

            return ids > 0;
        }



        public async Task<List<billtype>> GetAllSystemTypeAsync ()
        {
            //var list = await db.Queryable<billtype>().Where( it => it.issystemtype == true ).ToListAsync();

            //return list;

            var list = await fsql.Select<billtype>().Where( it => it.issystemtype == true ).ToListAsync();

            return list;
        }



        public async Task<List<billtype>> GetAllUserTypeAsync ( string mobile )
        {
            //var list = await db.Queryable<billtype>().Where( it => it.issystemtype == false && it.mobile == mobile ).ToListAsync();

            //return list;


            var list = await fsql.Select<billtype>().Where( it => it.issystemtype == false && it.mobile == mobile ).ToListAsync();

            return list;
        }



    }
}
