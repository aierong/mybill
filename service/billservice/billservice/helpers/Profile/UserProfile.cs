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
            CreateMap<UserDto , users>()
                .ForMember( destination => destination.password , opt => opt.MapFrom( src => new MD5Security().Encrypt( src.password ) ) )
               .AfterMap( ( src , dest ) =>
               {
                   DateTime now = DateTime.Now;

                   dest.lastlogindate = null;
                   dest.logintimes = 0;

                   dest.adddate = now;
                   dest.updatedate = null;
                   dest.deletedate = null;

                   dest.delmark = Constant.N;

                   dest.rolename = userrole.user.ToString();
               } );



        }
    }
}
