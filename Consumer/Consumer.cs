using QueueService_ns;
using System;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Xsl;

namespace Consumer_ns
{
    
    public class Consumer
    {

        IQueueService _qs;
        public Consumer(IQueueService qs) //Injecting the Dependency via Constructor
        {
            this._qs = qs;
        }
        public string TransformXMLToHTML(string inputXml, string xsltString)// Function to transform Xml to Html String
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            using (XmlReader reader = XmlReader.Create(new StringReader(xsltString), new XmlReaderSettings() { IgnoreWhitespace = true, DtdProcessing = DtdProcessing.Parse }))
            {
                transform.Load(reader);
            }
            StringWriter results = new StringWriter();
            using (XmlReader reader = XmlReader.Create(new StringReader(inputXml), new XmlReaderSettings() { IgnoreWhitespace = true, DtdProcessing = DtdProcessing.Parse }))
            {
                transform.Transform(reader, null, results);
            }
            return results.ToString();
        }
        public void ProcessQueue(QueuedObject queue) // Process the queue item
        {
            string processedHtmlstring = TransformXMLToHTML(queue.inputXml, queue.xsltString);

            Console.WriteLine
                (
                "Dequeued: " + queue.QueueID +
                "\t" + "Consumer ThreadID :" + Thread.CurrentThread.ManagedThreadId +
                "\t" + DateTime.Now.ToLongTimeString() +
                "\t" + "processedHtmlstring :" + processedHtmlstring
                );
            System.IO.File.WriteAllText(@"C:\\ProgrammerTest\Data\Output\" + queue.filename.ToLower().Replace(".xml", ".html"), processedHtmlstring);
        }
        public void ConsumeItems() // Consume Items from the queue
        {
            while (true)
            {
                
                QueuedObject qo = _qs.Dequeue();
                if(qo != null)
                ProcessQueue(qo);
            }
        }
    }
}
