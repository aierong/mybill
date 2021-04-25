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



        public bool Save ( bills bill )
        {
            var ids = db.Insertable( bill ).ExecuteReturnIdentity();

            return ids > 0;
        }


    }
}
