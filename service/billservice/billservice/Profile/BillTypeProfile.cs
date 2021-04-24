using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.models;
using billservice.models.Dto;
using billservice.Profile.Resolver;

namespace billservice.Profile
{
    public class BillTypeProfile : AutoMapper.Profile
    {
        public BillTypeProfile ()
        {
            CreateMap<BillTypeDto , billtype>()
               .ForMember( destination => destination.mobile , opt => opt.MapFrom<BillTypeMobileResolver>() )
               .AfterMap( ( src , dest ) =>
               {
                   DateTime now = DateTime.Now;

                   dest.issystemtype = false;

                   dest.avatar = ""; // 用户加的类型,有一个固定头像


                   dest.adddate = now;
                   dest.updatedate = null;
                   dest.deletedate = null;

               } );



        }
    }
}
