﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using billservice.models;

namespace billservice.interfaces
{
    public interface IBillType
    {
        bool IsExistName ( string typename , string mobile );

        bool IsExistType ( int billtypeid , bool isout );

        bool IsExistSystemType ( int billtypeid );

        bool IsExistUserType ( int billtypeid , string mobile );

        bool Save ( billtype _billtype );

        Task<bool> SaveAsync ( billtype _billtype );
    }
}
