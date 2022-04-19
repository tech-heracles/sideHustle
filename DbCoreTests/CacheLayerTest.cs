using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheLayer;
using System.Web;

namespace DbCoreTests
{
    [TestClass]
    public class CacheLayerTest
    {
        public CacheLayerTest()
        {
            CacheConfiguration.ConfigureCache(TimeSpan.FromMinutes(100));
            HttpContext.Current = Utils.FakeHttpContext("http://dev.alphaweb.al");
        }
        [TestMethod]
        public void InitAppCacheTest()
        {
            Assert.IsNotNull(GlobalCacheManager.MyAppCache);
        }
        [TestMethod]
        public void InitSessionCacheTest()
        {
            GlobalCacheManager.CreateNewSessionCache(HttpContext.Current.Session.SessionID);
            Assert.IsNotNull(GlobalCacheManager.MySessionCache);
        }
        [TestMethod]
        public void SessionCache_IssAddCorrect_string()
        {
            GlobalCacheManager.MySessionCache.Add("oneItemString", "123456");
            Assert.AreEqual("123456", GlobalCacheManager.MySessionCache.Get<string>("oneItemString"));

        }
        [TestMethod]
        public void SessionCache_IssAddCorrect_int()
        {
            GlobalCacheManager.MySessionCache.Add("oneItemInt", 1235);
            Assert.AreEqual(1235, GlobalCacheManager.MySessionCache.Get<int>("oneItemInt"));

        }
        [TestMethod]
        public void SessionCache_IsUpdateCorrect_int()
        {
            GlobalCacheManager.MySessionCache.Add("oneItemnumber", 1235);
            GlobalCacheManager.MySessionCache.Add("oneItemnumber", 1238);
            GlobalCacheManager.MySessionCache.Add("oneItemnumber", 1239);
            Assert.AreEqual(1239, GlobalCacheManager.MySessionCache.Get<int>("oneItemnumber"));

        }
        [TestMethod]
        public void SessionCache_IssAddCorrect_obj()
        {
            var newItem = new oneItem();
            GlobalCacheManager.MySessionCache.Add("oneItemObj", newItem);
            Assert.AreEqual(newItem, GlobalCacheManager.MySessionCache.Get<oneItem>("oneItemObj"));

        }
        [TestMethod]
        public void SessionCache_IsSetCorrect_obj()
        {
            var newItem = new oneItem();
            GlobalCacheManager.MySessionCache.Set("oneItemObjSet", newItem, false);
            Assert.AreEqual(newItem, GlobalCacheManager.MySessionCache.Get<oneItem>("oneItemObjSet"));

        }
        [TestMethod]
        public void SessionCache_IsSetUpdateCorrect_obj()
        {
            var newItem = new oneItem();
            var newItem2 = new oneItem();
            newItem2.number = 100000;
            GlobalCacheManager.MySessionCache.Set("oneItemObjSet2", newItem, false);
            GlobalCacheManager.MySessionCache.Set("oneItemObjSet2", newItem2, false);
            Assert.AreEqual(newItem2, GlobalCacheManager.MySessionCache.Get<oneItem>("oneItemObjSet2"));

        }
        [TestMethod]
        public void SessionCache_clearSessionCacheCorrect()
        {
            var newItem = new oneItem();
            GlobalCacheManager.MySessionCache.Add("oneItemObj", newItem);
            GlobalCacheManager.MySessionCache.Clear();
            var noKeys = GlobalCacheManager.MySessionCache.AllKeys.Count == 0;
            var noItems = GlobalCacheManager.MySessionCache.Get<oneItem>("oneItemObj") == null;
            Assert.IsTrue(noKeys && noItems);
        }
        [TestMethod]
        public void SessionCache_clearAppCacheCorrect()
        {
            var newItem = new oneItem();
            GlobalCacheManager.MyAppCache.Add("oneItemObj", newItem);
            GlobalCacheManager.MyAppCache.Clear();
            var noKeys = GlobalCacheManager.MyAppCache.AllKeys.Count == 0;
            var noItems = GlobalCacheManager.MyAppCache.Get<oneItem>("oneItemObj") == null;
            Assert.IsTrue(noKeys && noItems);
        }
    }
    class oneItem
    {
        public int number { get; set; } = 100;
        public DateTime dt { get; set; } = DateTime.Now;
        public Guid guid { get; set; } = Guid.NewGuid();
    }

}
