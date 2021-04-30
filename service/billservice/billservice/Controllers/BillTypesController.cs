using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using billservice.Helpers;
using billservice.Helpers.Result;
using billservice.interfaces;
using billservice.models;
using billservice.models.Dto;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace billservice.Controllers
{
    [Authorize]
    [Route( "api/billtypes" )]
    [ApiController]
    public class BillTypesController : Base.BaseController
    {
        readonly IMapper mapper;
        readonly IBillType Ibilltype;
        readonly IMemoryCache memoryCache;

        public BillTypesController ( IBillType Ibilltype , IMapper mapper , IMemoryCache memoryCache )
        {
            this.Ibilltype = Ibilltype;

            this.mapper = mapper;

            this.memoryCache = memoryCache;
        }



        [HttpGet]
        [Route( "getallbilltype/{isout}/{isrefresh}" )]
        public async Task<ServiceResult> getallbilltype ( bool isout = true , bool isrefresh = false )
        {

            var result = new ServiceResult<List<BillTypeReturnDto>>();

            var mobile = base.UserMobile;

            //先取系统类型回来
            List<billtype> systemlist = null;
            string cacheallname = "AllSystemType";

            if ( !this.memoryCache.TryGetValue( cacheallname , out systemlist ) )
            {
                systemlist = await this.Ibilltype.GetAllSystemTypeAsync(); //计算出cache的值

                //设置过期cache时间
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration( TimeSpan.FromMinutes( 60 ) );

                // cache加入
                this.memoryCache.Set( cacheallname , systemlist , cacheEntryOptions );
            }

            if ( systemlist != null && systemlist.Count > 0 )
            {
                systemlist.RemoveAll( item => isout ? !item.isout : item.isout );
            }


            //先取系统类型回来
            List<billtype> usertypelist = null;
            string cacheusername = "AllUserType";

            if ( isrefresh )
            {
                usertypelist = await this.Ibilltype.GetAllUserTypeAsync( mobile );
            }
            else
            {
                if ( !this.memoryCache.TryGetValue( cacheusername , out usertypelist ) )
                {
                    usertypelist = await this.Ibilltype.GetAllUserTypeAsync( mobile ); //计算出cache的值

                    //设置过期cache时间
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration( TimeSpan.FromMinutes( 60 ) );

                    // cache加入
                    this.memoryCache.Set( cacheusername , usertypelist , cacheEntryOptions );
                }

            }

            if ( usertypelist != null && usertypelist.Count > 0 )
            {
                usertypelist.RemoveAll( item => isout ? !item.isout : item.isout );
            }

            List<billtype> list = new List<billtype>() { };
            list.AddRange( systemlist );
            list.AddRange( usertypelist );

            List<BillTypeReturnDto> listDto = this.mapper.Map<List<billtype> , List<BillTypeReturnDto>>( list );

            result.IsSuccess( listDto );

            return result;
        }



        [HttpPost]
        public async Task<ServiceResult> add ( [FromBody] BillTypeDto billTypeDto )
        {
            var result = new ServiceResult();

            billtype _billtype = this.mapper.Map<BillTypeDto , billtype>( billTypeDto , opt => opt.Items[Constant.mobilename] = base.UserMobile );

            bool bl = await this.Ibilltype.SaveAsync( _billtype );

            if ( !bl )
            {
                result.IsFailed( "保存失败" );

                return result;
            }

            return result;
        }



    }
}
