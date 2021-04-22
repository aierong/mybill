using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.Models.Dto;
using billservice.Services.Interfaces;
using FluentValidation;

namespace billservice.Helpers.Validator
{
    public class BillTypeDtoValidator : AbstractValidator<BillTypeDto>
    {
        readonly IBillType billtype;


        public BillTypeDtoValidator ( IBillType billtype )
        {
            this.billtype = billtype;
             


            RuleFor( item => item.mobile )
               .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
               .NotEmpty().WithMessage( "{PropertyName}为空" )
               .WithName( "手机号码" );

            RuleFor( item => item.typename )
               .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
               .NotEmpty().WithMessage( "{PropertyName}为空" )
               .MaximumLength( 10 ).WithMessage( "{PropertyName}最大长度请小于10位" )
               .Must( ( item , name ) =>
               {

                   return true;
               } ).WithMessage( "" )
               .WithName( "名称" );


        }

    }
}
