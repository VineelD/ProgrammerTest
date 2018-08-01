using Consumer_ns;
using Producer_ns;
using QueueService_ns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            QueueService qs = new QueueService();//Create Queue Service instance
            Producer p = new Producer(qs); //Create Producer Instance and inject the Queue Service Instance
            Consumer c = new Consumer(qs);//Create Consumer Instance and inject the Queue Service Instance

            var t1 = Task.Factory.StartNew(() => p.ProduceItems()); //Start the Producer Thread
            var t2 = Task.Factory.StartNew(() => c.ConsumeItems());//Start the Consumer Thread
            Task.WaitAll(t1, t2); //Wait till all the threads complete
            Console.ReadLine();
        }
    }
}
