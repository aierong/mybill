using System;
using billservice.models;
using billservice.models.Dto;



namespace billservice.Profile
{
    public class BillTypeProfile : AutoMapper.Profile
    {
        public BillTypeProfile ()
        {
            CreateMap<BillTypeDto , billtype>().AfterMap( ( src , dest , context ) =>
               {
                   DateTime now = DateTime.Now;

                   dest.issystemtype = false;

                   // 用户加的类型,有一个固定头像
                   if ( src.isout )
                   {
                       dest.avatar = "icon-hanhan-01-011";
                   }
                   else
                   {
                       // 进的 头像  
                       dest.avatar = "icon-hanhan-01-01";
                   }

                   dest.adddate = now;
                   dest.updatedate = null;
                   dest.deletedate = null;

                   dest.mobile = context.Items[Constant.mobilename] != null ? context.Items[Constant.mobilename].ToString() : string.Empty;

               } );



            CreateMap<billtype , BillTypeReturnDto>();


        }
    }
}
