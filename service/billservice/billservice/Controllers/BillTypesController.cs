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
        public async Task<ServiceResult<List<BillTypeReturnDto>>> getallbilltype ( bool isout = true , bool isrefresh = false )
        {

            var result = new ServiceResult<List<BillTypeReturnDto>>();

            var mobile = base.UserMobile;

            //先取系统类型回来,这个类型可以从缓存中取
            List<billtype> systemlist = null;
            List<billtype> resultsystemlist = null;
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
                resultsystemlist = systemlist.FindAll( item => item.isout == isout );
                
            }


            //再取用户类型回来 ,判断一下:如果是刷新就取数据库,如果不是就取缓存
            List<billtype> usertypelist = null;
            List<billtype> resultusertypelist = null;
            string cacheusername = "AllUserType";
            var cacheUserEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration( TimeSpan.FromMinutes( 60 ) );

            if ( isrefresh )
            {
                usertypelist = await this.Ibilltype.GetAllUserTypeAsync( mobile );

                // 记录一下,要不下次取,不是最新
                this.memoryCache.Set( cacheusername , usertypelist , cacheUserEntryOptions );
            }
            else
            {
                if ( !this.memoryCache.TryGetValue( cacheusername , out usertypelist ) )
                {
                    usertypelist = await this.Ibilltype.GetAllUserTypeAsync( mobile ); //计算出cache的值

                    // cache加入
                    this.memoryCache.Set( cacheusername , usertypelist , cacheUserEntryOptions );
                }

            }

            if ( usertypelist != null && usertypelist.Count > 0 )
            {
                resultusertypelist = usertypelist.FindAll( item => item.isout == isout );
                
            }

            List<billtype> list = new List<billtype>() { };
            if ( resultsystemlist != null )
            {
                list.AddRange( resultsystemlist );
            }
            if ( resultusertypelist != null )
            {
                list.AddRange( resultusertypelist );
            }


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
