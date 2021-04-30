using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.models;
using billservice.models.Dto;
 

namespace billservice.Profile
{
    public class BillProfile : AutoMapper.Profile
    {
        public BillProfile ()
        {
            CreateMap<BillDto , bills>()
                //.ForMember( destination => destination.mobile , opt => opt.MapFrom<BillMobileResolver>() )
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
                    }
                    else
                    {
                        dest.updatedate = now;
                    }

                    dest.mobile = context.Items["mobile"] != null ? context.Items["mobile"].ToString() : string.Empty;
                } );


        }
    }
}
