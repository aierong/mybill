using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Helpers;
using billservice.models;
using billservice.models.Dto;


namespace billservice.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile ()
        {
            CreateMap<UserAddDto , users>()
               .ForMember( destination => destination.password , opt => opt.MapFrom( src => MD5Security.Encrypt( src.password ) ) )
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

            CreateMap<users , UserDto>();

        }
    }
}
