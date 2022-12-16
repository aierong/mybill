using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Helpers;
using billservice.interfaces;
using billservice.models;
using billservice.models.Dto;
using FluentValidation;

namespace billservice.Validator
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        readonly IUser user;


        public UserLoginDtoValidator ( IUser user )
        {
            this.user = user;

            //CascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;   //11.0版本的写法

            RuleFor( user => user.mobile )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                .NotEmpty().WithMessage( "{PropertyName}为空" )
                .MinimumLength( 6 ).WithMessage( "{PropertyName}最小长度6位" )
                // 判断手机号码是否存在
                .Must( ( item , mobile ) =>
                {
                    //返回true 就是验证成功
                    return this.user.IsExistUser( mobile );

                } ).WithMessage( item => string.Format( "{0}{1}不存在" , "{PropertyName}" , item.mobile ) )
                .WithName( "手机号码" );


            RuleFor( user => user.password )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                .NotEmpty().WithMessage( "{PropertyName}为空" )
                .Must( ( item , password ) =>
                {

                    users usermodel = this.user.GetUser( item.mobile );

                    if ( usermodel != null
                    && !string.IsNullOrWhiteSpace( usermodel.password )
                    && usermodel.password.Equals( MD5Security.Encrypt( password ) ) )
                    {
                        return true;
                    }

                    return false;

                } ).When( item => !string.IsNullOrWhiteSpace( item.mobile ) ).WithMessage( "密码错误" )

                .WithName( "密码" );



        }
    }
}
