using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;




namespace spiapi.irepository
{
    public interface IEmailRepository
    {
        

        Task<DataTable> GetDataListAsync ( string sql );
    }
}
