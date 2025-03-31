using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 


namespace spiapi.irepository
{
    public interface ISpiDataRepository
    {

        Task<DataTable> GetDataListAsync ( string sql );




    }
}
