using System;
using UnityEngine;

namespace AlephVault.Unity.Support
{
    namespace Types
    {
        namespace Sampling
        {
            /// <summary>
            ///   Takes two 3D vectors to generate an AABB and
            ///   and allows getting sample vectors out of it,
            ///   uniformly distributed.
            /// </summary>
            public class RandomBox3
            {
                Vector3 min;
                Vector3 max;

                public RandomBox3(Vector3 a, Vector3 b)
                {
                    min = new Vector3(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y), Mathf.Min(a.z, b.z));
                    max = new Vector3(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y), Mathf.Max(a.z, b.z));
                }

                /// <summary>
                ///   Returns a uniformly distributed vector from the box.
                /// </summary>
                /// <returns>The sampled vector</returns>
                public Vector3 Get()
                {
                    return new Vector3(
                        UnityEngine.Random.Range(min.x, max.x),
                        UnityEngine.Random.Range(min.y, max.y),
                        UnityEngine.Random.Range(min.z, max.z)
                    );
                }
            }
        }
    }
}
