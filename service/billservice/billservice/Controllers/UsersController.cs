using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using billservice.Helpers.Result;
using billservice.Models;
using billservice.Models.Dto;
using billservice.Services;
using billservice.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace billservice.Controllers
{
    [Route( "api/users" )]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUser user;

        private readonly IMapper mapper;

        public UsersController ( IUser user , IMapper mapper )
        {
            this.user = user;

            this.mapper = mapper;
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

            users user = this.mapper.Map<UserDto , users>( userDto );

            bool bl = this.user.SaveUser( user );

            if ( !bl )
            {
                result.IsFailed( "保存失败" );

                return result;
            }


            return result;
        }



    }
}
