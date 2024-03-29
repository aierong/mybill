﻿using billservice.interfaces;
using billservice.models.Dto;
using FluentValidation;



namespace billservice.Validator
{
    /// <summary>
    /// 验证类
    /// </summary>
    public class UserAddDtoValidator : AbstractValidator<UserAddDto>
    {
        /// <summary>
        /// 用户接口
        /// </summary>
        readonly IUser user;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="user"></param>
        public UserAddDtoValidator ( IUser user )
        {
            this.user = user;

            //CascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;   //11.0版本的写法


            RuleFor( user => user.mobile )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                .NotEmpty().WithMessage( "{PropertyName}为空" )
                .MinimumLength( 6 ).WithMessage( "{PropertyName}最小长度6位" )
                // 判断手机号码是否唯一
                .Must( ( item , mobile ) =>
                {
                    //返回true 就是验证成功
                    return !this.user.IsExistUser( mobile );

                } ).WithMessage( item => string.Format( "{0}{1}重复" , "{PropertyName}" , item.mobile ) )
                .WithName( "手机号码" );


            RuleFor( user => user.password )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                .NotEmpty().WithMessage( "{PropertyName}为空" )
                .WithName( "密码" );


            RuleFor( user => user.name )
                .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
                .NotEmpty().WithMessage( "{PropertyName}为空" )
                .WithName( "姓名" );

            RuleFor( user => user.avatar )
               .NotNull().WithMessage( "{PropertyName}没有传递或者空" )
               .NotEmpty().WithMessage( "{PropertyName}为空" )
               .WithName( "头像" );

            RuleFor( user => user.email )
                // 条件验证:如果有email的情况下 ,判断格式
                .EmailAddress().When( item => !string.IsNullOrWhiteSpace( item.email ) ).WithMessage( "{PropertyName}格式不正确" )
                // 条件验证:如果有email的情况下, 判断邮件地址是否唯一
                .Must( ( item , email ) =>
                {
                    return !this.user.IsExistEmail( email );

                } ).When( item => !string.IsNullOrWhiteSpace( item.email ) ).WithMessage( item => string.Format( "{0}{1}不唯一(重复)" , "{PropertyName}" , item.email ) )
                .WithName( "Email(邮件)" );

        }



    }
}
