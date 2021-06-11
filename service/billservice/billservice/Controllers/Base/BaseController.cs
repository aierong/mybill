using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace billservice.Controllers.Base
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class BaseController : ControllerBase
    {
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
