using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.models;

namespace billservice.interfaces
{
    /// <summary>
    /// 账单类型接口
    /// </summary>
    public interface IBillType
    {
        bool IsExistName ( string typename , string mobile );

        bool IsExistType ( int billtypeid , bool isout );

        bool IsExistSystemType ( int billtypeid );

        bool IsExistUserType ( int billtypeid , string mobile );

        bool Save ( billtype _billtype );

        Task<bool> SaveAsync ( billtype _billtype );


        Task<List<billtype>> GetAllSystemTypeAsync ();
        Task<List<billtype>> GetAllUserTypeAsync ( string mobile );
    }
}
