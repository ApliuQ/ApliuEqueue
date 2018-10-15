using Apliu.Data.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEqueue
{
    class Program
    {
        // 定义接口
        protected static IMongoDatabase _database;
        // 定义客户端
        protected static IMongoClient _client;
        static void Main(string[] args)
        {
            MongoDBHelper.Test();
            Console.ReadKey();
        }
    }
}
