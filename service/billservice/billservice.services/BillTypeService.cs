using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.interfaces;
using billservice.models;


namespace billservice.services
{
    /// <summary>
    /// 
    /// </summary>
    public class BillTypeService : IBillType
    {
        readonly IFreeSql fsql;

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="fsql"></param>
        public BillTypeService ( IFreeSql fsql )
        {
            this.fsql = fsql;
        }



        public bool IsExistName ( string typename , string mobile )
        {

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

            var isAny = fsql.Select<billtype>().Where( it => it.ids == billtypeid
                                                                                && it.isout == isout ).Any();

            return isAny;
        }



        public bool IsExistUserType ( int billtypeid , string mobile )
        {

            var isAny = fsql.Select<billtype>().Where( it => it.ids == billtypeid
                                                                                && it.issystemtype == false
                                                                                && it.mobile == mobile ).Any();

            return isAny;
        }



        public bool IsExistSystemType ( int billtypeid )
        {

            var isAny = fsql.Select<billtype>().Where( it => it.ids == billtypeid
                                                                                && it.issystemtype == true ).Any();

            return isAny;
        }



        public bool Save ( billtype _billtype )
        {

            var ids = fsql.Insert( _billtype ).ExecuteAffrows();

            return ids > 0;
        }



        public async Task<bool> SaveAsync ( billtype _billtype )
        {

            var ids = await fsql.Insert( _billtype ).ExecuteAffrowsAsync();

            return ids > 0;
        }



        public async Task<List<billtype>> GetAllSystemTypeAsync ()
        {

            var list = await fsql.Select<billtype>().Where( it => it.issystemtype == true ).ToListAsync();

            return list;
        }



        public async Task<List<billtype>> GetAllUserTypeAsync ( string mobile )
        {

            var list = await fsql.Select<billtype>().Where( it => it.issystemtype == false && it.mobile == mobile ).ToListAsync();

            return list;
        }



    }
}
