using System;
using System.Threading.Tasks;

namespace AlephVault.Unity.Support
{
    namespace Utils
    {
        /// <summary>
        ///   Several features involving asynchronous processing.
        /// </summary>
        public static class Tasks
        {
            public static async Task Blink()
            {
                await Task.Yield();
                // Task.Delay(TimeSpan.FromMilliseconds(1));
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync(this Func<Task> func, Action<Exception> onError = null)
            {
                foreach(Func<Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f();
                    }
                    else
                    {
                        try
                        {
                            await f();
                        }
                        catch(Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <param name="a1">The 1st argument to pass</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1>(this Func<T1, Task> func, T1 a1, Action<Exception> onError = null)
            {
                foreach (Func<T1, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1);
                    }
                    else
                    {
                        try
                        {
                            await f(a1);
                        }
                        catch(Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2>(this Func<T1, T2, Task> func, T1 a1, T2 a2, Action<Exception> onError = null)
            {
                foreach (Func<T1, T2, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2);
                    }
                    else
                    {
                        try
                        {
                            await f(a1, a2);
                        }
                        catch (Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3>(this Func<T1, T2, T3, Task> func, T1 a1, T2 a2, T3 a3, Action<Exception> onError = null)
            {
                foreach (Func<T1, T2, T3, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3);
                    }
                    else
                    {
                        try
                        {
                            await f(a1, a2, a3);
                        }
                        catch (Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4>(this Func<T1, T2, T3, T4, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, Action<Exception> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4);
                    }
                    else
                    {
                        try
                        {
                            await f(a1, a2, a3, a4);
                        }
                        catch (Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5>(this Func<T1, T2, T3, T4, T5, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, Action<Exception> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5);
                    }
                    else
                    {
                        try
                        {
                            await f(a1, a2, a3, a4, a5);
                        }
                        catch (Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6>(this Func<T1, T2, T3, T4, T5, T6, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, Action<Exception> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6);
                    }
                    else
                    {
                        try
                        {
                            await f(a1, a2, a3, a4, a5, a6);
                        }
                        catch (Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7>(this Func<T1, T2, T3, T4, T5, T6, T7, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, Action<Exception> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7);
                    }
                    else
                    {
                        try
                        {
                            await f(a1, a2, a3, a4, a5, a6, a7);
                        }
                        catch (Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, Action<Exception> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8);
                    }
                    else
                    {
                        try
                        {
                            await f(a1, a2, a3, a4, a5, a6, a7, a8);
                        }
                        catch (Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, Action<Exception> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9);
                    }
                    else
                    {
                        try
                        {
                            await f(a1, a2, a3, a4, a5, a6, a7, a8, a9);
                        }
                        catch (Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, T10 a10, Action<Exception> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10);
                    }
                    else
                    {
                        try
                        {
                            await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10);
                        }
                        catch (Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, T10 a10, T11 a11, Action<Exception> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11);
                    }
                    else
                    {
                        try
                        {
                            await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11);
                        }
                        catch (Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, T10 a10, T11 a11, T12 a12, Action<Exception> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12);
                    }
                    else
                    {
                        try
                        {
                            await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12);
                        }
                        catch (Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, T10 a10, T11 a11, T12 a12, T13 a13, Action<Exception> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13);
                    }
                    else
                    {
                        try
                        {
                            await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13);
                        }
                        catch (Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, T10 a10, T11 a11, T12 a12, T13 a13, T14 a14, Action<Exception> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14);
                    }
                    else
                    {
                        try
                        {
                            await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14);
                        }
                        catch (Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, T10 a10, T11 a11, T12 a12, T13 a13, T14 a14, T15 a15, Action<Exception> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15);
                    }
                    else
                    {
                        try
                        {
                            await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15);
                        }
                        catch (Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, T10 a10, T11 a11, T12 a12, T13 a13, T14 a14, T15 a15, T16 a16, Action<Exception> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16);
                    }
                    else
                    {
                        try
                        {
                            await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16);
                        }
                        catch (Exception e)
                        {
                            onError(e);
                        }
                    }
                }
            }
        }
    }
}