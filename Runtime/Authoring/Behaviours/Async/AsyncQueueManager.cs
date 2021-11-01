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

                // Runs the entire queue on each frame.
                private void Start()
                {
                    RunQueue();
                }

                // Runs the entire queue until the object is destroyed.
                // On each try, all the tasks will be executed. If one
                // task is not -at least- executed, a dummy "Blink"
                // operation will run.
                private async void RunQueue()
                {
                    bool ranLoop;
                    while (this != null && gameObject != null)
                    {
                        ranLoop = false;
                        while (tasks.TryDequeue(out Func<Task> task))
                        {
                            ranLoop = true;
                            await task();
                        }
                        if (!ranLoop) await Tasks.Blink();
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
                public Task<T> Queue<T>(Func<Task<T>> task)
                {
                    if (task == null || !this) return null;
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
                public Task Queue(Func<Task> task)
                {
                    if (task == null || !this) return null;
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
                public Task Queue(Action action)
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
                    return Queue(task);
                }
            }
        }
    }
}