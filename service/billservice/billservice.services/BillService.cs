using System.Collections.Generic;
using System.Threading.Tasks;
using billservice.models;
using billservice.models.Dto;

namespace billservice.services
{
    /// <summary>
    /// 账单服务
    /// </summary>
    public class BillService : interfaces.IBill
    {
        /// <summary>
        /// 
        /// </summary>
        readonly IRepository.IBill iBill;


        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="iBill"></param>
        public BillService ( IRepository.IBill iBill )
        {
            this.iBill = iBill;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public bool IsExistId ( int id , string mobile )
        {
            return this.iBill.IsExistId( id , mobile );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public async Task<bool> IsExistIdAsync ( int id , string mobile )
        {
            return await this.iBill.IsExistIdAsync( id , mobile );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public bool Save ( bills bill )
        {
            return this.iBill.Save( bill );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public async Task<bool> SaveAsync ( bills bill )
        {
            return await this.iBill.SaveAsync( bill );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public bool Update ( bills bill )
        {
            return this.iBill.Update( bill );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync ( bills bill )
        {
            return await this.iBill.UpdateAsync( bill );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="billtypeid"></param>
        /// <returns></returns>
        public async Task<List<BillReturnDto>> GetListAsync ( string mobile , int year , int month , int billtypeid )
        {
            return await this.iBill.GetListAsync( mobile , year , month , billtypeid );
        }


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
        public async Task<List<BillReturnDto>> GetListAsync ( string mobile , int year , int month , bool isout , int billtypeid , string mode )
        {
            return await this.iBill.GetListAsync( mobile , year , month , isout , billtypeid , mode );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BillReturnDto> GetAsync ( int id )
        {
            return await this.iBill.GetAsync( id );
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync ( int id )
        {
            return await this.iBill.DeleteAsync( id );
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="isout"></param>
        /// <returns></returns>
        public async Task<List<BillMapReturnDto>> GetStatListAsync ( string mobile , int year , int month , bool isout )
        {
            return await this.iBill.GetStatListAsync( mobile , year , month , isout );
        }



        public async Task<List<BillReturnDto>> GetTopOutListAsync ( string mobile , int year , int month , int topnum )
        {
            return await this.iBill.GetTopOutListAsync( mobile , year , month , topnum );
        }





        public async Task<int> GetOutListCountAsync ( string mobile , int year , int month )
        {
            return await this.iBill.GetOutListCountAsync( mobile , year , month );
        }



        public async Task<List<BillSumMonthReturnDto>> GetSumByMonthAsync ( string mobile , int year , int month , bool isout , int monthnum )
        {
            return await this.iBill.GetSumByMonthAsync( mobile , year , month , isout , monthnum );
        }



        public async Task<List<BillSumDayReturnDto>> GetSumByDayAsync ( string mobile , int year , int month , bool isout )
        {
            return await this.iBill.GetSumByDayAsync( mobile , year , month , isout );

        }
    }
}


