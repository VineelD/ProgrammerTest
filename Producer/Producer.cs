using QueueService_ns;
using System;
using System.IO;
using System.Threading;
using System.Xml;

namespace Producer_ns
{
   
    public class Producer
    {
        IQueueService _qs;

        public Producer(IQueueService qs) //Injecting the Dependency via Constructor
        {
            this._qs = qs;
        }
        
        static string GetXml(string url)// Function to Get the Xml String from Xml File
        {
            using (XmlReader xr = XmlReader.Create(url, new XmlReaderSettings() { IgnoreWhitespace = true ,DtdProcessing = DtdProcessing.Parse}))
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (XmlWriter xw = XmlWriter.Create(sw))
                    {
                        xw.WriteNode(xr, false);
                    }
                    return sw.ToString();
                }
            }
        }
        

        public void ProduceItems()//Function to enqueue the items in to the queue
        {
            string[] files = Directory.GetFiles(@"C:\\ProgrammerTest\Data\Computers");
            int i = 0;
            foreach(var file in files)
            {
                var queue = new QueuedObject
                {
                    QueueID = i,
                    ProducerThreadID = Thread.CurrentThread.ManagedThreadId,
                    EnqueueDateTime = DateTime.Now,
                    inputXml = GetXml(file),
                    xsltString = GetXml(@"C:\\ProgrammerTest\Resources\Computer.xslt"),
                    filename = Path.GetFileName(file)
                };

               
                 _qs.Enqueue(queue);
                

                Console.WriteLine
                    (
                    "Enqueued: " + queue.QueueID +
                    "\t" + "Producer ThreadID :" + queue.ProducerThreadID +
                    "\t" + queue.EnqueueDateTime.ToLongTimeString() +
                    "\t" + "XmlString   :" + queue.inputXml
                    );
                i++;
            }
        }
    }
}
