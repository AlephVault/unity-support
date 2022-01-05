using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;


namespace AlephVault.Unity.Support
{
    namespace Types
    {
        namespace Async
        {
            /// <summary>
            ///   Adds an awaitable definition to the <see cref="AsyncOperation" />
            ///   subclasses to make it actually asynchronous.
            /// </summary>
            public static class AsyncOperationExtensionMethods
            {
                public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
                {
                    var tcs = new TaskCompletionSource<object>();
                    asyncOp.completed += obj => { tcs.SetResult(null); };
                    return ((Task)tcs.Task).GetAwaiter();
                }
            }
        }
    }
}