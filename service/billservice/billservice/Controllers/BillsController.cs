using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace billservice.Controllers
{
    [Authorize]
    [Route( "api/bills" )]
    [ApiController]
    public class BillsController : Base.BaseController
    {




        // POST api/<BillController>
        [HttpPost]
        public void Post ( [FromBody] string value )
        {
        }




    }
}
