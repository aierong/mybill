using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.models;

namespace billservice.interfaces
{
    public interface IBillType
    {
        bool IsExistName ( string typename , string mobile );

        bool Save ( billtype _billtype );
    }
}
