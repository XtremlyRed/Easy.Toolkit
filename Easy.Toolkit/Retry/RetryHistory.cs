using System;
using System.Diagnostics;

namespace Easy.Toolkit
{
    /// <summary>
    /// execute history record
    /// </summary>
    [DebuggerDisplay("[Time:{RetryTime}]   [RetryIndex:{RetryIndex}]   {Exception}")]
    public class RetryHistory
    { 
        /// <summary>
        /// execute time
        /// </summary>
        public readonly DateTime RetryTime = DateTime.Now;

        /// <summary>
        /// execute index
        /// </summary>
        public int RetryIndex { get; internal set; }

        /// <summary>
        /// execute exception
        /// </summary> 
        public Exception Exception { get; internal set; }
    }

    /// <summary>
    /// execute history record with <typeparamref name="TResult"/>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    [DebuggerDisplay("[Time:{RetryTime}]   [RetryIndex:{RetryIndex}]   [Result:{Result}]   {Exception}")]
    public class RetryHistory<TResult> : RetryHistory
    { 

        /// <summary>
        /// execute result
        /// </summary>
        public TResult Result { get; internal set; }
    }
}
