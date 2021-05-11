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



        public async Task<List<BillReturnDto>> GetListAsync ( string mobile , int year , int month , int billtypeid )
        {
            var list = await db.Queryable<bills , billtype>( ( b , bt ) => new JoinQueryInfos(
       JoinType.Inner , b.billtypeid == bt.ids
            ) )
                .Where( b => b.mobile == mobile
                                    && b.moneyyear == year && b.moneymonth == month
                                    && b.delmark == "N" )
                .WhereIF( billtypeid > 0 , b => b.billtypeid == billtypeid )
                .OrderBy( b => b.moneydate , OrderByType.Desc )
                .OrderBy( b => b.adddate , OrderByType.Desc )
                .Select( ( b , bt ) => new BillReturnDto// 是一个新类
                {
                    typename = bt.typename ,
                    avatar = bt.avatar ,

                    ids = b.ids ,
                    billtypeid = b.billtypeid ,
                    isout = b.isout ,
                    memo = b.memo ,
                    mobile = b.mobile ,
                    moneys = b.moneys ,
                    delmark = b.delmark ,
                    sources = b.sources ,
                    adddate = b.adddate ,
                    deletedate = b.deletedate ,

                    moneydate = b.moneydate ,
                    moneyyear = b.moneyyear ,
                    moneymonth = b.moneymonth ,
                    moneyday = b.moneyday ,

                    updatedate = b.updatedate
                } ).ToListAsync();

            return list;

        }


        public List<BillReturnDto> GetList ( string mobile , int year , int month , int billtypeid )
        {
            var list = db.Queryable<bills , billtype>( ( b , bt ) => new JoinQueryInfos(
     JoinType.Inner , b.billtypeid == bt.ids
          ) )
                .Where( b => b.mobile == mobile
                                    && b.moneyyear == year && b.moneymonth == month
                                    && b.delmark == "N" )
                .WhereIF( billtypeid > 0 , b => b.billtypeid == billtypeid )
                .OrderBy( b => b.moneydate , OrderByType.Desc )
                .OrderBy( b => b.adddate , OrderByType.Desc )
                .Select( ( b , bt ) => new BillReturnDto// 是一个新类
                {
                    typename = bt.typename ,
                    avatar = bt.avatar ,

                    ids = b.ids ,
                    billtypeid = b.billtypeid ,
                    isout = b.isout ,
                    memo = b.memo ,
                    mobile = b.mobile ,
                    moneys = b.moneys ,
                    delmark = b.delmark ,
                    sources = b.sources ,
                    adddate = b.adddate ,
                    deletedate = b.deletedate ,

                    moneydate = b.moneydate ,
                    moneyyear = b.moneyyear ,
                    moneymonth = b.moneymonth ,
                    moneyday = b.moneyday ,

                    updatedate = b.updatedate
                } ).ToList();

            return list;

        }


        //public async Task<bills> GetAsync ( int id )
        //{
        //    var model = await db.Queryable<bills>().FirstAsync( it => it.ids == id );

        //    return model;
        //}


    }
}
