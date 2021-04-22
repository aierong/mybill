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

            RuleFor( item => item.isout )
              .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
              .WithName( "类型" );

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
                   return !this.billtype.IsExistName( name , item.mobile );

               } ).When( item => !string.IsNullOrWhiteSpace( item.typename ) && !string.IsNullOrWhiteSpace( item.mobile ) ).WithMessage( item => string.Format( "{0}:{1}不唯一" , "{PropertyName}" , item.typename ) )
               .WithName( "名称" );


        }

    }
}
