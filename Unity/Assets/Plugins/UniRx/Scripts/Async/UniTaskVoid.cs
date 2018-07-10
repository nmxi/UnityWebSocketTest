﻿#if CSHARP_7_OR_LATER
#pragma warning disable CS1591

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UniRx.Async.CompilerServices;

namespace UniRx.Async
{
    [AsyncMethodBuilder(typeof(AsyncUniTaskVoidMethodBuilder))]
    public struct UniTaskVoid
    {
        public void Forget()
        {
        }

        [DebuggerHidden]
        public Awaiter GetAwaiter()
        {
            return new Awaiter();
        }

        public struct Awaiter : ICriticalNotifyCompletion
        {
            [DebuggerHidden]
            public bool IsCompleted => true;

            [DebuggerHidden]
            public void GetResult()
            {
                UnityEngine.Debug.LogWarning("UniTask can't await, always fire-and-forget. use Forget instead of await.");
            }

            [DebuggerHidden]
            public void OnCompleted(Action continuation)
            {
            }

            [DebuggerHidden]
            public void UnsafeOnCompleted(Action continuation)
            {
            }
        }
    }
}

#endif