using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using billservice.Helpers.Result;
using billservice.interfaces;
using billservice.models;
using billservice.models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace billservice.Controllers
{
    [Authorize]
    [Route( "api/billtypes" )]
    [ApiController]
    public class BillTypesController : Base.BaseController
    {
        private readonly IMapper mapper;
        readonly IBillType Ibilltype;

        public BillTypesController ( IBillType Ibilltype , IMapper mapper )
        {
            this.Ibilltype = Ibilltype;
            this.mapper = mapper;
        }



        // POST api/<BillTypeController>
        [HttpPost]
        public ServiceResult add ( [FromBody] BillTypeDto billTypeDto )
        {
            var result = new ServiceResult();
            //base.HttpContext.User

            billtype _billtype = this.mapper.Map<BillTypeDto , billtype>( billTypeDto );

            if ( _billtype != null )
            {

            }

            return result;
        }


    }
}
