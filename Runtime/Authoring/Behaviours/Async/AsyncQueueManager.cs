using AlephVault.Unity.Support.Utils;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UnityEngine;


namespace AlephVault.Unity.Support
{
    namespace Authoring
    {
        namespace Behaviours
        {
            /// <summary>
            ///   Maintains an asynchronous queue manager process. This
            ///   process is triggered each time a task is queued and
            ///   the process is not already running. Otherwise, the
            ///   task will be queued into the already running process.
            /// </summary>
            public class AsyncQueueManager : MonoBehaviour
            {
                // This is the list of complex tasks that are pending
                // to be executed. These tasks wrap complex things that
                // must be waited for someone else, instead of using
                // a mutex for it (those are things not necessarily
                // related to the main Unity thread).
                private ConcurrentQueue<Func<Task>> tasks = new ConcurrentQueue<Func<Task>>();

                /// <summary>
                ///   The delay to start the async queue.
                /// </summary>
                [SerializeField]
                private float invokeDelay = 0;

                // Tells whether it is running or not.
                private bool running = false;

                // Runs the entire queue.
                private async void RunQueue()
                {
                    if (!running && gameObject != null)
                    {
                        running = true;
                        while (!tasks.TryDequeue(out Func<Task> task))
                        {
                            await task();
                        }
                        running = false;
                    }
                }

                /// <summary>
                ///   Queues a task-returning function to be executed
                ///   in the queue processing. The returned result is
                ///   another task that must be waited for. This new
                ///   task is resolved when the function executes in
                ///   its turn in the execution queue. This version
                ///   of the function deals with function that return
                ///   typed tasks.
                /// </summary>
                /// <param name="task">The function to queue for execution</param>
                /// <returns>A typed task to be waited for, or null if either the task function is null or the current object is destroyed</returns>
                public Task<T> QueueTask<T>(Func<Task<T>> task)
                {
                    if (task == null || gameObject == null) return null;
                    TaskCompletionSource<T> source = new TaskCompletionSource<T>();
                    tasks.Enqueue(async () => {
                        try
                        {
                            source.SetResult(await task());
                        }
                        catch (Exception e)
                        {
                            source.SetException(e);
                        }
                    });
                    Invoke("RunQueue", invokeDelay);
                    return source.Task;
                }

                /// <summary>
                ///   Queues a task-returning function to be executed
                ///   in the queue processing. The returned result is
                ///   another task that must be waited for. This new
                ///   task is resolved when the function executes in
                ///   its turn in the execution queue.
                /// </summary>
                /// <param name="task">The function to queue for execution</param>
                /// <returns>A task to be waited for, or null if either the task function is null or the current object is destroyed</returns>
                public Task QueueTask(Func<Task> task)
                {
                    if (task == null || gameObject == null) return null;
                    TaskCompletionSource<bool> source = new TaskCompletionSource<bool>();
                    tasks.Enqueue(async () => {
                        try
                        {
                            await task();
                            source.SetResult(true);
                        }
                        catch (Exception e)
                        {
                            source.SetException(e);
                        }
                    });
                    Invoke("RunQueue", invokeDelay);
                    return source.Task;
                }

                /// <summary>
                ///   Queues a standard function to be executed in the
                ///   queue processing. The returned result is a task
                ///   that must be waited for. This task is resolved
                ///   when the function executes in its turn in the
                ///   execution queue.
                /// </summary>
                /// <param name="action">The function to queue for execution</param>
                /// <returns>A task to be waited for, or null if either the function is null or the current object is destroyed</returns>
                public Task QueueAction(Action action)
                {
                    Func<Task> task = null;
                    if (action != null)
                    {
                        task = () =>
                        {
                            action();
                            return Task.CompletedTask;
                        };
                    }
                    return QueueTask(task);
                }
            }
        }
    }
}