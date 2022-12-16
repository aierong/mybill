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



namespace billservice.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [Authorize]
    [Route( "api/users" )]
    [ApiController]
    public class UsersController : Base.BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        readonly IUser iuser;

        /// <summary>
        /// 
        /// </summary>
        readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        readonly TokenConfigData tokenConfigData;


        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="user"></param>
        /// <param name="mapper"></param>
        /// <param name="_TokenConfigDataOptions"></param>
        public UsersController ( IUser user , IMapper mapper , IOptionsMonitor<TokenConfigData> _TokenConfigDataOptions )
        {
            this.iuser = user;
            this.mapper = mapper;
            this.tokenConfigData = _TokenConfigDataOptions.CurrentValue;
        }



        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route( "login" )]
        public async Task<ServiceResult<UserTokenDto>> login ( [FromBody] UserLoginDto userLoginDto )
        {
            string mobile = userLoginDto.mobile;

            var result = new ServiceResult<UserTokenDto>();

            users usermodel = await this.iuser.GetUserAsync( mobile );

            if ( usermodel == null )
            {
                result.IsFailed( "手机号错误" );

                return result;
            }
            else
            {
                // 修改一下,登录信息
                await this.iuser.UpdateLoginInfoAsync( mobile , usermodel.logintimes + 1 );
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
                
                //3.自定义名称
                new Claim( "mobile",mobile ), //其实没有啥用,先记录着吧
                new Claim( "avatar",usermodel.avatar ),

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

                //rolename = _role ,

                mobile = mobile ,
            } );

            return result;
        }



        [HttpGet]
        public async Task<ServiceResult<UserDto>> get ()
        {
            var result = new ServiceResult<UserDto>();

            var mobile = base.UserMobile;

            var user = await this.iuser.GetUserAsync( mobile );

            UserDto _UserDto = this.mapper.Map<users , UserDto>( user );

            result.IsSuccess( _UserDto );

            return result;
        }



        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route( "" )]
        public async Task<ServiceResult<string>> add ( [FromBody] UserAddDto userDto )
        {
            var result = new ServiceResult<string>() { Result = string.Empty };

            users user = this.mapper.Map<UserAddDto , users>( userDto );

            bool bl = await this.iuser.SaveAsync( user );

            if ( !bl )
            {
                result.IsFailed( "保存失败" );

                return result;
            }
                        
            return result;
        }




        [HttpPost]
        [Route( "updateavatar" )]
        public async Task<ServiceResult<string>> updateavatar ( [FromForm] string avatar )
        {
            var result = new ServiceResult<string>() { Result = string.Empty };

            var mobile = base.UserMobile;

            bool bl = await this.iuser.UpdateAvatarAsync( mobile , avatar );

            if ( !bl )
            {
                result.IsFailed( "保存失败" );

                return result;
            }


            return result;
        }



        [HttpPost]
        [Route( "updatepassword" )]
        public async Task<ServiceResult<string>> updatepassword ( [FromForm] string password )
        {
            var result = new ServiceResult<string>() { Result = string.Empty };

            var mobile = base.UserMobile;

            bool bl = await this.iuser.UpdatePassWordAsync( mobile , MD5Security.Encrypt( password ) );

            if ( !bl )
            {
                result.IsFailed( "保存失败" );

                return result;
            }

            return result;
        }



    }
}
