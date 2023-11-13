using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.models.Dto;
using billservice.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace billservice.Extensions
{
    public static class FluentValidationServiceExtensions
    {
        ///// <summary>
        ///// FluentValidation的相关服务注册
        ///// </summary>
        ///// <param name="services"></param>
        ///// <param name="configuration"></param>
        ///// <returns></returns>
        //public static IServiceCollection AddFluentValidationService ( this IServiceCollection services , IConfiguration configuration )
        //{

        //    //services.AddMvc().AddFluentValidation();
        //    services.AddFluentValidationAutoValidation( config =>
        //    {
        //        //执行FluentValidation后，任何其他验证器提供程序也将有机会执行。这意味着您可以将FluentValidation自动验证与DataAnnotations属性（或任何其他ASP.NET ModelValidatorProvider实现）混合使用。
        //        //以便FluentValidation是唯一执行的验证库
        //        config.DisableDataAnnotationsValidation = true;

        //        //config.ImplicitlyValidateChildProperties = true;
        //        //config.ImplicitlyValidateRootCollectionElements = true;
        //    } );  //新版本注册


        //    services.AddTransient<IValidator<UserAddDto> , UserAddDtoValidator>();
        //    services.AddTransient<IValidator<UserLoginDto> , UserLoginDtoValidator>();
        //    services.AddTransient<IValidator<BillTypeDto> , BillTypeDtoValidator>();
        //    services.AddTransient<IValidator<BillDto> , BillDtoValidator>();

        //    return services;
        //}



    }
}
