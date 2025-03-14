

using System.Threading.Tasks;

namespace spiapi.irepository
{
    public interface ICommonRepository
    {
        Task<int> GetSTATIONId ( string _STATION );



        Task<int> GetLINEId ( string _LINENO );



        Task<int> GetMACHINEId ( string _MACHINE );



        Task<int> GetItemId ( string _itemcode );


        Task<int> GetEmpId ( string _empcode );



        Task<int> GetErrorId ( string _code );


        Task<string> GetCleanLastState ( string barcode , int cate );





        
    }
}