using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.interfaces;
using billservice.IRepository;
using billservice.models;


namespace billservice.services
{
    /// <summary>
    /// 
    /// </summary>
    public class BillTypeService : interfaces.IBillType
    {
        readonly IRepository.IBillType iBillType;


        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="iBillType"></param>
        public BillTypeService ( IRepository.IBillType iBillType )
        {
            this.iBillType = iBillType;
        }



        public bool IsExistName ( string typename , string mobile )
        {

            return this.iBillType.IsExistName( typename , mobile );
        }



        public bool IsExistType ( int billtypeid , bool isout )
        {


            return this.iBillType.IsExistType( billtypeid , isout );
        }



        public bool IsExistUserType ( int billtypeid , string mobile )
        {



            return this.iBillType.IsExistUserType( billtypeid , mobile );
        }



        public bool IsExistSystemType ( int billtypeid )
        {


            return this.iBillType.IsExistSystemType( billtypeid );
        }



        public bool Save ( billtype _billtype )
        {



            return this.iBillType.Save( _billtype );
        }



        public async Task<bool> SaveAsync ( billtype _billtype )
        {



            return await this.iBillType.SaveAsync( _billtype );
        }



        public async Task<List<billtype>> GetAllSystemTypeAsync ()
        {



            return await this.iBillType.GetAllSystemTypeAsync();
        }



        public async Task<List<billtype>> GetAllUserTypeAsync ( string mobile )
        {



            return await this.iBillType.GetAllUserTypeAsync( mobile );
        }



    }
}
