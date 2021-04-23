using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using billservice.interfaces;
using billservice.models.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace billservice.Validator
{
    public class BillTypeDtoValidator : AbstractValidator<BillTypeDto>
    {
        readonly IBillType billtype;
        IHttpContextAccessor _context;

        public BillTypeDtoValidator ( IBillType billtype , IHttpContextAccessor _context )
        {


            this.billtype = billtype;
            this._context = _context;

            RuleFor( item => item.isout )
              .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
              .WithName( "类型" );

            //RuleFor( item => item.mobile )
            //   .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
            //   .NotEmpty().WithMessage( "{PropertyName}为空" )
            //   .WithName( "手机号码" );

            RuleFor( item => item.typename )
               .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
               .NotEmpty().WithMessage( "{PropertyName}为空" )
               .MaximumLength( 10 ).WithMessage( "{PropertyName}最大长度请小于10位" )

               .Must( ( item , name ) =>
               {
                   if ( _context != null 
                            && _context.HttpContext != null 
                            && _context.HttpContext.User != null )
                   {
                       var user = _context.HttpContext.User;

                       var claims = user.Claims;

                       if ( claims != null && claims.Count() > 0 )
                       {
                           var mobile = claims.FirstOrDefault( item => item.Type == System.Security.Claims.ClaimTypes.Name )?.Value;


                       }


                   }

                   //return !this.billtype.IsExistName( name , item.mobile );

                   return true;

               } ).When( item => !string.IsNullOrWhiteSpace( item.typename ) ).WithMessage( item => string.Format( "{0}:{1}不唯一" , "{PropertyName}" , item.typename ) )
               .WithName( "名称" );


        }

    }
}
