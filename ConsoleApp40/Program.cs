
using System;
using System.Collections;
using KdSoft.Lmdb;


using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using PutOptions = KdSoft.Lmdb.PutOptions;


namespace lmdb
{
    class Program
    {

        public static void Connect()
        {
            var envConfig = new LmdbEnvironmentConfiguration(10);
            using (var env = new LmdbEnvironment(envConfig))
            {

                env.Open("C:\\Users\\Mr.Den\\source\\repos\\LMDBRULIT");
                Database dbase;
                var dbConfig = new KdSoft.Lmdb.DatabaseConfiguration(DatabaseOptions.Create);

                using (var tx = env.BeginDatabaseTransaction(TransactionModes.None))
                {
                    List<String> list = new List<String>();
                    list.Add("string");
                    list.Add("другая string");
                    list.Add("ещё одна string");

                    HashSet<Object> set = new HashSet<Object>();
                    set.Add("string");
                    set.Add(22.33);
                    set.Add(111);

                    ArrayList array = new ArrayList();
                    array.AddRange(new String[] { "Hello", "LMDB!" });
                    array.Add("first");
                    array.Add(2.3);

                    Hashtable htab = new Hashtable();
                    htab.Add("redis", "Hello, LMDB with mdbx.NET!");
                    htab.Add("float", 11.22);
                    htab["int"] = 2019;


                    Dictionary<Object, Object> dic = new Dictionary<Object, Object>();
                    dic.Add(1, "notepad.exe");
                    dic.Add(2, "paint.exe");
                    dic.Add(3, "excel.exe");
                    dic.Add(4, "bor.exe");

                    dbase = tx.OpenDatabase("TestDb1", dbConfig);

                    int key = 1;
                    int key2 = 2;
                    int key3 = 3;
                    int key4 = 4;
                    int key5 = 5;

                    var keyBuf = BitConverter.GetBytes(key);
                    var keyBuf2 = BitConverter.GetBytes(key2);
                    var keyBuf3 = BitConverter.GetBytes(key3);
                    var keyBuf4 = BitConverter.GetBytes(key4);
                    var keyBuf5 = BitConverter.GetBytes(key5);

                    var serialize = JsonSerializer.Serialize(list);
                    var serialize2 = JsonSerializer.Serialize(set);
                    var serialize3 = JsonSerializer.Serialize(array);
                    var serialize4 = JsonSerializer.Serialize(htab);
                    var serialize5 = Newtonsoft.Json.JsonConvert.SerializeObject(dic);


                    dbase.Put(tx, keyBuf, Encoding.UTF8.GetBytes(serialize), PutOptions.None);
                    dbase.Put(tx, keyBuf2, Encoding.UTF8.GetBytes(serialize2), PutOptions.None);
                    dbase.Put(tx, keyBuf3, Encoding.UTF8.GetBytes(serialize3), PutOptions.None);
                    dbase.Put(tx, keyBuf4, Encoding.UTF8.GetBytes(serialize4), PutOptions.None);
                    dbase.Put(tx, keyBuf5, Encoding.UTF8.GetBytes(serialize5), PutOptions.None);

                    ReadOnlySpan<byte> getData;
                    ReadOnlySpan<byte> getData2;
                    ReadOnlySpan<byte> getData3;
                    ReadOnlySpan<byte> getData4;
                    ReadOnlySpan<byte> getData5;

                    dbase.Get(tx, keyBuf, out getData);
                    dbase.Get(tx, keyBuf2, out getData2);
                    dbase.Get(tx, keyBuf3, out getData3);
                    dbase.Get(tx, keyBuf4, out getData4);
                    dbase.Get(tx, keyBuf5, out getData5);


                    var pt = Encoding.UTF8.GetString(getData);
                    var pt2 = Encoding.UTF8.GetString(getData2);
                    var pt3 = Encoding.UTF8.GetString(getData3);
                    var pt4 = Encoding.UTF8.GetString(getData4);
                    var pt5 = Encoding.UTF8.GetString(getData5);

                    var deserialize = Newtonsoft.Json.JsonConvert.DeserializeObject(pt);
                    var deserialize2 = Newtonsoft.Json.JsonConvert.DeserializeObject(pt2);
                    var deserialize3 = Newtonsoft.Json.JsonConvert.DeserializeObject(pt3);
                    var deserialize4 = Newtonsoft.Json.JsonConvert.DeserializeObject(pt4);
                    var deserialize5 = Newtonsoft.Json.JsonConvert.DeserializeObject(pt5);

                    Console.WriteLine(deserialize);
                    Console.WriteLine(deserialize2);
                    Console.WriteLine(deserialize3);
                    Console.WriteLine(deserialize4);
                    Console.WriteLine(deserialize5);

                    tx.Commit();


                }


                

            }
        }


        static void Main(string[] args)
        {
            Connect();

        }
    }




}

