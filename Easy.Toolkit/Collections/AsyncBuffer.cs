using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Easy.Toolkit
{



    /// <summary>
    ///  a class of <see cref="AsyncBuffer{TTarget}"/>
    /// </summary>
    /// <typeparam name="TTarget"></typeparam> 
    public class AsyncBuffer<TTarget> : IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private Queue<TTarget> Queue = new();
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private bool disposed;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly object putSyncRoot = new();
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private SemaphoreSlim popupLocker = new(0, 1);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private SemaphoreSlim putLocker = new(1, 1);
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private SemaphoreSlim asyncLocker = new(1, 1);

        /// <summary>
        /// Constructor
        /// </summary>
        public AsyncBuffer()
        {
        }

        /// <summary>
        /// Put a <typeparamref name="TTarget"/> Array  into the Buffer
        /// </summary>
        /// <param name="targets">array</param>
        /// <returns>put count</returns>
        /// <Exception cref="ObjectDisposedException"></Exception>
        /// <Exception cref="ArgumentNullException"></Exception>
        public void Put(params TTarget[] targets)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("Put handle in disposed object");
            }

            if (targets is null || targets.Length == 0)
            {
                return;
            }
            try
            {
                putLocker.Wait();

                foreach (TTarget target in targets)
                {
                    Queue.Enqueue(target);
                }
            }
            finally
            {
                if (popupLocker.CurrentCount == 0)
                {
                    popupLocker.Release();
                }

                putLocker.Release();
            }
        }





        /// <summary>
        /// Popup an instance form the buffer async
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <Exception cref="ObjectDisposedException"></Exception>
        public async Task<TTarget> PopupAsync(CancellationToken cancellationToken = default)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("Popup handle in disposed object");
            }

            await asyncLocker.WaitAsync(cancellationToken);

            try
            {
                while (Queue.Count < 1)
                {
                    await popupLocker.WaitAsync(cancellationToken);
                }

                return Queue.Dequeue();
            }
            finally
            {
                asyncLocker.Release();
            }
        }



        /// <summary>
        /// Popup an instance form the buffer async
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <Exception cref="ObjectDisposedException"></Exception>
        public TTarget Popup(CancellationToken cancellationToken = default)
        {
            if (disposed)
            {
                throw new ObjectDisposedException("Popup handle in disposed object");
            }

            asyncLocker.Wait(cancellationToken);


            try
            {

                while (Queue.Count < 1)
                {
                    popupLocker.Wait(cancellationToken);
                }

                return Queue.Dequeue();
            }
            finally
            {
                asyncLocker.Release();
            }
        }

        /// <summary>
        /// Dispose the buffer
        /// </summary>
        public void Dispose()
        {
            disposed = true;
            Queue?.Clear();
            Queue = null;

            asyncLocker?.Dispose();
            asyncLocker = null;

            putLocker?.Dispose();
            putLocker = null;

            popupLocker?.Dispose();
            popupLocker = null;
        }
    }
}
