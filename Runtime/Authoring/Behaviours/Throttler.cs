using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AlephVault.Unity.Support
{
    namespace Authoring
    {
        namespace Behaviours
        {
            /// <summary>
            ///   This behaviour allows the object to throttle function executions. This is done
            ///     by invoking <see cref="Throttled(Action)"/>, which will execute the given
            ///     function but disallow further calls of <see cref="Throttled(Action)"/> until
            ///     the time specified in <see cref="lapse"/> passes. 
            /// </summary>
            public class Throttler : MonoBehaviour
            {
                /// <summary>
                ///   <![CDATA[
                ///     Time that must pass after the last call to <see cref="Throttled(Action)"/>
                ///       before another call is allowed. If < 0, this value will be forced to 1.
                ///       This value is expressed in seconds.
                ///   ]]>
                /// </summary>
                public float Lapse;

                void Awake()
                {
                    if (Lapse <= 0) Lapse = 1f;
                }

                /// <summary>
                ///   Tells whether the current throttler is locked or not (this is: the time
                ///     before allowing the next call has not yet passed).
                /// </summary>
                public bool Locked { get; private set; }

                private async void Unlock()
                {
                    float currentTime = 0;
                    while (currentTime < Lapse)
                    {
                        await Utils.Tasks.Blink();
                        currentTime += Time.unscaledDeltaTime;
                    }
                    Locked = false;
                }

                /// <summary>
                ///   Executes a given function in a throttled fasion. This is: this method
                ///     will fail silently if the time after the last call to it was less than
                ///     the value expressed in <see cref="lapse"/>.
                /// </summary>
                /// <param name="action">The function to execute. Usually, an anonymous one.</param>
                public void Throttled(Action action)
                {
                    if (Locked) return;

                    Locked = true;
                    try
                    {
                        action();
                        Unlock();
                    }
                    catch (Exception)
                    {
                        Locked = false;
                        throw;
                    }
                }
            }
        }
    }
}
