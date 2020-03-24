using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Magni.APIClient.V2.Models.BulkQueue
{
    public class QueueEngine
    {
        string email;
        int queueAssignerDelay = 2000;
        int queueProcessDelay = 2000;

        private CancellationTokenSource queueAssignerToken;
        private CancellationTokenSource queueProcessToken;

        /// <summary>
        /// Waiting queue where items are placed before assigner to a processing queue
        /// </summary>
        private ConcurrentQueue<QueueItem> waitingQueue;
        private ConcurrentQueue<APIDocumentCreateResponse> DocumentsCreated { get; set; }
        private object ProcessingQueues { get; set; }


        Dictionary<string, SortedList<DateTime, List<QueueItem>>> processingQueue = new Dictionary<string, SortedList<DateTime, List<QueueItem>>>();

        /// <summary>
        /// Queueu Engine  constructor
        /// </summary>
        /// <param name="username">API account email</param>
        public QueueEngine(string username)
        {
            this.email = username;

            this.waitingQueue = new ConcurrentQueue<QueueItem>();
            this.DocumentsCreated = new ConcurrentQueue<APIDocumentCreateResponse>();

            QueueAssigner();
            ProcessQueue();
        }


        public void AddToQueue(QueueItem newItem)
        {
            this.waitingQueue.Enqueue(newItem);
        }

        public void AddToQueue(IEnumerable<QueueItem> queue)
        {
            foreach (var item in queue)
            {
                this.AddToQueue(item);
            }
        }


        /// <summary>
        /// Push from waiting queue and place in processing waiting for process queue.
        /// </summary>
        private void QueueAssigner()
        {
            this.queueAssignerToken = new CancellationTokenSource();

            Task.Run(() => {
                QueueItem newItem;

                while (!queueAssignerToken.IsCancellationRequested)
                {
                    while(!this.waitingQueue.IsEmpty)
                    {
                        if(this.waitingQueue.TryDequeue(out newItem))
                        {
                            if (!this.processingQueue.ContainsKey(newItem.Token))
                            {
                                this.processingQueue.Add(newItem.Token, new SortedList<DateTime, List<QueueItem>>());
                            }

                            if (!this.processingQueue[newItem.Token].ContainsKey(newItem.Document.Date.Date))
                            {
                                // use date without hours or miliseconds
                                this.processingQueue[newItem.Token].Add(newItem.Document.Date.Date, new List<QueueItem>());
                            }

                            this.processingQueue[newItem.Token][newItem.Document.Date.Date].Add(newItem);
                        }
                    }

                    Task.Delay(queueAssignerDelay);
                }
            }, queueAssignerToken.Token);
        }

        /// <summary>
        /// Get and assign to available slots documents to process.
        /// The number of simultaneous processes is limited to the slots number.
        /// </summary>
        private void ProcessQueue()
        {
            this.queueProcessToken = new CancellationTokenSource();

            Task.Run(() =>
            {
                while (!queueProcessToken.IsCancellationRequested)
                {
                    Parallel.ForEach(new List<QueueItem>(), action =>
                    {
                        Invoicing apiClient = new Invoicing(null, this.email, "");

                        //apiClient.InvoiceSimplifiedCreate(action.Client, action.Document, action.IsClosed, action.EMailToSend);

                    });

                    Task.Delay(queueProcessDelay);
                }
            }, this.queueProcessToken.Token);
        }


        public IReadOnlyCollection<APIDocumentCreateResponse> GetProcessedDocuments()
        {
            return this.DocumentsCreated;
        }
    }
}
