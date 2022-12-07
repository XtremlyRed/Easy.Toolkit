using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

namespace Easy.Toolkit
{
    /// <summary>
    /// <see cref="IRetry{TResult}"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IRetry<TResult>
    {
        /// <summary>
        /// handle Exception
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <returns></returns>
        IRetry<TResult> OrHandle<TException>() where TException : Exception;

        /// <summary>
        /// handle Exception
        ///  <para> eligible will be deemed as having to try again </para>
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="exceptionChecker">retry condition</param>
        /// <returns></returns>
        IRetry<TResult> OrHandle<TException>(Func<TException, bool> exceptionChecker) where TException : Exception;

        /// <summary>
        /// handle result
        /// <para> eligible will be deemed as having to try again </para>
        /// </summary>
        /// <param name="resultChecker">retry condition</param>
        /// <returns></returns>
        IRetry<TResult> OrResult(Func<TResult, bool> resultChecker);

        /// <summary>
        /// retry count
        /// </summary>
        /// <param name="tryCount"></param>
        /// <returns></returns>
        IRetry<TResult> Count(int tryCount);

        /// <summary>
        /// retry interval <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="intervalTimeSpans"></param>
        /// <returns></returns>
        IRetry<TResult> Interval(params TimeSpan[] intervalTimeSpans);

        /// <summary>
        /// execute delegate
        /// </summary>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>execute <see cref="RetryResult{TResult}"/></returns>
        RetryResult<TResult> Execute(Func<TResult> action, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// <see cref="Retry{TResult}"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class Retry<TResult> : IRetry<TResult>
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
        public static IRetry<TResult> Handle<TException>() where TException : Exception
        {
            Retry<TResult> retry = new Retry<TResult>();
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
        public static IRetry<TResult> Handle<TException>(Func<TException, bool> exceptionChecker) where TException : Exception
        {
            Retry<TResult> retry = new Retry<TResult>();
            retry.exceptionDeclare.Declare<TException>(exceptionChecker);

            return retry;
        }

        /// <summary>
        /// handle <typeparamref name="TResult"/>
        ///  <para> eligible will be deemed as having to try again </para>
        /// </summary>
        /// <param name="resultChecker">retry condition</param>
        /// <returns></returns>
        public static IRetry<TResult> Result(Func<TResult, bool> resultChecker)
        {
            Retry<TResult> retry = new Retry<TResult>();
            retry.resultDeclare.Declare(resultChecker);

            return retry;
        }

        IRetry<TResult> IRetry<TResult>.OrHandle<TException>()
        {
            exceptionDeclare.Declare<TException>();
            return this;
        }

        IRetry<TResult> IRetry<TResult>.OrHandle<TException>(Func<TException, bool> exceptionChecker)
        {
            exceptionDeclare.Declare<TException>(exceptionChecker);
            return this;
        }

        IRetry<TResult> IRetry<TResult>.OrResult(Func<TResult, bool> resultChecker)
        {
            resultDeclare.Declare(resultChecker);

            return this;
        }

        IRetry<TResult> IRetry<TResult>.Count(int tryCount)
        {
            this.tryCount = tryCount.FromRange(1, int.MaxValue);
            return this;
        }

        IRetry<TResult> IRetry<TResult>.Interval(params TimeSpan[] intervalTimeSpans)
        {
            if (intervalTimeSpans != null && intervalTimeSpans.Length > 0)
            {
                this.intervalTimeSpans = intervalTimeSpans;
            }
            return this;
        }

        RetryResult<TResult> IRetry<TResult>.Execute(Func<TResult> action, CancellationToken cancellationToken = default)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            RetryResult<TResult> retryResult = new RetryResult<TResult>
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

                    RetryHistory<TResult> history = new RetryHistory<TResult>
                    {
                        RetryIndex = currentCounter
                    };

                    try
                    {
                        TResult resul = action.Invoke();

                        if (resultDeclare.ShouldRetryAgain(resul) == false)
                        {
                            retryResult.Status = RetryStatus.Success;
                            retryResult.Result = resul;
                            break;
                        }

                        currentCounter++;
                        history.Result = resul;
                        retryResult.Histories.Add(history);

                        semaphoreSlim.Wait(GetWaitTimeSpan(currentCounter), cancellationToken);

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
