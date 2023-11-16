using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using billservice.Helpers.Result;
using billservice.interfaces;
using billservice.models;
using billservice.models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace billservice.Controllers
{
    /// <summary>
    /// 账单控制器
    /// </summary>
    [Authorize]
    [Route( "api/bills" )]
    [ApiController]
    public class BillsController : Base.BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        readonly IBill Ibill;


        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="Ibill"></param>
        /// <param name="mapper"></param>
        public BillsController ( IBill Ibill , IMapper mapper )
        {
            this.Ibill = Ibill;

            this.mapper = mapper;
        }




        [HttpPost]
        [Route( "add" )]
        public async Task<ServiceResult<string>> add ( [FromBody] BillDto billDto )
        {
            var result = new ServiceResult<string>() { Result = string.Empty };

            bills _bill = this.mapper.Map<BillDto , bills>( billDto , opt => opt.Items[Constant.mobilename] = base.UserMobile );

            bool bl = await this.Ibill.SaveAsync( _bill );

            if ( !bl )
            {
                result.IsFailed( "保存失败" );

                return result;
            }

            return result;
        }



        [HttpPost]
        [Route( "update" )]
        public async Task<ServiceResult<string>> update ( [FromBody] BillDto billDto )
        {
            var result = new ServiceResult<string>() { Result = string.Empty };

            bills _bill = this.mapper.Map<BillDto , bills>( billDto , opt => opt.Items[Constant.mobilename] = base.UserMobile );

            bool bl = await this.Ibill.UpdateAsync( _bill );

            if ( !bl )
            {
                result.IsFailed( "保存失败" );

                return result;
            }

            return result;
        }



        [HttpGet]
        [Route( "getlist/{year}/{month}/{billtypeid}" )]
        public async Task<ServiceResult<List<BillReturnDto>>> getlist ( int year = 2020 , int month = 1 , int billtypeid = 0 )
        {
            var result = new ServiceResult<List<BillReturnDto>>();

            var mobile = base.UserMobile;

            // billtypeid=0 代表全部
            var list = await this.Ibill.GetListAsync( mobile , year , month , billtypeid );

            result.IsSuccess( list );

            return result;
        }



        [HttpGet]
        [Route( "getlist/{year}/{month}/{isout}/{billtypeid}/{mode}" )]
        public async Task<ServiceResult<List<BillReturnDto>>> getlist ( int year , int month , bool isout , int billtypeid , string mode )
        {
            var result = new ServiceResult<List<BillReturnDto>>();

            var mobile = base.UserMobile;

            var list = await this.Ibill.GetListAsync( mobile , year , month , isout , billtypeid , mode );

            result.IsSuccess( list );

            return result;
        }



        [HttpGet]
        [Route( "getstatmonthlist/{year}/{month}/{isout}/{monthnum}" )]
        public async Task<ServiceResult<List<BillSumMonthReturnDto>>> getstatmonthlist ( int year , int month , bool isout , int monthnum )
        {
            var result = new ServiceResult<List<BillSumMonthReturnDto>>();

            var mobile = base.UserMobile;

            var list = await this.Ibill.GetSumByMonthAsync( mobile , year , month , isout , monthnum );

            result.IsSuccess( list );

            return result;
        }



        [HttpGet]
        [Route( "getstatdaylist/{year}/{month}/{isout}" )]
        public async Task<ServiceResult<List<BillSumDayReturnDto>>> getstatdaylist ( int year , int month , bool isout )
        {
            var result = new ServiceResult<List<BillSumDayReturnDto>>();

            var mobile = base.UserMobile;

            var list = await this.Ibill.GetSumByDayAsync( mobile , year , month , isout );

            result.IsSuccess( list );

            return result;
        }



        [HttpGet]
        [Route( "getstatlist/{year}/{month}/{isout}" )]
        public async Task<ServiceResult<List<BillMapReturnDto>>> getstatlist ( int year = 2020 , int month = 1 , bool isout = false )
        {
            var result = new ServiceResult<List<BillMapReturnDto>>();

            var mobile = base.UserMobile;

            var list = await this.Ibill.GetStatListAsync( mobile , year , month , isout );

            result.IsSuccess( list );

            return result;
        }



        [HttpGet]
        [Route( "gettopoutlist/{year}/{month}/{topnum}" )]
        public async Task<ServiceResult<BillReturnTopDto>> gettopoutlist ( int year = 2020 , int month = 1 , int topnum = 1 )
        {
            var result = new ServiceResult<BillReturnTopDto>();

            var mobile = base.UserMobile;

            var list = await this.Ibill.GetTopOutListAsync( mobile , year , month , topnum );

            result.IsSuccess( new BillReturnTopDto()
            {
                list = list ,
                counts = await this.Ibill.GetOutListCountAsync( mobile , year , month )
            } );

            return result;
        }



        [HttpGet]
        [Route( "get/{id}" )]
        public async Task<ServiceResult<BillReturnDto>> get ( int id = 0 )
        {
            var result = new ServiceResult<BillReturnDto>();

            var model = await this.Ibill.GetAsync( id );

            result.IsSuccess( model );

            return result;
        }



        [HttpPost]
        [Route( "delete" )]
        public async Task<ServiceResult<string>> delete ( [FromForm] int id = 0 )
        {
            var result = new ServiceResult<string>() { Result = string.Empty };

            var mobile = base.UserMobile;

            bool bl = await this.Ibill.IsExistIdAsync( id , mobile );

            if ( !bl )
            {
                result.IsFailed( "id错误" );

                return result;
            }

            bl = await this.Ibill.DeleteAsync( id );

            if ( !bl )
            {
                result.IsFailed( "保存失败" );

                return result;
            }

            return result;
        }



    }
}
