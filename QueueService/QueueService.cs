using System;
using System.Collections.Concurrent;
using System.Diagnostics;


namespace QueueService_ns
{
    public class QueuedObject
    {
        public int QueueID { get; set; }
        public int ConsumerThreadID { get; set; }
        public int ProducerThreadID { get; set; }
        public string inputXml { get; set; }
        public string xsltString { get; set; }
        public string filename { get; set; }
        public DateTime EnqueueDateTime { get; set; }
        public DateTime DequeueDateTime { get; set; }
    }
    public interface IQueueService
    {
        void Enqueue(QueuedObject qo);
        QueuedObject Dequeue();
    }
    public class QueueService : IQueueService
    {
        ConcurrentQueue<QueuedObject> _queue;

        public QueueService()
        {
            _queue = new ConcurrentQueue<QueuedObject>();
        }

        public void Enqueue(QueuedObject qo)
        {
            
            _queue.Enqueue(qo);
        }

        public QueuedObject Dequeue()
        {

            try
            {
                QueuedObject qo;
                if (_queue.TryDequeue(out qo))
                    return qo;
                else
                    return null;
            }
            catch (NullReferenceException ex)
            {
                string w = ex.Message;
                Debug.WriteLine(ex.Message);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                
            }

            return null;
        
          
        }
    }
}
