using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace AsyncDeadlock
{
    public sealed class DedicatedThreadSynchronisationContext : SynchronizationContext, IDisposable
    {
        /// <summary>The queue of work items.</summary>
        private readonly BlockingCollection<KeyValuePair<SendOrPostCallback, object>> m_queue = new BlockingCollection<KeyValuePair<SendOrPostCallback, object>>();
        private readonly Thread m_thread = null;

        public DedicatedThreadSynchronisationContext()
        {
            m_thread = new Thread(ThreadWorkerDelegate);
            m_thread.Start(this);
        }

        public void Dispose()
        {
            m_queue.CompleteAdding();
        }

        /// <summary>Dispatches an asynchronous message to the synchronization context.</summary>
        /// <param name="callback">The System.Threading.SendOrPostCallback delegate to call.</param>
        /// <param name="state">The object passed to the delegate.</param>
        public override void Post(SendOrPostCallback callback, object state)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            m_queue.Add(new KeyValuePair<SendOrPostCallback, object>(callback, state));
        }

        /// <summary> As 
        public override void Send(SendOrPostCallback callback, object state)
        {
            using (var handledEvent = new ManualResetEvent(false))
            {
                Post(SendOrPostCallback_BlockingWrapper, Tuple.Create(callback, state, handledEvent));
                handledEvent.WaitOne();
            }
        }
        //=========================================================================================

        private static void SendOrPostCallback_BlockingWrapper(object state)
        {
            var innerCallback = (state as Tuple<SendOrPostCallback, object, ManualResetEvent>);
            try
            {
                innerCallback.Item1(innerCallback.Item2);
            }
            finally
            {
                innerCallback.Item3.Set();
            }
        }

        /// <summary>Runs an loop to process all queued work items.</summary>
        private void ThreadWorkerDelegate(object obj)
        {
            SetSynchronizationContext(obj as SynchronizationContext);

            try
            {
                foreach (var workItem in m_queue.GetConsumingEnumerable())
                    workItem.Key(workItem.Value);
            }
            catch (ObjectDisposedException) { }
        }
    }
}