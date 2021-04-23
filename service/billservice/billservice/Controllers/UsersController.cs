using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using billservice.Helpers;
using billservice.Helpers.ConfigData;
using billservice.Helpers.Result;
using billservice.interfaces;
using billservice.models;
using billservice.models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace billservice.Controllers
{
    [Authorize]
    [Route( "api/users" )]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUser user;

        private readonly IMapper mapper;

        private readonly TokenConfigData tokenConfigData;

        public UsersController ( IUser user , IMapper mapper , IOptionsMonitor<TokenConfigData> _TokenConfigDataOptions )
        {
            this.user = user;

            this.mapper = mapper;

            this.tokenConfigData = _TokenConfigDataOptions.CurrentValue;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route( "login" )]
        public ServiceResult<UserTokenDto> login ( [FromForm] string mobile , [FromForm] string pwd )
        {
            var result = new ServiceResult<UserTokenDto>();

            // 一些验证

            users usermodel = this.user.GetUser( mobile );

            if ( usermodel == null )
            {
                // 
                result.IsFailed( "手机号错误" );

                return result;
            }
            else
            {
                if ( !usermodel.password.Equals( new MD5Security().Encrypt( pwd ) ) )
                {
                    //密码这里暂时关闭
                    //result.IsFailed( "密码错误" );

                    //return result;
                }
            }

            var _role = usermodel.rolename;

            // 验证通过了
            var claims = new Claim[]
            {
                //这里演示：3种定义载荷的方式

                //1.System.Security.Claims.ClaimTypes
                new Claim( System.Security.Claims.ClaimTypes.Name, mobile),

                //2.System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames
                new Claim( System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, usermodel.email ),
                
                // 角色               
                new Claim( System.Security.Claims.ClaimTypes.Role, _role),
            };

            //Key的长度至少是16位  (注意密钥最少16位)
            //var key = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( "1234567890123456key" ) );
            var key = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( this.tokenConfigData.key ) );

            //issuer代表颁发Token的Web应用程序，audience是Token的受理者, 暂时用不到，随便指定
            var token = new JwtSecurityToken(
                issuer: this.tokenConfigData.issuer ,        // "http://localhost:5000" ,
                audience: this.tokenConfigData.audience ,   //  "http://localhost:5001" ,
                claims: claims ,
                notBefore: DateTime.Now ,
                // 过期时间
                expires: DateTime.Now.AddMinutes( this.tokenConfigData.expiresminute ) ,
                signingCredentials: new SigningCredentials( key , SecurityAlgorithms.HmacSha256 )
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken( token );

            // 把自己的数据带回去
            result.IsSuccess( new UserTokenDto()
            {
                token = jwtToken ,

                mobile = mobile ,

                rolename = _role
            } );

            return result;
        }



        [AllowAnonymous]
        [HttpPost]
        [Route( "" )]
        public ServiceResult add ( [FromBody] UserDto userDto )
        {
            var result = new ServiceResult();

            users user = this.mapper.Map<UserDto , users>( userDto );

            bool bl = this.user.Save( user );

            if ( !bl )
            {
                result.IsFailed( "保存失败" );

                return result;
            }


            return result;
        }



    }
}
