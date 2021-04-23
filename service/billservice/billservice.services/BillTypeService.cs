using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.interfaces;
using billservice.models;
using SqlSugar;

namespace billservice.services
{

    public class BillTypeService : IBillType
    {
        readonly SqlSugarClient db;

        public BillTypeService ( SqlSugarClient db )
        {
            this.db = db;
        }



        public bool IsExistName ( string typename , string mobile )
        {
            // 先判断自己的类型是否重名
            var isAny = db.Queryable<billtype>().Where( it => it.typename == typename
                                                                                && it.mobile == mobile
                                                                                && it.issystemtype == false ).Any();

            if ( isAny )
            {
                return true;
            }

            // 再判断 是否于系统的名称是否重名
            isAny = db.Queryable<billtype>().Where( it => it.typename == typename
                                                                                && it.issystemtype == true ).Any();

            if ( isAny )
            {
                return true;
            }

            return false;
        }



        public bool Save ( billtype _billtype )
        {
            var ids = db.Insertable( _billtype ).ExecuteReturnIdentity();

            return ids > 0;
        }
    }
}
