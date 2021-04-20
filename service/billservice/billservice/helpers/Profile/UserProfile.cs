using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Models;
using billservice.Models.Dto;

namespace billservice.Helpers.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile ()
        {
            CreateMap<UserDto , users>();

            //CreateMap<UserDto , users>()
            //.ForMember( destination => destination.name , opt => opt.NullSubstitute( "null搞个默认值(搞个默认名字)" ) )
            //.ForMember( destination => destination.num , opt => opt.MapFrom( src => src.year - 2000 ) );


        }
    }
}
