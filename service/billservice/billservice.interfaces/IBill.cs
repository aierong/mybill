using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.models;

// billservice.services

namespace billservice.interfaces
{
    public interface IBill
    {
        bool Save ( bills bill );

        bool Update ( bills bill );

        bool IsExistId ( int id , string mobile );
    }
}
