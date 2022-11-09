
using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Easy.Toolkit
{
    /// <summary>
    /// <para> class of  <see cref="EasyTimer"/></para>
    /// <para> a timer helper class of the execute time</para>
    /// </summary>
    public sealed class EasyTimer : IDisposable
    {

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly ConcurrentDictionary<object, EasyTimer> timerMapper = new();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Stopwatch stopwatch;



        private EasyTimer()
        {
            stopwatch = Stopwatch.StartNew();
        }
        /// <summary>
        /// dispose this timer
        /// </summary>
        ~EasyTimer()
        {
            Dispose();
        }
        /// <summary>
        /// dispose this timer
        /// </summary>
        public void Dispose()
        {
            Stop();
        }


        private void Stop()
        {
            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }
        }

        /// <summary>
        /// stop this timer and return  execute  use time
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetTimeSpan()
        {
            Stop();

            return stopwatch.Elapsed;
        }

        /// <summary>
        ///  stop this timer and return  execute  use time
        /// </summary>
        /// <returns></returns>
        public long GetTotalMilliseconds()
        {
            Stop();

            return stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Timer:{stopwatch.ElapsedMilliseconds} ms";
        }

        /// <summary>
        /// start a new timer
        /// </summary>
        /// <returns></returns>
        public static EasyTimer StartNew()
        {
            return new EasyTimer();
        }

        /// <summary>
        /// set a token  and  start a new timer by token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static EasyTimer SetTimer(object token)
        {
            return token is null
                ? throw new ArgumentNullException(nameof(token))
                : timerMapper.TryGetValue(token, out EasyTimer timer)
                ? throw new ArgumentException($"Token:{token} registered", nameof(token))
                : (timerMapper[token] = new EasyTimer());
        }

        /// <summary>
        /// get used time by token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="removeTokenAfterRead"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public static TimeSpan GetTimeSpan(object token, bool removeTokenAfterRead = true)
        {
            if (token is null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (!timerMapper.TryGetValue(token, out EasyTimer timer))
            {
                throw new NotSupportedException($"Token:{token} not registered ");
            }

            if (removeTokenAfterRead)
            {
                timerMapper.TryRemove(token, out EasyTimer _);
            }

            return timer.GetTimeSpan();
        }

        /// <summary>
        /// get used time by token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="removeTokenAfterRead"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public static double GetTotalMilliseconds(object token, bool removeTokenAfterRead = true)
        {
            if (token is null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (!timerMapper.TryGetValue(token, out EasyTimer timer))
            {
                throw new NotSupportedException($"Token:{token} not registered ");
            }

            timer.Stop();

            if (removeTokenAfterRead)
            {
                timerMapper.TryRemove(token, out EasyTimer _);
            }

            return timer.GetTotalMilliseconds();
        }


        /// <summary>
        /// run an action and return a timer
        /// </summary>
        /// <param name="action">action callback</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EasyTimer Run(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            EasyTimer timer = EasyTimer.StartNew();
            try
            {
                action();
            }
            finally
            {
                timer.Stop();
            }

            return timer;
        }
    }
}