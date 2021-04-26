using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.interfaces;
using billservice.models;
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



        public bool IsExistId ( int id , string mobile )
        {
            var isAny = db.Queryable<bills>().Where( it => it.ids == id
                                                                                && it.mobile == mobile ).Any();

            return isAny;
        }



        public bool Save ( bills bill )
        {
            var ids = db.Insertable( bill ).ExecuteReturnIdentity();

            return ids > 0;
        }


    }
}
