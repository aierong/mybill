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


        

        bool IsExistId ( int id , string mobile );


        bool Save ( bills bill );
        Task<bool> SaveAsync ( bills bill );

        bool Update ( bills bill );
        Task<bool> UpdateAsync ( bills bill );

    }
}
