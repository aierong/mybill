using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.models;
using billservice.models.Dto;

 

namespace billservice.interfaces
{
    /// <summary>
    /// 账单接口
    /// </summary>
    public interface IBill
    {

        bool IsExistId ( int id , string mobile );
        Task<bool> IsExistIdAsync ( int id , string mobile );

        bool Save ( bills bill );
        Task<bool> SaveAsync ( bills bill );

        bool Update ( bills bill );
        Task<bool> UpdateAsync ( bills bill );


        Task<List<BillReturnDto>> GetListAsync ( string mobile , int year , int month , int billtypeid );
        Task<List<BillReturnDto>> GetListAsync ( string mobile , int year , int month , bool isout , int billtypeid , string mode );


        Task<BillReturnDto> GetAsync ( int id );

        Task<bool> DeleteAsync ( int id );


        Task<List<BillMapReturnDto>> GetStatListAsync ( string mobile , int year , int month , bool isout );


        Task<List<BillReturnDto>> GetTopOutListAsync ( string mobile , int year , int month , int topnum );



        Task<int> GetOutListCountAsync ( string mobile , int year , int month );


        Task<List<BillSumMonthReturnDto>> GetSumByMonthAsync ( string mobile , int year , int month , bool isout , int monthnum );

        Task<List<BillSumDayReturnDto>> GetSumByDayAsync ( string mobile , int year , int month , bool isout );
    }
}
