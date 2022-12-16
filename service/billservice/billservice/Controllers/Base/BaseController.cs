using System.Linq;
using Microsoft.AspNetCore.Mvc;



namespace billservice.Controllers.Base
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 获取登录账号
        /// </summary>
        public string UserMobile
        {
            get
            {
                var user = base.User;

                if ( user != null )
                {
                    var claims = user.Claims;

                    if ( claims != null && claims.Count() > 0 )
                    {
                        var name = claims.FirstOrDefault( it => it.Type == System.Security.Claims.ClaimTypes.Name )?.Value;

                        return name;
                    }

                }

                return string.Empty;
            }
        }



    }
}
