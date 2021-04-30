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



namespace billservice.Controllers
{
    [Authorize]
    [Route( "api/billtypes" )]
    [ApiController]
    public class BillTypesController : Base.BaseController
    {
        readonly IMapper mapper;
        readonly IBillType Ibilltype;


        public BillTypesController ( IBillType Ibilltype , IMapper mapper )
        {
            this.Ibilltype = Ibilltype;
            this.mapper = mapper;
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
