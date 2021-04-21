using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Helpers.Result;
using billservice.Models.Dto;
using billservice.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace billservice.Controllers
{
    [Route( "api/users" )]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUser user;
        public UsersController ( IUser user )
        {
            this.user = user;
        }


        [HttpPost]
        [Route( "login" )]
        public IEnumerable<string> login ()
        {
            return new string[] { "value1" , "value2" };
        }



        [HttpPost]
        [Route( "" )]
        public ServiceResult adduser ( [FromBody] UserDto userDto )
        {
            var result = new ServiceResult();


            //bool bl = this.user.IsExistUser(  userDto.mobile );

            //if ( bl )
            //{
            //}

            return result;
        }



    }
}
