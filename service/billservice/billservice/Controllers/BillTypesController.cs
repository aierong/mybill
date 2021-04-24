using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Helpers.Result;
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


        // POST api/<BillTypeController>
        [HttpPost]
        public ServiceResult add ( [FromBody] BillTypeDto billTypeDto )
        {
            var result = new ServiceResult();
            //base.HttpContext.User

            return result;
        }


    }
}
