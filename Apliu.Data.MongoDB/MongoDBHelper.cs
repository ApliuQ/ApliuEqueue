using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apliu.Data.MongoDB
{
    public class MongoDBHelper
    {
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        private String _connectionStr = String.Empty;
        /// <summary>
        /// 数据库名称
        /// </summary>
        private String _databaseName = String.Empty;
        /// <summary>
        /// 集合名称
        /// </summary>
        private String _collectionName = String.Empty;

        /// <summary>
        /// Represents a database in MongoDB.
        /// </summary>
        private IMongoDatabase _mongoDatabase;
        /// <summary>
        /// The client interface to MongoDB.
        /// </summary>
        private IMongoClient _mongoClient;

        //private IMongoCollection<T> _mongoCollection;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="connectionStr">数据库链接字符串</param>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="collectionName">集合名称</param>
        public MongoDBHelper(String connectionStr, String databaseName, String collectionName)
        {
            _connectionStr = connectionStr;
            _databaseName = databaseName;
            _collectionName = collectionName;
            Initialization();
        }

        /// <summary>
        /// 初始化数据库链接
        /// </summary>
        private void Initialization()
        {
            try
            {
                MongoUrlBuilder mongoUrlBuilder = new MongoUrlBuilder(_connectionStr);
                _mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());//创建并实例化客户端
                _mongoDatabase = _mongoClient.GetDatabase(_databaseName);//实例化数据库
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取数据库中指定集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>()
        {
            return _mongoDatabase.GetCollection<T>(_collectionName);
        }

        /// <summary>
        /// 获取数据库中指定集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>(String collectionName)
        {
            return _mongoDatabase.GetCollection<T>(collectionName);
        }

        /// <summary>
        /// 向集合中插入Json字符串
        /// </summary>
        /// <param name="jsonData"></param>
        public void InsertJson(String jsonData)
        {
            IMongoCollection<BsonDocument> mongoCollection = GetCollection<BsonDocument>();
            if (BsonDocument.TryParse(jsonData, out BsonDocument bsonDocument))
            {
                mongoCollection.InsertOne(bsonDocument);
            }
        }

        /// <summary>
        /// 向集合中插入Object对象（需可序列化）
        /// </summary>
        /// <param name="objData"></param>
        public void InsertObject(Object objData)
        {
            InsertJson(JsonConvert.SerializeObject(objData));
        }

        public List<BsonDocument> FindObject(String key, String value)
        {
            IMongoCollection<BsonDocument> mongoCollection = GetCollection<BsonDocument>();
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq(key, value);
            List<BsonDocument> findResult = mongoCollection.Find(filter).ToList();
            return findResult;
        }

        public static void Test()
        {
            String strCon = "mongodb://admin:apliu2018@140.143.5.141:27017";
            String strDbn = "apliumq";
            String strColl = "equeue";
            MongoDBHelper mongoDBHelper = new MongoDBHelper(strCon, strDbn, strColl);

            mongoDBHelper.FindObject("id", "1");
        }
    }

    public class ObjData
    {
        public string id = "1";
        public string name = "2";
        public string remark = "3";
    }
}
