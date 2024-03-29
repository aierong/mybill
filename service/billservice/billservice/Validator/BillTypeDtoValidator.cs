﻿using System.Linq;
using billservice.interfaces;
using billservice.models.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Http;



namespace billservice.Validator
{
    /// <summary>
    /// 验证类
    /// </summary>
    public class BillTypeDtoValidator : AbstractValidator<BillTypeDto>
    {
        /// <summary>
        /// 
        /// </summary>
        readonly IBillType billtype;

        /// <summary>
        /// 上下文
        /// </summary>
        readonly IHttpContextAccessor _context;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="billtype"></param>
        /// <param name="_context"></param>
        public BillTypeDtoValidator ( IBillType billtype , IHttpContextAccessor _context )
        {

            this.billtype = billtype;
            this._context = _context;

            //CascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;   //11.0版本的写法


            RuleFor( item => item.isout )
              .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
              .WithName( "类型" );



            RuleFor( item => item.typename )
               .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
               .NotEmpty().WithMessage( "{PropertyName}为空" )
               .MaximumLength( 10 ).WithMessage( "{PropertyName}最大长度请小于10位" )
               .Must( ( item , name ) =>
               {

                   var user = _context.HttpContext.User;

                   var claims = user.Claims;

                   var mobile = ( claims != null && claims.Count() > 0 ) ? ( claims.FirstOrDefault( item => item.Type == System.Security.Claims.ClaimTypes.Name )?.Value ) : string.Empty;

                   return !this.billtype.IsExistName( name , mobile );

               } ).When( item => !string.IsNullOrWhiteSpace( item.typename ) ).WithMessage( item => string.Format( "{0}:{1}不唯一" , "{PropertyName}" , item.typename ) )
               .WithName( "名称" );


        }

    }
}
