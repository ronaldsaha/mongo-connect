﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Models
{
    public interface IdentityProvider
    {
        Identity GetEmptyID();
        Identity GetNewID();
        Identity ParseID(string id);
    }
}