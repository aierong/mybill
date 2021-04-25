using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.interfaces;
using SqlSugar;

namespace billservice.services
{
    public class BillService : IBill
    {
        readonly SqlSugarClient db;

        public BillService ( SqlSugarClient db )
        {
            this.db = db;
        }

        //public bool IsExist ( int billtypeid , bool isout )
        //{
        //    var isAny = db.Queryable<billtype>().Where( it => it.typename == typename
        //                                                                        && it.mobile == mobile
        //                                                                        && it.issystemtype == false ).Any();

        //}
    }
}
