using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Easy.Toolkit
{
    /// <summary>
    /// interface of <see cref="IRetryAsync{TResult}"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IRetryAsync<TResult>
    {
        /// <summary>
        /// handle <typeparamref name="TException"/>
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <returns></returns>
        IRetryAsync<TResult> OrHandle<TException>() where TException : Exception;

        /// <summary>
        /// handle <typeparamref name="TException"/>
        ///  <para> eligible will be deemed as having to try again </para>
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="exceptionChecker">retry condition</param>
        /// <returns></returns>
        IRetryAsync<TResult> OrHandle<TException>(Func<TException, bool> exceptionChecker) where TException : Exception;

        /// <summary>
        /// handle result
        ///  <para> eligible will be deemed as having to try again </para>
        /// </summary>
        /// <param name="resultChecker">retry condition</param>
        /// <returns></returns>
        IRetryAsync<TResult> OrResult(Func<TResult, bool> resultChecker);

        /// <summary>
        /// retry count
        /// </summary>
        /// <param name="tryCount"></param>
        /// <returns></returns>
        IRetryAsync<TResult> Count(int tryCount);

        /// <summary>
        /// retry interval
        /// </summary>
        /// <param name="intervalTimeSpans"></param>
        /// <returns></returns>
        IRetryAsync<TResult> Interval(params TimeSpan[] intervalTimeSpans);

        /// <summary>
        /// execute
        /// </summary>
        /// <param name="action">execute callback</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>execute result</returns>
        Task<RetryResult<TResult>> ExecuteAsync(Func<Task<TResult>> action, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// <see cref="RetryAsync{TResult}"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class RetryAsync<TResult> : IRetryAsync<TResult>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private int tryCount = 1;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private TimeSpan[] intervalTimeSpans = new[] { TimeSpan.FromSeconds(1) };
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private readonly ExceptionDeclare exceptionDeclare = new ExceptionDeclare();
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private readonly ResultDeclare<TResult> resultDeclare = new ResultDeclare<TResult>();
        
        /// <summary>
        /// handle <typeparamref name="TException"/>
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <returns></returns>
        public static IRetryAsync<TResult> Handle<TException>() where TException : Exception
        {
            RetryAsync<TResult> retry = new RetryAsync<TResult>();
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
        public static IRetryAsync<TResult> Handle<TException>(Func<TException, bool> exceptionChecker) where TException : Exception
        {
            RetryAsync<TResult> retry = new RetryAsync<TResult>();
            retry.exceptionDeclare.Declare<TException>(exceptionChecker);

            return retry;
        }

        /// <summary>
        /// handle <typeparamref name="TResult"/>
        ///  <para> eligible will be deemed as having to try again </para>
        /// </summary>
        /// <param name="resultChecker">retry condition</param>
        /// <returns></returns>
        public static IRetryAsync<TResult> Result(Func<TResult, bool> resultChecker)
        {
            RetryAsync<TResult> retry = new RetryAsync<TResult>();
            retry.resultDeclare.Declare(resultChecker);

            return retry;
        }

        IRetryAsync<TResult> IRetryAsync<TResult>.OrHandle<TException>()
        {
            exceptionDeclare.Declare<TException>();
            return this;
        }

        IRetryAsync<TResult> IRetryAsync<TResult>.OrHandle<TException>(Func<TException, bool> exceptionChecker)
        {
            exceptionDeclare.Declare<TException>(exceptionChecker);
            return this;
        }

        IRetryAsync<TResult> IRetryAsync<TResult>.OrResult(Func<TResult, bool> resultChecker)
        {
            resultDeclare.Declare(resultChecker);

            return this;
        }

        IRetryAsync<TResult> IRetryAsync<TResult>.Count(int tryCount)
        {
            this.tryCount = tryCount.FromRange(1, int.MaxValue);
            return this;
        }

        IRetryAsync<TResult> IRetryAsync<TResult>.Interval(params TimeSpan[] intervalTimeSpans)
        {
            if (intervalTimeSpans != null && intervalTimeSpans.Length > 0)
            {
                this.intervalTimeSpans = intervalTimeSpans;
            }
            return this;
        }

        async Task<RetryResult<TResult>> IRetryAsync<TResult>.ExecuteAsync(Func<Task<TResult>> action, CancellationToken cancellationToken = default)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            RetryResult<TResult> retryResult = new RetryResult<TResult>
            {
                TryCount = tryCount
            };
            SemaphoreSlim semaphoreSlim = new SemaphoreSlim(-0);

            Stopwatch stop = Stopwatch.StartNew();
            try
            {

                int currentCounter = 0;
                while (currentCounter < tryCount)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    RetryHistory<TResult> history = new RetryHistory<TResult>
                    {
                        RetryIndex = currentCounter
                    };

                    try
                    {
                        TResult resul = await action.Invoke();

                        if (resultDeclare.ShouldRetryAgain(resul) == false)
                        {
                            retryResult.Status = RetryStatus.Success;
                            retryResult.Result = resul;
                            break;
                        }

                        currentCounter++;
                        history.Result = resul;
                        retryResult.Histories.Add(history);

                        await semaphoreSlim.WaitAsync(GetWaitTimeSpan(currentCounter), cancellationToken);

                    }
                    catch (Exception ex)
                    {
                        history.Exception = ex;

                        retryResult.Histories.Add(history);

                        if (exceptionDeclare.ShouldRetryAgain(ex) == false)
                        {
                            throw ex;
                        }

                        await semaphoreSlim.WaitAsync(GetWaitTimeSpan(currentCounter++), cancellationToken);
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

            if (retryResult.Histories.Count >= tryCount)
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
