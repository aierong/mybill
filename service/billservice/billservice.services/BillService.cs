using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
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
            var isAny = fsql.Select<bills>().Where( it => it.ids == id
                                                                                 && it.mobile == mobile ).Any();
            return isAny;
        }



        public async Task<bool> IsExistIdAsync ( int id , string mobile )
        {
            var isAny = await fsql.Select<bills>().Where( it => it.ids == id
                                                                                 && it.mobile == mobile ).AnyAsync();

            return isAny;
        }


        public bool Save ( bills bill )
        {

            var ids = fsql.Insert( bill ).ExecuteAffrows();

            return ids > 0;
        }



        public async Task<bool> SaveAsync ( bills bill )
        {

            var ids = await fsql.Insert( bill ).ExecuteAffrowsAsync();

            return ids > 0;
        }



        public bool Update ( bills bill )
        {

            var result = fsql.Update<bills>()
                         .Set( a => a.billtypeid , bill.billtypeid )
                         .Set( a => a.isout , bill.isout )
                         .Set( a => a.moneys , bill.moneys )
                         .Set( a => a.moneydate , bill.moneydate )
                         .Set( a => a.moneyyear , bill.moneyyear )
                         .Set( a => a.moneymonth , bill.moneymonth )
                         .Set( a => a.moneyday , bill.moneyday )
                         .Set( a => a.memo , bill.memo )
                         .Set( a => a.updatedate , bill.updatedate )
                         .Where( it => it.ids == bill.ids )
                         .ExecuteAffrows();

            return result > 0;
        }



        public async Task<bool> UpdateAsync ( bills bill )
        {

            var result = await fsql.Update<bills>()
                         .Set( a => a.billtypeid , bill.billtypeid )
                         .Set( a => a.isout , bill.isout )
                         .Set( a => a.moneys , bill.moneys )
                         .Set( a => a.moneydate , bill.moneydate )
                         .Set( a => a.moneyyear , bill.moneyyear )
                         .Set( a => a.moneymonth , bill.moneymonth )
                         .Set( a => a.moneyday , bill.moneyday )
                         .Set( a => a.memo , bill.memo )
                         .Set( a => a.updatedate , bill.updatedate )
                         .Where( it => it.ids == bill.ids )
                         .ExecuteAffrowsAsync();

            return result > 0;
        }



        public async Task<List<BillReturnDto>> GetListAsync ( string mobile , int year , int month , int billtypeid )
        {

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



        public async Task<List<BillReturnDto>> GetListAsync ( string mobile , int year , int month , bool isout , int billtypeid , string mode )
        {
            // billtypeid大于等于0 ,这样就查询所有数据,但是要判断一下类型,看是出还是入

            string sql = @"SELECT    bt.typename ,
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
                            
                            AND bt.isout=@isout
                            

                            -- 类型筛选
                            #sx#

                        -- 按金额排行
                            #or#
                        ";

            // money按金额 date按日期
            sql = sql.Replace( "#or#" , "money".Equals( mode , StringComparison.OrdinalIgnoreCase ) ? " ORDER BY b.moneys DESC " : ( "date".Equals( mode , StringComparison.OrdinalIgnoreCase ) ? " ORDER BY b.moneydate DESC " : string.Empty ) )
                .Replace( "#sx#" , billtypeid <= 0 ? string.Empty : string.Format( "  and b.billtypeid={0}" , billtypeid ) );


            List<BillReturnDto> dt = await fsql.Ado.QueryAsync<BillReturnDto>( sql ,
                new
                {

                    mobile = mobile ,
                    moneyyear = year ,
                    moneymonth = month ,
                    isout = isout

                } );

            return dt;

        }



        public async Task<BillReturnDto> GetAsync ( int id )
        {
            string sql = @"SELECT  top 1    bt.typename ,
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

                        WHERE  b.ids = @ids                                                 ";

            List<BillReturnDto> dt = await fsql.Ado.QueryAsync<BillReturnDto>( sql ,
                new
                {

                    ids = id ,

                } );

            if ( dt != null && dt.Count > 0 )
            {
                return dt[0];  //取第1个回去
            }

            return null;

        }



        public async Task<bool> DeleteAsync ( int id )
        {
            var result = await fsql.Update<bills>()
                          .Set( a => a.delmark , "Y" )
                          .Set( a => a.deletedate , DateTime.Now )
                          .Where( it => it.ids == id )
                          .ExecuteAffrowsAsync();

            return result > 0;
        }



        public async Task<List<BillMapReturnDto>> GetStatListAsync ( string mobile , int year , int month , bool isout )
        {
            string sql = @"

SELECT      b.billtypeid ,
            MAX ( t.typename ) AS typename ,
            MAX ( t.avatar ) AS avatar ,
            SUM ( b.moneys ) AS moneys
INTO        #tem
FROM        dbo.bills AS b
JOIN        [dbo].[billtype] AS t
ON  b.billtypeid = t.ids
WHERE       b.moneyyear = @moneyyear
            AND b.moneymonth = @moneymonth
            AND b.[delmark] = 'N'
            AND t.isout = @isout
            AND b.mobile= @mobile
GROUP BY    b.billtypeid

SELECT      billtypeid ,
            typename ,
            avatar ,
            moneys ,
            --SUM(moneys) over() as sum,
            CONVERT ( MONEY, moneys / SUM ( moneys ) OVER ()) AS ratio
FROM        #tem
ORDER BY    moneys DESC

DROP TABLE #tem";

            List<BillMapReturnDto> dt = await fsql.Ado.QueryAsync<BillMapReturnDto>( sql ,
                new
                {
                    mobile = mobile ,
                    moneyyear = year ,
                    moneymonth = month ,
                    isout = isout
                } );

            return dt;

        }



        public async Task<List<BillReturnDto>> GetTopOutListAsync ( string mobile , int year , int month , int topnum )
        {

            string sql = @"SELECT  TOP ( @num )  bt.typename ,
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
                            
                             AND bt.isout=@isout

                        -- 按金额排行
                        ORDER BY b.moneys DESC 
                        ";



            List<BillReturnDto> dt = await fsql.Ado.QueryAsync<BillReturnDto>( sql ,
                new
                {
                    num = topnum ,
                    mobile = mobile ,
                    moneyyear = year ,
                    moneymonth = month ,
                    isout = true
                } );

            return dt;

        }





        public async Task<int> GetOutListCountAsync ( string mobile , int year , int month )
        {
            string sql = @"SELECT    COUNT ( 1 ) AS counts
                        FROM        bills AS b
                        JOIN   billtype AS bt
                        ON  b.billtypeid = bt.ids 

                        WHERE b.mobile = @mobile 
                            AND b.moneyyear = @moneyyear AND b.moneymonth= @moneymonth 
                            AND b.delmark='N'
                            
                            AND bt.isout=@isout                                                 ";


            DataTable tb = await fsql.Ado.ExecuteDataTableAsync( sql , new
            {

                mobile = mobile ,
                moneyyear = year ,
                moneymonth = month ,
                isout = true
            } );

            if ( tb != null && tb.Rows != null && tb.Rows.Count > 0 )
            {
                DataRow row = tb.Rows[0];

                return row[0] != System.DBNull.Value ? Convert.ToInt32( row[0] ) : 0;
            }

            return 0;

        }



        public async Task<List<BillSumMonthReturnDto>> GetSumByMonthAsync ( string mobile , bool isout , int monthnum )
        {
            DateTime now = DateTime.Now;  //获取最新时间

            Dictionary<int , int> dictionary = new Dictionary<int , int>();
            dictionary.Add( now.Year , now.Month );

            for ( int i = 1 ; i < monthnum - 1 ; i++ )
            {
                DateTime q = now.AddMonths( i * -1 );

                dictionary.Add( q.Year , q.Month );
            }


            return null;
        }
    }
}


/**
 SELECT 
      [moneyyear]
      ,[moneymonth]
      
      ,sum([moneys])
  FROM [mybill].[dbo].[bills]
  where delmark='N'
  --and [mobile]=''
  and (  ([moneyyear]=2021 and [moneymonth]=5) or  ([moneyyear]=2021 and [moneymonth]=4) )
  group by [moneyyear] ,[moneymonth]
*/