using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace billservice.Models.ConfigData
{
    public class TokenConfigData
    {
        public string issuer
        {
            get; set;
        }

        public string audience
        {
            get; set;
        }

        public string key
        {
            get; set;
        }

        public int expiresminute
        {
            get; set;
        }
    }
}
