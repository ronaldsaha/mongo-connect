using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Models
{
    public abstract class TenantEntity : Entity
    {
        public TenantEntity() : base() { TenantId = new NullIdentity(); }
        public TenantEntity(Identity id) : base(id) { TenantId = new NullIdentity(); }
        public Identity TenantId { get; internal set; }
    }
}