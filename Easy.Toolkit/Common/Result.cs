using System;
using System.Diagnostics;

namespace Easy.Toolkit
{
    /// <summary>
    /// result type
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public readonly ref struct Result<TResult> 
    {
        
        /// <summary>
        /// result exception
        /// </summary>
        public readonly Exception Exception;
        /// <summary>
        /// result value
        /// </summary>
        public readonly TResult Value;

        /// <summary>
        /// result state
        /// </summary>
        public readonly bool? IsSuccess = null;

        /// <summary>
        /// create a success <see cref="Result{TResult}"/> by <typeparamref name="TResult"/> value
        /// </summary>
        /// <param name="value"></param>
        public Result(TResult value)
        {
            IsSuccess = true;
            Value = value;
            Exception = null;
        }

        /// <summary>
        /// createa faulted <see cref="Result{TResult}"/> by exception
        /// </summary>
        /// <param name="e"></param>
        public Result(Exception e)
        {
            IsSuccess = false;
            Exception = e;
            Value = default;
        }

        /// <summary>
        /// create a success <see cref="Result{TResult}"/> by <typeparamref name="TResult"/> value
        /// </summary>
        /// <param name="value">result value</param>
        public static implicit operator Result<TResult>(TResult value)
        {
            return new Result<TResult>(value);
        }

        public override string ToString()
        {
            if (IsSuccess == false)
            {
                return Exception?.ToString() ?? "faulted result";
            }

            if (IsSuccess == true)
            {
                return Value?.ToString() ?? "success result";
            }

            return "";
        }

        
        /// <summary>
        /// check result 
        /// <para><see cref="IsSuccess"/>: <c>true</c> and return <see cref="Value"/></para>
        /// <para><see cref="IsFaulted"/>: <c>true</c> and return <paramref name="defaultValue"/></para>
        /// </summary>
        /// <param name="defaultValue">default value</param>
        /// <returns></returns>
        public TResult IfFaulted(TResult defaultValue)
        {
            if (IsSuccess == true)
            {
                return Value;
            }

            return defaultValue;
        }

        /// <summary>
        /// check result 
        /// <para><see cref="IsSuccess"/>: <c>true</c> and return <see cref="Value"/></para>
        /// <para><see cref="IsFaulted"/>: <c>true</c> and invoke <paramref name="invokerFunc"/> callback</para>
        /// </summary>
        /// <param name="invokerFunc">faulted callback</param>
        /// <returns></returns>
        public TResult IfFaulted(Func<Exception, TResult> invokerFunc)
        {
            if (IsSuccess == true)
            {
                return Value;
            }

            return invokerFunc(Exception);
        }

        /// <summary>
        /// check result  
        /// <para><see cref="IsFaulted"/>: <c>true</c> and invoke <paramref name="invokeAction"/> callback</para>
        /// </summary>
        /// <param name="invokeAction">faulted callback</param>
        /// <returns></returns>
        public void IfFaulted(Action<Exception> invokeAction)
        {
            if (IsSuccess == true)
            {
                invokeAction(Exception);
            }

        }

        /// <summary>
        /// check result 
        /// <para><see cref="IsSuccess"/>: <c>true</c> and invoke <paramref name="invokeAction"/> callback</para>
        /// </summary>
        /// <param name="invokeAction">success callback</param>
        /// <returns></returns>
        public void IfSuccess(Action<TResult> invokeAction)
        {
            if (IsSuccess == true)
            {
                invokeAction(Value);
            }

        }

        /// <summary>
        /// check result 
        /// <para><see cref="IsSuccess"/>: <c>true</c> and invoke <paramref name="invokeSuccessAction"/> callback</para>
        /// <para><see cref="IsFaulted"/>: <c>true</c> and invoke <paramref name="invokeFaultedAction"/> callback</para>
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="invokeSuccessAction"></param>
        /// <param name="invokeFaultedAction"></param>
        /// <returns></returns>
        public TReturn Match<TReturn>(Func<TResult, TReturn> invokeSuccessAction, Func<Exception, TReturn> invokeFaultedAction)
        {
            if (!IsSuccess == true)
            {
                return invokeSuccessAction(Value);
            }

            return invokeFaultedAction(Exception);
        }


    }
}