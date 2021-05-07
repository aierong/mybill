using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Helpers;
using billservice.models;
using billservice.models.Dto;


namespace billservice.Profile
{
    public class BillProfile : AutoMapper.Profile
    {
        public BillProfile ()
        {
            CreateMap<BillDto , bills>()
                
                .AfterMap( ( src , dest , context ) =>
                {
                    DateTime now = DateTime.Now;

                    if ( src.isadd )
                    {
                        dest.sources = string.Empty;

                        dest.adddate = now;
                        dest.updatedate = null;
                        dest.deletedate = null;

                        dest.delmark = Helpers.Constant.N;

                        DateTime date = Convert.ToDateTime( src.moneydate )  ;

                        dest.moneyyear = date.Year;
                        dest.moneymonth = date.Month;
                        dest.moneyday = date.Day;
                    }
                    else
                    {
                        dest.updatedate = now;
                    }

                    dest.mobile = context.Items[Constant.mobilename] != null ? context.Items[Constant.mobilename].ToString() : string.Empty;
                } );


        }
    }
}
