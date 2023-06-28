using System.Runtime.CompilerServices;

namespace UCG.siteTRAXLite.Extensions
{
    public static class FuncEx
    {
        #region Method no return result
        public static ConfiguredTaskAwaitable ExcuteAsync(Func<Task> f, bool oncontinueOnCapturedContext = true)
        {
            return f().ConfigureAwait(oncontinueOnCapturedContext);
        }

        public static ConfiguredTaskAwaitable ExcuteAsync<T1>(Func<T1, Task> f, T1 p1, bool oncontinueOnCapturedContext = true)
        {
            return f(p1).ConfigureAwait(oncontinueOnCapturedContext);
        }

        public static ConfiguredTaskAwaitable ExcuteAsync<T1, T2>(Func<T1, T2, Task> f, T1 p1, T2 p2, bool oncontinueOnCapturedContext = true)
        {
            return f(p1, p2).ConfigureAwait(oncontinueOnCapturedContext);
        }

        public static ConfiguredTaskAwaitable ExcuteAsync<T1, T2, T3>(Func<T1, T2, T3, Task> f, T1 p1, T2 p2, T3 p3, bool oncontinueOnCapturedContext = true)
        {
            return f(p1, p2, p3).ConfigureAwait(oncontinueOnCapturedContext);
        }

        public static ConfiguredTaskAwaitable ExcuteAsync<T1, T2, T3, T4>(Func<T1, T2, T3, T4, Task> f, T1 p1, T2 p2, T3 p3, T4 p4, bool oncontinueOnCapturedContext = true)
        {
            return f(p1, p2, p3, p4).ConfigureAwait(oncontinueOnCapturedContext);
        }

        public static ConfiguredTaskAwaitable ExcuteAsync<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, Task> f, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, bool oncontinueOnCapturedContext = true)
        {
            return f(p1, p2, p3, p4, p5).ConfigureAwait(oncontinueOnCapturedContext);
        }

        public static ConfiguredTaskAwaitable ExcuteAsync<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, Task> f, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, bool oncontinueOnCapturedContext = true)
        {
            return f(p1, p2, p3, p4, p5, p6).ConfigureAwait(oncontinueOnCapturedContext);
        }

        public static ConfiguredTaskAwaitable ExcuteAsync<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, Task> f, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, bool oncontinueOnCapturedContext = true)
        {
            return f(p1, p2, p3, p4, p5, p6, p7).ConfigureAwait(oncontinueOnCapturedContext);
        }

        #endregion

        #region Method with return result
        public static ConfiguredTaskAwaitable<TRes> ExcuteAsync<TRes>(Func<Task<TRes>> f, bool oncontinueOnCapturedContext = true)
        {
            return f().ConfigureAwait(oncontinueOnCapturedContext);
        }

        public static ConfiguredTaskAwaitable<TRes> ExcuteAsync<T, TRes>(Func<T, Task<TRes>> f, T param, bool oncontinueOnCapturedContext = true)
        {
            return f(param).ConfigureAwait(oncontinueOnCapturedContext);
        }

        public static ConfiguredTaskAwaitable<TRes> ExcuteAsync<T1, T2, TRes>(Func<T1, T2, Task<TRes>> f, T1 p1, T2 p2, bool oncontinueOnCapturedContext = true)
        {
            return f(p1, p2).ConfigureAwait(oncontinueOnCapturedContext);
        }

        public static ConfiguredTaskAwaitable<TRes> ExcuteAsync<T1, T2, T3, TRes>(Func<T1, T2, T3, Task<TRes>> f, T1 p1, T2 p2, T3 p3, bool oncontinueOnCapturedContext = true)
        {
            return f(p1, p2, p3).ConfigureAwait(oncontinueOnCapturedContext);
        }

        public static ConfiguredTaskAwaitable<TRes> ExcuteAsync<T1, T2, T3, T4, TRes>(Func<T1, T2, T3, T4, Task<TRes>> f, T1 p1, T2 p2, T3 p3, T4 p4, bool oncontinueOnCapturedContext = true)
        {
            return f(p1, p2, p3, p4).ConfigureAwait(oncontinueOnCapturedContext);
        }

        public static ConfiguredTaskAwaitable<TRes> ExcuteAsync<T1, T2, T3, T4, T5, TRes>(Func<T1, T2, T3, T4, T5, Task<TRes>> f, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, bool oncontinueOnCapturedContext = true)
        {
            return f(p1, p2, p3, p4, p5).ConfigureAwait(oncontinueOnCapturedContext);
        }

        public static ConfiguredTaskAwaitable<TRes> ExcuteAsync<T1, T2, T3, T4, T5, T6, TRes>(Func<T1, T2, T3, T4, T5, T6, Task<TRes>> f, T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, bool oncontinueOnCapturedContext = true)
        {
            return f(p1, p2, p3, p4, p5, p6).ConfigureAwait(oncontinueOnCapturedContext);
        }
        #endregion

        #region Function Utilities
        public static void ExecuteWithCondition(Func<bool> condition, Action action)
        {
            if (condition())
            {
                action.Invoke();
            }
        }

        public static void ExecuteWhenNotNull(object o, Action action)
        {
            if (o != null)
                action.Invoke();
        }
        #endregion

        public static void ExecuteFunctionWithCondition(bool condition, Action a)
        {
            if (condition)
                a.Invoke();
        }
    }
}
