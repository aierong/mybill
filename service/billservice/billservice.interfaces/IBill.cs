using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.models;
using billservice.models.Dto;

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


        Task<List<BillReturnDto>> GetListAsync ( string mobile , int year , int month , int billtypeid );

        


        Task<BillReturnDto> GetAsync ( int id );
    }
}
