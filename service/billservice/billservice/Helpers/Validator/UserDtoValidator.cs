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
                .NotNull().WithMessage( "手机号码没有传递或者空" )
                .NotEmpty().WithMessage( "手机号码为空" );


            RuleFor( user => user.password )
                .NotNull().WithMessage( "密码没有传递或者空" )
                .NotEmpty().WithMessage( "密码为空" );


            RuleFor( user => user.name )
                .NotNull().WithMessage( "姓名没有传递或者空" )
                .NotEmpty().WithMessage( "姓名为空" );

            RuleFor( user => user.avatar )
               .NotNull().WithMessage( "头像没有传递或者空" )
               .NotEmpty().WithMessage( "头像为空" );

        }

    }
}
