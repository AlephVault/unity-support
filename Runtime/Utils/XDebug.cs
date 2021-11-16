using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AlephVault.Unity.Support
{
    namespace Utils
    {
        /// <summary>
        ///   A custom log wrapper to use <see cref="Debug"/> with some
        ///   prefixes and helpers.
        /// </summary>
        public class XDebug
        {
            // A friendly short name of the package I am debugging.
            private string packagePrefix;

            // The object whose method I am debugging right now.
            private object target;

            // The current call.
            private string currentCall;

            /// <summary>
            ///   Logs a debug info start message.
            /// </summary>
            public void Start()
            {
                Debug.Log($"{packagePrefix}.{target?.GetType()?.Name ?? "null"}::{currentCall}::Begin");
            }

            /// <summary>
            ///   Logs a debug info end message.
            /// </summary>
            public void End()
            {
                Debug.Log($"{packagePrefix}.{target?.GetType()?.Name ?? "null"}::{currentCall}::End");
            }

            /// <summary>
            ///   Logs a debug info message.
            /// </summary>
            public void Info(string message)
            {
                Debug.Log($"{packagePrefix}.{target?.GetType()?.Name ?? "null"}::{currentCall}::>>> {message}");
            }

            /// <summary>
            ///   Logs a debug warning message.
            /// </summary>
            public void Warning(string message)
            {
                Debug.LogWarning($"{packagePrefix}.{target?.GetType()?.Name ?? "null"}::{currentCall}::>>> {message}");
            }

            /// <summary>
            ///   Logs a debug error message.
            /// </summary>
            public void Error(string message)
            {
                Debug.LogError($"{packagePrefix}.{target?.GetType()?.Name ?? "null"}::{currentCall}::>>> {message}");
            }

            /// <summary>
            ///   Logs an exception and a marking debug error message.
            /// </summary>
            public void Exception(Exception e)
            {
                Debug.LogException(e);
                Error("An exception has been triggered");
            }
        }
    }
}