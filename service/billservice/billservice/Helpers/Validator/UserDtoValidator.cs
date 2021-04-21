using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Models.Dto;
using FluentValidation;

namespace billservice.Helpers.Validator
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {


        public UserDtoValidator ()
        {
            RuleFor( user => user.mobile )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                .NotEmpty().WithMessage( "{PropertyName}为空" )
                .MinimumLength( 6 ).WithMessage( "{PropertyName}最小长度6位" )
                .WithName( "手机号码" );


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
