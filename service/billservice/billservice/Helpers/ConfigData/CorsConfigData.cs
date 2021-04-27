using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace billservice.Helpers.ConfigData
{
    public class CorsConfigData
    {
        public string name
        {
            get; set;
        }


        public List<string> urls
        {
            get; set;
        }
    }
}
