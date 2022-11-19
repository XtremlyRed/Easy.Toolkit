using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace Easy.Toolkit
{
    /// <summary>
    /// interface of <see cref="IRetry"/>
    /// </summary>
    public interface IRetry
    {
        /// <summary>
        /// handle <typeparamref name="TException"/>
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <returns></returns>
        IRetry OrHandle<TException>() where TException : Exception;

        /// <summary>
        /// handle <typeparamref name="TException"/>
        ///  <para> eligible will be deemed as having to try again </para>
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="exceptionChecker">retry condition</param>
        /// <returns></returns>
        IRetry OrHandle<TException>(Func<TException, bool> exceptionChecker) where TException : Exception;

        /// <summary>
        /// retry count
        /// </summary>
        /// <param name="tryCount"></param>
        /// <returns></returns>
        IRetry Count(int tryCount);

        /// <summary>
        /// retry interval
        /// </summary>
        /// <param name="intervalTimeSpans"></param>
        /// <returns></returns>
        IRetry Interval(params TimeSpan[] intervalTimeSpans);

        /// <summary>
        /// execute 
        /// </summary>
        /// <param name="action">execute callback</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>execute result</returns>
        RetryResult Execute(Action action, CancellationToken cancellationToken = default);

    }


    /// <summary>
    /// <see cref="Retry"/>
    /// </summary>
    public class Retry : IRetry
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private int tryCount = 1;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private TimeSpan[] intervalTimeSpans = new[] { TimeSpan.FromSeconds(1) };
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private readonly ExceptionDeclare exceptionDeclare = new ExceptionDeclare();

        /// <summary>
        /// handle <typeparamref name="TException"/>
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <returns></returns>
        public static IRetry Handle<TException>() where TException : Exception
        {
            Retry retry = new Retry();
            retry.exceptionDeclare.Declare<TException>();

            return retry;
        }

        /// <summary>
        /// handle <typeparamref name="TException"/>
        ///  <para> eligible will be deemed as having to try again </para>
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="exceptionChecker">retry condition</param>
        /// <returns></returns>
        public static IRetry Handle<TException>(Func<TException, bool> exceptionChecker) where TException : Exception
        {
            Retry retry = new Retry();
            retry.exceptionDeclare.Declare<TException>(exceptionChecker);

            return retry;
        }

        IRetry IRetry.OrHandle<TException>()
        {
            exceptionDeclare.Declare<TException>();
            return this;
        }

        IRetry IRetry.OrHandle<TException>(Func<TException, bool> exceptionChecker)
        {
            exceptionDeclare.Declare<TException>(exceptionChecker);
            return this;
        }


        IRetry IRetry.Count(int tryCount)
        {
            this.tryCount = tryCount.FromRange(1, int.MaxValue);
            return this;
        }

        IRetry IRetry.Interval(params TimeSpan[] intervalTimeSpans)
        {
            if (intervalTimeSpans != null && intervalTimeSpans.Length > 0)
            {
                this.intervalTimeSpans = intervalTimeSpans;
            }
            return this;
        }

        RetryResult IRetry.Execute(Action action, CancellationToken cancellationToken = default)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            RetryResult retryResult = new RetryResult
            {
                TryCount = tryCount
            };
            SemaphoreSlim semaphoreSlim = new SemaphoreSlim(0);

            Stopwatch stop = Stopwatch.StartNew();
            try
            {

                int currentCounter = 0;
                while (currentCounter < tryCount)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    RetryHistory history = new RetryHistory
                    {
                        RetryIndex = currentCounter
                    };

                    try
                    {
                        action.Invoke();

                        retryResult.Status = RetryStatus.Success;
                    }
                    catch (Exception ex)
                    {
                        history.Exception = ex;

                        retryResult.Histories.Add(history);

                        if (exceptionDeclare.ShouldRetryAgain(ex) == false)
                        {
                            throw ex;
                        }
                        semaphoreSlim.Wait(GetWaitTimeSpan(currentCounter++), cancellationToken);
                    }
                }
            }
            catch (Exception e)
            {
                retryResult.Unhandle = e;
                retryResult.Status = RetryStatus.Failure;
            }
            finally
            {
                semaphoreSlim.Dispose();
                stop.Stop();
                retryResult.TimeSpan = stop.Elapsed;
            }

            if (retryResult.Unhandle is null && retryResult.Histories.Count >= tryCount)
            {
                retryResult.Status = RetryStatus.Failure;
                retryResult.Unhandle = $"the maximum number of attempts has been reached";
            }

            return retryResult;


            TimeSpan GetWaitTimeSpan(int index1)
            {
                if (intervalTimeSpans.Length > index1)
                {
                    return intervalTimeSpans[index1];
                }

                return intervalTimeSpans[intervalTimeSpans.Length - 1];
            }
        }


    }


}
