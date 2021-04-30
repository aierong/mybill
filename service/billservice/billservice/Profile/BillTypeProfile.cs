using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Helpers;
using billservice.models;
using billservice.models.Dto;


namespace billservice.Profile
{
    public class BillTypeProfile : AutoMapper.Profile
    {
        public BillTypeProfile ()
        {
            CreateMap<BillTypeDto , billtype>()

               .AfterMap( ( src , dest , context ) =>
               {
                   DateTime now = DateTime.Now;

                   dest.issystemtype = false;

                   dest.avatar = ""; // 用户加的类型,有一个固定头像


                   dest.adddate = now;
                   dest.updatedate = null;
                   dest.deletedate = null;

                   dest.mobile = context.Items[Constant.mobilename] != null ? context.Items[Constant.mobilename].ToString() : string.Empty;

               } );



        }
    }
}
