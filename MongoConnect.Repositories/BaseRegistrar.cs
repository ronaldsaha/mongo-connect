using MongoConnect.Models;
using MongoConnect.Repositories.Utils;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repositories
{
    public abstract class BaseRegistrar
    {
        public void Register()
        {
            if (IsRegistered) return;
            IsRegistered = true;
            ObjectIDGenerator IDGenerator = new ObjectIDGenerator();
            BsonSerializer.RegisterIdGenerator(typeof(Identity), IDGenerator);
            BsonSerializer.RegisterIdGenerator(typeof(ObjectIdentity), IDGenerator);

            ObjectIDSerializer IDSerializer = new ObjectIDSerializer();
            BsonSerializer.RegisterSerializer(typeof(Identity), IDSerializer);
            BsonSerializer.RegisterSerializer(typeof(ObjectIdentity), IDSerializer);

            if (MongoConnectConfiguration.IsMultiTenant)
            {
                BsonClassMap.RegisterClassMap<Tenant>();

                var conventionPack = new ConventionPack();
                conventionPack.Add(new IgnoreExtraElementsConvention(true));
                ConventionRegistry.Register("GlobalIgnoreExtraElements", conventionPack, t => true);
            }
                

            OnRegistration();
        }
        public abstract void OnRegistration();

        private static bool IsRegistered = false;
    }
}
