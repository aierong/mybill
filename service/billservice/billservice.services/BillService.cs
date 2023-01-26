using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using billservice.interfaces;
using billservice.IRepository;
using billservice.models;
using billservice.models.Dto;
using System.Collections.Specialized;

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
        /// 注入
        /// </summary>
        /// <param name="iBill"></param>
        public BillService ( IRepository.IBill iBill )
        {
            this.iBill = iBill;
        }



        public bool IsExistId ( int id , string mobile )
        {

            return this.iBill.IsExistId( id , mobile );
        }



        public async Task<bool> IsExistIdAsync ( int id , string mobile )
        {


            return await this.iBill.IsExistIdAsync( id , mobile );
        }



        public bool Save ( bills bill )
        {


            return this.iBill.Save( bill );
        }



        public async Task<bool> SaveAsync ( bills bill )
        {



            return await this.iBill.SaveAsync( bill );
        }



        public bool Update ( bills bill )
        {



            return this.iBill.Update( bill );
        }



        public async Task<bool> UpdateAsync ( bills bill )
        {



            return await this.iBill.UpdateAsync( bill );
        }



        public async Task<List<BillReturnDto>> GetListAsync ( string mobile , int year , int month , int billtypeid )
        {


            return await this.iBill.GetListAsync( mobile , year , month , billtypeid );

        }



        public async Task<List<BillReturnDto>> GetListAsync ( string mobile , int year , int month , bool isout , int billtypeid , string mode )
        {


            return await this.iBill.GetListAsync( mobile , year , month , isout , billtypeid , mode );

        }



        public async Task<BillReturnDto> GetAsync ( int id )
        {

            return await this.iBill.GetAsync( id );
        }



        public async Task<bool> DeleteAsync ( int id )
        {


            return await this.iBill.DeleteAsync( id );
        }



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


