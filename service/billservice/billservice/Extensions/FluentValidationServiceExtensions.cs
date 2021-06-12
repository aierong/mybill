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
        /// <summary>
        /// FluentValidation的相关服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddFluentValidationService ( this IServiceCollection services , IConfiguration configuration )
        {
            services.AddMvc().AddFluentValidation();

            services.AddTransient<IValidator<UserAddDto> , UserAddDtoValidator>();
            services.AddTransient<IValidator<UserLoginDto> , UserLoginDtoValidator>();
            services.AddTransient<IValidator<BillTypeDto> , BillTypeDtoValidator>();
            services.AddTransient<IValidator<BillDto> , BillDtoValidator>();

            return services;
        }
    }
}
