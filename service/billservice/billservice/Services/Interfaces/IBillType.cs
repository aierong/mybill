using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace billservice.Services.Interfaces
{
    public interface IBillType
    {
        bool IsExistName ( string typename , string mobile );
    }
}
