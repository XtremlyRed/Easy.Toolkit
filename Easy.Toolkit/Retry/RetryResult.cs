using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Easy.Toolkit
{
    /// <summary>
    /// execute result status
    /// </summary>
    public enum RetryStatus
    {
        /// <summary>
        /// execute success
        /// </summary>
        Success,
        /// <summary>
        /// execute failure
        /// </summary>
        Failure,

    }

    /// <summary>
    /// <see cref="RetryResult"/>
    /// </summary>
    [DebuggerDisplay("{Status}")]
    public class RetryResult
    {
        /// <summary>
        /// create a new retry result
        /// </summary>
        public RetryResult()
        {
            Histories = new Collection<RetryHistory>();
        }

        /// <summary>
        /// try history 
        /// </summary>
        public ICollection<RetryHistory> Histories { get; }

        /// <summary>
        /// total used time
        /// </summary>
        public TimeSpan TimeSpan { get; internal set; }

        /// <summary>
        /// try count
        /// </summary>
        public int TryCount { get; internal set; }

        /// <summary>
        /// execute result
        /// </summary>
        public RetryStatus Status { get; internal set; } = RetryStatus.Success;

        /// <summary>
        /// unhandle message
        /// </summary> 
        public object Unhandle { get; internal set; }
    }

    /// <summary>
    /// <see cref="RetryResult{TResult}"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    [DebuggerDisplay("[{Status}]   [{Result}]")]
    public class RetryResult<TResult> : RetryResult
    {
        /// <summary>
        /// execute result
        /// </summary>
        public TResult Result { get; internal set; }


        /// <summary>
        /// get result from <paramref name="retryResult"/>
        /// </summary>
        /// <Exception cref="ArgumentNullException"><paramref name="retryResult"/> is null</Exception>
        /// <param name="retryResult"><typeparamref name="TResult"/></param>
        public static implicit operator TResult(RetryResult<TResult> retryResult)
        {
            if (retryResult == null)
            {
                throw new ArgumentNullException(nameof(retryResult));
            }

            return retryResult.Result;
        }
    }

}
