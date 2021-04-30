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
    [Route( "api/bills" )]
    [ApiController]
    public class BillsController : Base.BaseController
    {

        readonly IMapper mapper;
        readonly IBill Ibill;


        public BillsController ( IBill Ibill , IMapper mapper )
        {
            this.Ibill = Ibill;
            this.mapper = mapper;
        }




        [HttpPost]
        public async Task<ServiceResult> add ( [FromBody] BillDto billDto )
        {
            var result = new ServiceResult();

            bills _bill = this.mapper.Map<BillDto , bills>( billDto , opt => opt.Items[Constant.mobilename] = base.UserMobile );

            bool bl = await this.Ibill.SaveAsync( _bill );

            if ( !bl )
            {
                result.IsFailed( "保存失败" );

                return result;
            }

            return result;
        }



        [HttpPut]
        public async Task<ServiceResult> update ( [FromBody] BillDto billDto )
        {
            var result = new ServiceResult();

            bills _bill = this.mapper.Map<BillDto , bills>( billDto , opt => opt.Items[Constant.mobilename] = base.UserMobile );

            bool bl = await this.Ibill.UpdateAsync( _bill );

            if ( !bl )
            {
                result.IsFailed( "保存失败" );

                return result;
            }

            return result;
        }



    }
}
