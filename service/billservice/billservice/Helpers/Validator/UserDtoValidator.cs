using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Models.Dto;
using billservice.Services;
using FluentValidation;

namespace billservice.Helpers.Validator
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {

        IUser user;
        //
        public UserDtoValidator ( IUser user )
        {
            this.user = user;

            RuleFor( user => user.mobile )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                .NotEmpty().WithMessage( "{PropertyName}为空" )
                .MinimumLength( 6 ).WithMessage( "{PropertyName}最小长度6位" )
                .Must( ( item , mobile ) =>
                {
                    //返回true 就是验证成功
                    return !this.user.IsExistUser( mobile );                   

                } ).WithMessage( item => string.Format( "{0}{1}重复" , "{PropertyName}" , item.mobile ) ).WithName( "手机号码" );


            RuleFor( user => user.password )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                .NotEmpty().WithMessage( "{PropertyName}为空" )
                .MinimumLength( 6 ).WithMessage( "{PropertyName}最小长度6位" )
                .WithName( "密码" );


            RuleFor( user => user.name )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                .NotEmpty().WithMessage( "{PropertyName}为空" )
                .WithName( "姓名" );

            RuleFor( user => user.avatar )
               .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
               .NotEmpty().WithMessage( "{PropertyName}为空" )
               .WithName( "头像" );

        }

    }
}
