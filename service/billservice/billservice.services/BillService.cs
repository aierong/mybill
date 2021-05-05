using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.interfaces;
using billservice.models;
using billservice.models.Dto;
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



        public async Task<bool> SaveAsync ( bills bill )
        {
            var ids = await db.Insertable( bill ).ExecuteReturnIdentityAsync();

            return ids > 0;
        }



        public bool Update ( bills bill )
        {
            var result = db.Updateable( bill ).UpdateColumns( it => new { it.billtypeid , it.isout , it.moneys , it.moneydate , it.memo , it.updatedate } ).ExecuteCommand();

            return result > 0;
        }



        public async Task<bool> UpdateAsync ( bills bill )
        {
            var result = await db.Updateable( bill ).UpdateColumns( it => new { it.billtypeid , it.isout , it.moneys , it.moneydate , it.memo , it.updatedate } ).ExecuteCommandAsync();

            return result > 0;
        }



        public async Task<List<BillReturnDto>> GetListAsync ( string mobile , string moneydate , int billtypeid )
        {
            var oneClass = await db.Queryable<bills , billtype>( ( b , bt ) => new JoinQueryInfos(
       JoinType.Inner , b.billtypeid == bt.ids
            ) )
                .Where( b => b.mobile == mobile && b.moneydate == moneydate )
                .WhereIF( billtypeid > 0 , b => b.billtypeid == billtypeid )
                .Select( ( o , bt ) => new BillReturnDto// 是一个新类
                {

                    typename = bt.typename ,
                    avatar = bt.avatar ,

                    ids = o.ids ,
                    billtypeid = o.billtypeid ,
                    isout = o.isout ,
                    memo = o.memo ,
                    mobile = o.mobile ,
                    moneys = o.moneys ,
                    delmark = o.delmark ,
                    sources = o.sources ,
                    adddate = o.adddate ,
                    deletedate = o.deletedate ,
                    moneydate = o.moneydate ,
                    updatedate = o.updatedate
                } ).ToListAsync();

            return oneClass;

        }



    }
}
