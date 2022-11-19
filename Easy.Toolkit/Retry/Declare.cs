using System;
using System.Collections.Generic;

namespace Easy.Toolkit
{
    internal class ExceptionDeclare
    {
        private readonly Dictionary<Type, Func<Exception, bool>> declaresMapper = new Dictionary<Type, Func<Exception, bool>>();

        public void Declare<TException>(Func<TException, bool> checker = null) where TException : Exception
        {
            declaresMapper[typeof(TException)] = new Func<Exception, bool>((e) =>
            {
                if (checker is null)
                {
                    return true;
                }

                if (e is not TException exception)
                {
                    return false;
                }
                return checker.Invoke(exception);
            });
        }

        public void Declare(Type exceptionType, Func<Exception, bool> checker = null)
        {
            declaresMapper[exceptionType] = new Func<Exception, bool>((e) =>
            {
                if (checker is null)
                {
                    return true;
                }

                return checker.Invoke(e);
            });
        }


        public bool ShouldRetryAgain(Exception exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            Type type = exception.GetType();

            if (declaresMapper.TryGetValue(type, out Func<Exception, bool> checker) == false)
            {
                return false;
            }

            return checker(exception);
        }

    }



    internal class ResultDeclare<TResult>
    {
        private readonly List<Func<TResult, bool>> declaresMapper = new List<Func<TResult, bool>>();

        public void Declare(Func<TResult, bool> checker)
        {
            if (checker is null)
            {
                return;
            }
            declaresMapper.Add(checker);
        }



        public bool ShouldRetryAgain(TResult result)
        {
            foreach (Func<TResult, bool> checker in declaresMapper)
            {
                if (checker.Invoke(result))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
