using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.interfaces;
using billservice.models.Dto;
using FluentValidation;

namespace billservice.Validator
{
    public class BillDtoValidator : AbstractValidator<BillDto>
    {
        readonly IBillType billtype;

        public BillDtoValidator ( IBillType billtype )
        {
            this.billtype = billtype;


            RuleFor( item => item.billtypeid )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                .WithName( "账目类型ID" );

            RuleFor( item => item.isout )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                .WithName( "账目进出类型" );

            RuleFor( item => item.moneydate )
               .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
               .NotEmpty().WithMessage( "{PropertyName}为空" )
               .Length( 10 , 10 ).WithMessage( "{PropertyName}长度不对!(格式:xxxx-xx-xx)" )
               .WithName( "账目日期" );

            RuleFor( item => item.moneys )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                .GreaterThan( 0 ).WithMessage( "{PropertyName}请大于0" )
                .WithName( "金额" );


        }

    }
}
