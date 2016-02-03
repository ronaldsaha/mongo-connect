using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repository
{
    public class ObjectIDSerializer : IBsonSerializer
    {
        //public object Deserialize(MongoDB.Bson.IO.BsonReader bsonReader, Type nominalType, Type actualType, IBsonSerializationOptions options)
        //{
        //    return Deserialize(bsonReader, nominalType, options);
        //}

        //public object Deserialize(MongoDB.Bson.IO.BsonReader bsonReader, Type nominalType, IBsonSerializationOptions options)
        //{
        //    //short pid;
        //    //int timeStamp, machine, increment;
        //    //bsonReader.ReadObjectId(out timeStamp, out machine, out pid, out increment);
        //    //return new MongoIdentity(new MongoDB.Bson.ObjectId(timeStamp, machine, pid, increment));
        //    return new ObjectIdentity(bsonReader.ReadObjectId());
        //}

        //public IBsonSerializationOptions GetDefaultSerializationOptions()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool GetDocumentId(object document, out object id, out Type idNominalType, out IIdGenerator idGenerator)
        //{
        //    throw new NotImplementedException();
        //}

        //public BsonSerializationInfo GetItemSerializationInfo()
        //{
        //    throw new NotImplementedException();
        //}

        //public BsonSerializationInfo GetMemberSerializationInfo(string memberName)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Serialize(MongoDB.Bson.IO.BsonWriter bsonWriter, Type nominalType, object value, IBsonSerializationOptions options)
        //{
        //    ObjectIdentity id = value as ObjectIdentity;
        //    if (value == null)
        //        id = new ObjectIdentity();
        //    //bsonWriter.WriteObjectId(id.IdentityValue.Timestamp, id.IdentityValue.Machine, id.IdentityValue.Pid, id.IdentityValue.Increment);
        //    bsonWriter.WriteObjectId(id.IdentityValue);
        //}

        //public void SetDocumentId(object document, object id)
        //{
        //    throw new NotImplementedException();
        //}
        public Type ValueType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            throw new NotImplementedException();
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            throw new NotImplementedException();
        }
    }
}
