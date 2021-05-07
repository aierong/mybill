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
    public class BillDtoValidator : AbstractValidator<BillDto>
    {
        readonly IBillType billtype;
        readonly IBill bill;
        readonly IHttpContextAccessor _context;


        public BillDtoValidator ( IBillType billtype , IBill bill , IHttpContextAccessor _context )
        {
            this.billtype = billtype;
            this.bill = bill;
            this._context = _context;

            CascadeMode = CascadeMode.Stop;



            RuleFor( item => item.isadd )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                .WithName( "操作类型" );


            RuleFor( item => item.billtypeid )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                .GreaterThan( 0 ).WithMessage( "{PropertyName}请大于0" )
                // 判断id是否合法
                .Must( ( item , billtypeid ) =>
                {
                    // 判断分2步: 第1判断是否为系统公用的 第2判断是否为该用户专用的

                    bool bl = this.billtype.IsExistSystemType( billtypeid );

                    if ( bl )
                    {
                        return true;
                    }
                    else
                    {
                        var user = _context.HttpContext.User;

                        var claims = user.Claims;

                        var mobile = ( claims != null && claims.Count() > 0 ) ? ( claims.FirstOrDefault( item => item.Type == System.Security.Claims.ClaimTypes.Name )?.Value ) : string.Empty;

                        bl = this.billtype.IsExistUserType( billtypeid , mobile );

                        return bl;

                    }

                    //return true;
                } ).WithMessage( item => string.Format( "{0}:{1}错误!它不是系统公用类型或者不是该用户专属类型" , "{PropertyName}" , item.billtypeid ) )
                .WithName( "账目类型ID" );

            RuleFor( item => item.isout )
                .Cascade( CascadeMode.Stop )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                // 判断类型id和出入是否一致
                .Must( ( item , isout ) =>
                {
                    var _billtypeid = item.billtypeid;

                    return this.billtype.IsExistType( _billtypeid , isout );

                    //return true;
                } ).WithMessage( "账目类型ID与账目进出类型不相符" )
                .WithName( "账目进出类型" );

            RuleFor( item => item.moneydate )
               .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
               .NotEmpty().WithMessage( "{PropertyName}为空" )
               .Length( 10 , 10 ).WithMessage( "{PropertyName}长度不对!(格式:xxxx-xx-xx)" )
               .Must( ( item , moneydate ) =>
               {
                   try
                   {
                       Convert.ToDateTime( moneydate );
                   }
                   catch ( Exception )
                   {
                       return false;
                   }

                   return true;
               } ).WithMessage( "{PropertyName}不是正确日期格式(格式:xxxx-xx-xx)" )
               .WithName( "账目日期" );

            RuleFor( item => item.moneys )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                .GreaterThan( 0 ).WithMessage( "{PropertyName}请大于0" )
                .WithName( "金额" );


            // 修改时,才验证
            RuleFor( item => item.ids )
               .Cascade( CascadeMode.Stop )
               .NotNull().When( item => !item.isadd ).WithMessage( "{PropertyName}没有传递或者空" )
               .GreaterThan( 0 ).When( item => !item.isadd ).WithMessage( "{PropertyName}请大于0" )
               .Must( ( item , ids ) =>
               {
                   if ( !item.isadd )
                   {
                       var user = _context.HttpContext.User;

                       var claims = user.Claims;

                       var mobile = ( claims != null && claims.Count() > 0 ) ? ( claims.FirstOrDefault( item => item.Type == System.Security.Claims.ClaimTypes.Name )?.Value ) : string.Empty;

                       return this.bill.IsExistId( ids , mobile );
                   }

                   return true;
               } ).When( item => !item.isadd ).WithMessage( "{PropertyName}错误" )
               .WithName( "记录ID" );

        }




    }
}
