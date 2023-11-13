using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using billservice.Helpers.ConfigData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace billservice.Extensions
{
    public static class TokenServiceExtensions
    {
        //public static IServiceCollection AddTokenService ( this IServiceCollection services , IConfiguration configuration )
        //{
        //    const string sectionname = "TokenInfo";

        //    services.Configure<TokenConfigData>( configuration.GetSection( sectionname ) );


        //    //把配置文件中，配置信息加载回来
        //    TokenConfigData _tokenconfigdata = configuration.GetSection( sectionname ).Get<TokenConfigData>();


        //    // 注册JwtBearer服务
        //    services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme ).AddJwtBearer( options =>
        //    {
        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {

        //            ValidateIssuer = true ,  //是否验证Issuer
        //            ValidateAudience = true ,//是否验证Audience
        //            ValidateLifetime = true ,//是否验证失效时间
        //            ValidateIssuerSigningKey = true ,//是否验证SecurityKey

        //            ValidAudience = _tokenconfigdata.audience , // "http://localhost:5001" ,//Audience
        //            ValidIssuer = _tokenconfigdata.issuer , // "http://localhost:5000" ,//Issuer，这两项和前面签发jwt的设置一致
        //                                                    //IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( "1234567890123456key" ) )//拿到SecurityKey
        //            IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( _tokenconfigdata.key ) )//拿到SecurityKey

        //        };
        //    } );

        //    return services;
        //}


    }
}
