using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.models;
using billservice.models.Dto;
using billservice.Profile.Resolver;

namespace billservice.Profile
{
    public class BillProfile : AutoMapper.Profile
    {
        public BillProfile ()
        {
            CreateMap<BillDto , bills>()
                .ForMember( destination => destination.mobile , opt => opt.MapFrom<BillMobileResolver>() )
                .AfterMap( ( src , dest ) =>
                {
                    DateTime now = DateTime.Now;


                } );
        }
    }
}
