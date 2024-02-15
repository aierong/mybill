using System.Collections.Generic;
using System.Threading.Tasks;
using billservice.models;



namespace billservice.interfaces
{
    /// <summary>
    /// 账单类型接口
    /// </summary>
    public interface IBillType
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typename"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        bool IsExistName ( string typename , string mobile );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="billtypeid"></param>
        /// <param name="isout"></param>
        /// <returns></returns>
        bool IsExistType ( int billtypeid , bool isout );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="billtypeid"></param>
        /// <returns></returns>
        bool IsExistSystemType ( int billtypeid );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="billtypeid"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        bool IsExistUserType ( int billtypeid , string mobile );



        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="_billtype"></param>
        /// <returns></returns>
        bool Save ( billtype _billtype );



        /// <summary>
        /// 
        /// </summary>
        /// <param name="_billtype"></param>
        /// <returns></returns>
        Task<bool> SaveAsync ( billtype _billtype );



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<billtype>> GetAllSystemTypeAsync ();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        Task<List<billtype>> GetAllUserTypeAsync ( string mobile );
    }
}
