﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Models
{
    public interface TenantProvider
    {
        string GetTenantCollectionName();
        string GetCurrentTenantKey();
    }
}
