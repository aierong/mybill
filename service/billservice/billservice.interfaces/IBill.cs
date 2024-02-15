using System.Collections.Generic;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        bool IsExistId ( int id , string mobile );


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        Task<bool> IsExistIdAsync ( int id , string mobile );



        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        bool Save ( bills bill );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        Task<bool> SaveAsync ( bills bill );



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        bool Update ( bills bill );


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync ( bills bill );


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="billtypeid"></param>
        /// <returns></returns>
        Task<List<BillReturnDto>> GetListAsync ( string mobile , int year , int month , int billtypeid );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="isout"></param>
        /// <param name="billtypeid"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        Task<List<BillReturnDto>> GetListAsync ( string mobile , int year , int month , bool isout , int billtypeid , string mode );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BillReturnDto> GetAsync ( int id );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync ( int id );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="isout"></param>
        /// <returns></returns>
        Task<List<BillMapReturnDto>> GetStatListAsync ( string mobile , int year , int month , bool isout );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="topnum"></param>
        /// <returns></returns>
        Task<List<BillReturnDto>> GetTopOutListAsync ( string mobile , int year , int month , int topnum );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        Task<int> GetOutListCountAsync ( string mobile , int year , int month );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="isout"></param>
        /// <param name="monthnum"></param>
        /// <returns></returns>
        Task<List<BillSumMonthReturnDto>> GetSumByMonthAsync ( string mobile , int year , int month , bool isout , int monthnum );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="isout"></param>
        /// <returns></returns>
        Task<List<BillSumDayReturnDto>> GetSumByDayAsync ( string mobile , int year , int month , bool isout );
    }
}
