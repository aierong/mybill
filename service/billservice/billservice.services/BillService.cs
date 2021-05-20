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



    }
}
