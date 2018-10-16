﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apliu.Data.MongoDB
{
    public class DbTestClass
    {
        public static void Test()
        {
            String strCon = String.Empty;
            String strDbn = "apliumq";
            String strColl = "equeue";
            MongoDBHelper mongoDBHelper = new MongoDBHelper(strCon, strDbn, strColl);
            //mongoDBHelper.InsertObject(new TestData());

            //所有的查找, 数据类型也必须一致, 一般是String Int Bool等常用数据类型
            Dictionary<string, Object> dicData = new Dictionary<string, Object>() { };
            dicData.Add("name", "ObjData");
            List<TestData> testDatas01 = mongoDBHelper.FindObjectList<TestData>(dicData);

            Dictionary<string, Object> dicData03 = new Dictionary<string, Object>() { };
            dicData03.Add("num", 39);
            List<TestData> testDatas03 = mongoDBHelper.FindObjectList<TestData>(dicData03);

            Dictionary<string, Object> dicData02 = new Dictionary<string, Object>() { };
            dicData02.Add("num", 30);
            List<TestData> testDatas02 = mongoDBHelper.FindObjectGreater<TestData>(dicData02);

            //首字符大于则查找成功，比如“3”也可以通过
            Dictionary<string, Object> dicData04 = new Dictionary<string, Object>() { };
            dicData04.Add("remark", "2018-10-16");
            List<TestData> testDatas04 = mongoDBHelper.FindObjectGreater<TestData>(dicData04);

            Object[] intArry = new Object[2] { 2, 39 };
            List<TestData> testDatas05 = mongoDBHelper.FindObjectIn<TestData>("num", intArry);

            //模糊查询
            List<TestData> testDatas06 = mongoDBHelper.FindAllLinq<TestData>().Where(a => a.remark.Contains(" 13:")).ToList();

            Dictionary<string, Object> dicData06 = new Dictionary<string, Object>() { };
            dicData06.Add("num", 2);
            long dele = mongoDBHelper.DeleteMany(dicData06);

            Dictionary<string, Object> dicData07 = new Dictionary<string, Object>() { };
            dicData07.Add("num", 39);
            Dictionary<string, Object> dicData08 = new Dictionary<string, Object>() { };
            dicData08.Add("name", "heiheihei");
            long update = mongoDBHelper.UpdateMany(dicData07, dicData08);
        }
    }

    public class TestData
    {
        public string id = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
        public string name = "ObjData";
        public string remark = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public Int32 num = DateTime.Now.Second;
    }
}
