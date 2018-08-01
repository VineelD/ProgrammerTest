using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueueService_ns;
using Producer_ns;
using Consumer_ns;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            QueueService qs = new QueueService();
            Producer p = new Producer(qs);
            Consumer c = new Consumer(qs);

            var t1 = Task.Factory.StartNew(() => p.ProduceItems());
            var t2 = Task.Factory.StartNew(() => c.ConsumeItems());
            Task.WaitAll(t1, t2);
        }
    }
}
