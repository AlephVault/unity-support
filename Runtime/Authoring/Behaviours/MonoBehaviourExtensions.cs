using System;
using System.Collections;
using UnityEngine;


namespace AlephVault.Unity.Support
{
    namespace Authoring
    {
        namespace Behaviours
        {
            /// <summary>
            ///   Several extensions for all <see cref="MonoBehaviour"/>.
            /// </summary>
            public static class MonoBehaviourExtensions
            {
                /// <summary>
                ///   Invokes ANY function (i.e. a parameterless callback)
                ///   after a given time. If the time is negative, the call
                ///   will be synchronous.
                /// </summary>
                /// <param name="mb">The sender behaviour</param>
                /// <param name="f">The callback to execute</param>
                /// <param name="delay">The delay for the callback to be executed</param>
                public static void Invoke(this MonoBehaviour mb, Action f, float delay)
                {
                    if (delay < 0) { f(); } else {
                        mb.StartCoroutine(InvokeRoutine(f, delay));
                    }
                }

                private static IEnumerator InvokeRoutine(Action f, float delay)
                {
                    yield return new WaitForSeconds(delay);
                    f();
                }
            }
        }
    }
}