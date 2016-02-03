using MongoConnect.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoConnect.Repository
{
    public class MongoEntityCollection<TEntity> where TEntity : Entity
    {
        public MongoEntityCollection(MongoContext context, IMongoDatabase database, string collectionName)
            : this(context, new MongoEntitySerializer<TEntity>(), database, collectionName)
        {
        }

        public MongoEntityCollection(MongoContext context, DocumentSerializer<TEntity> serializer, IMongoDatabase database, string collectionName)
        {
            Context = context;
            Serializer = serializer;
            Collection = database.GetCollection<TEntity>(collectionName);
        }

        public bool Insert(TEntity entity)
        {
            //return Collection.InsertOneAsync(entity);
            throw new NotImplementedException();
        }

        public bool Save(TEntity entity)
        {
            //return Collection.Save(Serializer.Serialize(entity)).Ok;
            throw new NotImplementedException();
        }

        public bool Remove(MongoQuery query)
        {
            //return Collection.Remove(query).Ok;
            throw new NotImplementedException();
        }

        public bool RemoveAll()
        {
            //return Collection.RemoveAll().Ok;
            throw new NotImplementedException();
        }

        public TEntity FindOne(MongoQuery query)
        {
            //return Serializer.Deserialize(Collection.FindOne(query));
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindAll()
        {
            //return Serializer.Deserialize(Collection.FindAll().GetEnumerator());
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindAll(int offset, int pageSize)
        {
            //return Serializer.Deserialize(Collection.FindAll().SetSkip(offset).SetLimit(pageSize).GetEnumerator());
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindAll(MongoQuery query)
        {
            //return Serializer.Deserialize(Collection.Find(query).GetEnumerator());
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindAll(MongoQuery query, int offset, int pageSize)
        {
            //return Serializer.Deserialize(Collection.Find(query).SetSkip(offset).SetLimit(pageSize).GetEnumerator());
            throw new NotImplementedException();
        }

        public long Count()
        {
            //return Collection.Count();
            throw new NotImplementedException();
        }

        MongoContext Context;
        IMongoCollection<TEntity> Collection;
        DocumentSerializer<TEntity> Serializer;
    }
}
