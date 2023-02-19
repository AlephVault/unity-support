using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AlephVault.Unity.Support
{
    namespace Utils
    {
        /// <summary>
        ///   Several features involving asynchronous processing.
        /// </summary>
        public static class Tasks
        {
            /// <summary>
            ///   A default function to handle an incoming exception.
            /// </summary>
            /// <param name="e">The exception to handle</param>
            public static async Task DefaultOnError(Exception e)
            {
                Debug.LogException(e);
            }
            
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
            public static async Task InvokeAsync(this Func<Task> func, Func<Exception, Task> onError = null)
            {
                foreach(Func<Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f();
                    }
                    else
                    {
                        await UntilDone(f(), onError);
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
            public static async Task InvokeAsync<T1>(this Func<T1, Task> func, T1 a1, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1);
                    }
                    else
                    {
                        await UntilDone(f(a1), onError);
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2>(this Func<T1, T2, Task> func, T1 a1, T2 a2, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, T2, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2);
                    }
                    else
                    {
                        await UntilDone(f(a1, a2), onError);
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3>(this Func<T1, T2, T3, Task> func, T1 a1, T2 a2, T3 a3, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, T2, T3, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3);
                    }
                    else
                    {
                        await UntilDone(f(a1, a2, a3), onError);
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4>(this Func<T1, T2, T3, T4, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4);
                    }
                    else
                    {
                        await UntilDone(f(a1, a2, a3, a4), onError);
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5>(this Func<T1, T2, T3, T4, T5, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5);
                    }
                    else
                    {
                        await UntilDone(f(a1, a2, a3, a4, a5), onError);
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6>(this Func<T1, T2, T3, T4, T5, T6, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6);
                    }
                    else
                    {
                        await UntilDone(f(a1, a2, a3, a4, a5, a6), onError);
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7>(this Func<T1, T2, T3, T4, T5, T6, T7, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7);
                    }
                    else
                    {
                        await UntilDone(f(a1, a2, a3, a4, a5, a6, a7), onError);
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8);
                    }
                    else
                    {
                        await UntilDone(f(a1, a2, a3, a4, a5, a6, a7, a8), onError);
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9);
                    }
                    else
                    {
                        await UntilDone(f(a1, a2, a3, a4, a5, a6, a7, a8, a9), onError);
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, T10 a10, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10);
                    }
                    else
                    {
                        await UntilDone(f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10), onError);
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, T10 a10, T11 a11, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11);
                    }
                    else
                    {
                        await UntilDone(f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11), onError);
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, T10 a10, T11 a11, T12 a12, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12);
                    }
                    else
                    {
                        await UntilDone(f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12), onError);
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, T10 a10, T11 a11, T12 a12, T13 a13, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13);
                    }
                    else
                    {
                        await UntilDone(f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13), onError);
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, T10 a10, T11 a11, T12 a12, T13 a13, T14 a14, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14);
                    }
                    else
                    {
                        await UntilDone(f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14), onError);
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, T10 a10, T11 a11, T12 a12, T13 a13, T14 a14, T15 a15, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15);
                    }
                    else
                    {
                        await UntilDone(f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15), onError);
                    }
                }
            }

            /// <summary>
            ///   Invokes an asynchronous event, waiting for all of its
            ///   callbacks sequentially. Any error stops the execution.
            /// </summary>
            /// <param name="func">The asynchronous event</param>
            /// <returns>The task, since this call is asynchronous</returns>
            public static async Task InvokeAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task> func, T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9, T10 a10, T11 a11, T12 a12, T13 a13, T14 a14, T15 a15, T16 a16, Func<Exception, Task> onError = null)
            {
                foreach (Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, Task> f in func.GetInvocationList())
                {
                    if (onError == null)
                    {
                        await f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16);
                    }
                    else
                    {
                        await UntilDone(f(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16), onError);
                    }
                }
            }

            /// <summary>
            ///   Waits for a single task. It returns immediately if it is null.
            ///   Any error is handled in an optional callback (if absent, the
            ///   callback "diaperizes" the error).
            /// </summary>
            /// <param name="target">The target task to wait</param>
            /// <param name="onError">What to do on error</param>
            public static async Task UntilDone(Task target, Func<Exception, Task> onError = null)
            {
                onError = onError != null ? onError : (async (e) => {
                    Debug.LogWarning($"UntilDone :: Error while waiting: {e.GetType().FullName} - {e.Message}");
                });

                if (target != null)
                {
                    try
                    {
                        await target;
                    }
                    catch(Exception e)
                    {
                        try
                        {
                            await onError(e);
                        }
                        catch(Exception e2)
                        {
                            Debug.LogError($"UntilDone :: Error while handling an exception ({e.GetType().FullName} - {e.Message}): {e2.GetType().FullName} - {e2.Message}");
                        }
                    }
                }
            }

            /// <summary>
            ///   Waits for many tasks. It skips null tasks and, for each task,
            ///   any error is handled in an optional callback (if absent, the
            ///   callback "diaperizes" the error).
            /// </summary>
            /// <param name="target">The target tasks to wait</param>
            /// <param name="onError">What to do on error</param>
            public static async Task UntilAllDone(IEnumerable<Task> target, Func<Exception, Task> onError = null)
            {
                onError = onError != null ? onError : (async (e) => {
                    Debug.LogWarning($"UntilAllDone :: Error while waiting: {e.GetType().FullName} - {e.Message}");
                });

                foreach(Task task in target)
                {
                    if (task != null)
                    {
                        try
                        {
                            await task;
                        }
                        catch (Exception e)
                        {
                            try
                            {
                                await onError(e);
                            }
                            catch (Exception e2)
                            {
                                Debug.LogError($"UntilAllDone :: Error while handling an exception ({e.GetType().FullName} - {e.Message}): {e2.GetType().FullName} - {e2.Message}");
                            }
                        }
                    }
                }
            }
        }
    }
}