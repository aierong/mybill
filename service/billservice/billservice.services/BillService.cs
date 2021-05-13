using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.interfaces;
using billservice.models;
using billservice.models.Dto;

namespace billservice.services
{
    public class BillService : IBill
    {
        readonly IFreeSql fsql;



        public BillService ( IFreeSql fsql )
        {
            this.fsql = fsql;
        }



        public bool IsExistId ( int id , string mobile )
        {
            //var isAny = db.Queryable<bills>().Where( it => it.ids == id
            //                                                                    && it.mobile == mobile ).Any();

            //return isAny;

            var isAny = fsql.Select<bills>().Where( it => it.ids == id
                                                                                 && it.mobile == mobile ).Any();
            return isAny;
        }



        public bool Save ( bills bill )
        {
            //var ids = db.Insertable( bill ).ExecuteReturnIdentity();

            //return ids > 0;

            var ids = fsql.Insert( bill ).ExecuteAffrows();

            return ids > 0;
        }



        public async Task<bool> SaveAsync ( bills bill )
        {
            //var ids = await db.Insertable( bill ).ExecuteReturnIdentityAsync();

            //return ids > 0;

            var ids = await fsql.Insert( bill ).ExecuteAffrowsAsync();

            return ids > 0;
        }



        public bool Update ( bills bill )
        {
            //var result = db.Updateable( bill ).UpdateColumns( it => new { it.billtypeid , it.isout , it.moneys , it.moneydate , it.memo , it.updatedate } ).ExecuteCommand();

            //return result > 0;

            var result = fsql.Update<bills>()
                         .Set( a => a.billtypeid , bill.billtypeid )
                         .Set( a => a.isout , bill.isout )
                         .Set( a => a.moneys , bill.moneys )
                         .Set( a => a.moneydate , bill.moneydate )
                         .Set( a => a.memo , bill.memo )
                         .Set( a => a.updatedate , bill.updatedate )
                         .Where( it => it.ids == bill.ids )
                         .ExecuteAffrows();

            return result > 0;
        }



        public async Task<bool> UpdateAsync ( bills bill )
        {
            //var result = await db.Updateable( bill ).UpdateColumns( it => new { it.billtypeid , it.isout , it.moneys , it.moneydate , it.memo , it.updatedate } ).ExecuteCommandAsync();

            //return result > 0;

            var result = await fsql.Update<bills>()
                         .Set( a => a.billtypeid , bill.billtypeid )
                         .Set( a => a.isout , bill.isout )
                         .Set( a => a.moneys , bill.moneys )
                         .Set( a => a.moneydate , bill.moneydate )
                         .Set( a => a.memo , bill.memo )
                         .Set( a => a.updatedate , bill.updatedate )
                         .Where( it => it.ids == bill.ids )
                         .ExecuteAffrowsAsync();

            return result > 0;
        }



        public async Task<List<BillReturnDto>> GetListAsync ( string mobile , int year , int month , int billtypeid )
        {


            //string sql = @"SELECT      bt.typename ,
            //                        bt.avatar ,
            //                        b.ids ,
            //                        b.mobile ,
            //                        b.billtypeid ,
            //                        b.isout ,
            //                        b.moneys ,
            //                        b.moneydate ,
            //                        b.moneyyear ,
            //                        b.moneymonth ,
            //                        b.moneyday ,
            //                        b.memo ,
            //                        b.sources ,
            //                        b.adddate ,
            //                        b.updatedate ,
            //                        b.deletedate ,
            //                        b.delmark
            //            FROM        dbo.bills AS b
            //            JOIN   dbo.billtype AS bt
            //            ON  b.billtypeid = bt.ids 
            //            WHERE b.mobile=@mobile 
            //                AND b.moneyyear =@moneyyear AND b.moneymonth=@moneymonth 
            //                AND b.delmark='N'
            //                {0}
            //            ORDER BY b.moneydate DESC,b.adddate desc
            //            ";

            //sql = string.Format( sql , billtypeid > 0 ? string.Format( " and b.billtypeid={0}" , billtypeid ) : string.Empty );

            //var dt = await db.Ado.SqlQueryAsync<BillReturnDto>( sql ,
            //          new
            //          {
            //              mobile = mobile ,
            //              moneyyear = year ,
            //              moneymonth = month ,

            //          } );

            //return dt;

            string sql = @"SELECT      bt.typename ,
                                    bt.avatar ,
                                    b.ids ,
                                    b.mobile ,
                                    b.billtypeid ,
                                    b.isout ,
                                    b.moneys ,
                                    b.moneydate ,
                                    b.moneyyear ,
                                    b.moneymonth ,
                                    b.moneyday ,
                                    b.memo ,
                                    b.sources ,
                                    b.adddate ,
                                    b.updatedate ,
                                    b.deletedate ,
                                    b.delmark
                        FROM        bills AS b
                        JOIN   billtype AS bt
                        ON  b.billtypeid = bt.ids 

                        WHERE b.mobile = @mobile 
                            AND b.moneyyear = @moneyyear AND b.moneymonth= @moneymonth 
                            AND b.delmark='N'

                            {0}

                        ORDER BY b.moneydate DESC,b.adddate desc
                        ";

            sql = string.Format( sql , billtypeid > 0 ? string.Format( " and b.billtypeid={0}" , billtypeid ) : string.Empty );

            List<BillReturnDto> dt = await fsql.Ado.QueryAsync<BillReturnDto>( sql ,
                new
                {
                    mobile = mobile ,
                    moneyyear = year ,
                    moneymonth = month ,

                } );

            return dt;

        }


        //   public List<BillReturnDto> GetList ( string mobile , int year , int month , int billtypeid )
        //   {
        //       var list = db.Queryable<bills , billtype>( ( b , bt ) => new JoinQueryInfos(
        //JoinType.Inner , b.billtypeid == bt.ids
        //     ) )
        //           .Where( b => b.mobile == mobile
        //                               && b.moneyyear == year && b.moneymonth == month
        //                               && b.delmark == "N" )
        //           .WhereIF( billtypeid > 0 , b => b.billtypeid == billtypeid )
        //           .OrderBy( b => b.moneydate , OrderByType.Desc )
        //           .OrderBy( b => b.adddate , OrderByType.Desc )
        //           .Select( ( b , bt ) => new BillReturnDto// 是一个新类
        //           {
        //               typename = bt.typename ,
        //               avatar = bt.avatar ,

        //               ids = b.ids ,
        //               billtypeid = b.billtypeid ,
        //               isout = b.isout ,
        //               memo = b.memo ,
        //               mobile = b.mobile ,
        //               moneys = b.moneys ,
        //               delmark = b.delmark ,
        //               sources = b.sources ,
        //               adddate = b.adddate ,
        //               deletedate = b.deletedate ,

        //               moneydate = b.moneydate ,
        //               moneyyear = b.moneyyear ,
        //               moneymonth = b.moneymonth ,
        //               moneyday = b.moneyday ,

        //               updatedate = b.updatedate
        //           } ).ToList();

        //       return list;

        //   }


        //public async Task<bills> GetAsync ( int id )
        //{
        //    var model = await db.Queryable<bills>().FirstAsync( it => it.ids == id );

        //    return model;
        //}


    }
}
