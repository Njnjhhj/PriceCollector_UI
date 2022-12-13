using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace PriceCollector.Core.Utils
{
    public class MethodUtils
    {
        public static T WaitForElement<T>(Func<T> method, TimeSpan wait, params Type[] ignoredExceptionTypes)
        {
            var timer = Stopwatch.StartNew();

            while (timer.Elapsed <= wait)
            {
                var result = TryGetResult(method, ignoredExceptionTypes);
                if (result == null || result.Equals(default(T)))
                    continue;
                return result;
            }

            return default;
        }

        public static bool IsIgnoredException(IEnumerable<Type> ignoredExceptions, Exception exception)
        {
            return ignoredExceptions.Any(type => type.IsInstanceOfType(exception));
        }

        public static void Wait(TimeSpan timeSpan)
        {
            Thread.Sleep(timeSpan);
        }

        private static T TryGetResult<T>(Func<T> method, params Type[] ignoredExceptionTypes)
        {
            try
            {
                var element = method();

                if (element == null)
                    return default;

                if (element is IEnumerable == false)
                    return element;

                if ((element as IEnumerable).GetEnumerator().MoveNext())
                    return element;

                return default;
            }
            catch (Exception e)
            {
                if (!IsIgnoredException(ignoredExceptionTypes, e))
                    throw;

                Console.WriteLine(@$"+++TryGetResult. Ignored exception of type {e.GetType()}: {e.Message}");
                return default;
            }
        }
    }
}
