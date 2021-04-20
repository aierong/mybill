using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Models.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace billservice.Controllers
{
    [Route( "api/users" )]
    [ApiController]
    public class UsersController : ControllerBase
    {



        [HttpPost]
        [Route( "login" )]
        public IEnumerable<string> login ()
        {
            return new string[] { "value1" , "value2" };
        }



        [HttpPost]
        [Route( "adduser" )]
        public IEnumerable<string> adduser ( [FromBody] UserDto userDto )
        {


            return new string[] { "value1" , "value2" };
        }



    }
}
