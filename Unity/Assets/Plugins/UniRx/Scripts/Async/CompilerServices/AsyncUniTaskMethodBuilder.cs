﻿#if CSHARP_7_OR_LATER

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace UniRx.Async.CompilerServices
{
    public struct AsyncUniTaskMethodBuilder
    {
        Promise<AsyncUnit> promise;
        Action moveNext;

        // 1. Static Create method.
        [DebuggerHidden]
        public static AsyncUniTaskMethodBuilder Create()
        {
            var builder = new AsyncUniTaskMethodBuilder();
            return builder;
        }

        // 2. TaskLike Task property.
        [DebuggerHidden]
        public UniTask Task
        {
            get
            {
                if (promise != null)
                {
                    return new UniTask(promise);
                }

                if (moveNext == null)
                {
                    return UniTask.CompletedTask;
                }
                else
                {
                    promise = new Promise<AsyncUnit>();
                    return new UniTask(promise);
                }
            }
        }

        // 3. SetException
        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            if (promise == null)
            {
                promise = new Promise<AsyncUnit>();
            }
            promise.SetException(exception);
        }

        // 4. SetResult
        [DebuggerHidden]
        public void SetResult()
        {
            if (moveNext == null)
            {
            }
            else
            {
                if (promise == null)
                {
                    promise = new Promise<AsyncUnit>();
                }
                promise.SetResult(default(AsyncUnit));
            }
        }

        // 5. AwaitOnCompleted
        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (moveNext == null)
            {
                if (promise == null)
                {
                    promise = new Promise<AsyncUnit>(); // built future.
                }

                var runner = new MoveNextRunner();
                moveNext = runner.Run;

                var boxed = (IAsyncStateMachine)stateMachine;
                runner.StateMachine = boxed; // boxed
            }

            awaiter.OnCompleted(moveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        [DebuggerHidden]
        [SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (moveNext == null)
            {
                if (promise == null)
                {
                    promise = new Promise<AsyncUnit>(); // built future.
                }

                var runner = new MoveNextRunner();
                moveNext = runner.Run;

                var boxed = (IAsyncStateMachine)stateMachine;
                runner.StateMachine = boxed; // boxed
            }

            awaiter.UnsafeOnCompleted(moveNext);
        }

        // 7. Start
        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        // 8. SetStateMachine
        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }

        class MoveNextRunner
        {
            public IAsyncStateMachine StateMachine;

            public void Run()
            {
                StateMachine.MoveNext();
            }
        }
    }


    public struct AsyncUniTaskMethodBuilder<T>
    {
        T result;
        Promise<T> promise;
        Action moveNext;

        // 1. Static Create method.
        [DebuggerHidden]
        public static AsyncUniTaskMethodBuilder<T> Create()
        {
            var builder = new AsyncUniTaskMethodBuilder<T>();
            return builder;
        }

        // 2. TaskLike Task property.
        [DebuggerHidden]
        public UniTask<T> Task
        {
            get
            {
                if (promise != null)
                {
                    return new UniTask<T>(promise);
                }

                if (moveNext == null)
                {
                    return new UniTask<T>(result);
                }
                else
                {
                    promise = new Promise<T>();
                    return new UniTask<T>(promise);
                }
            }
        }

        // 3. SetException
        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            if (promise == null)
            {
                promise = new Promise<T>();
            }
            promise.SetException(exception);
        }

        // 4. SetResult
        [DebuggerHidden]
        public void SetResult(T result)
        {
            if (moveNext == null)
            {
                this.result = result;
            }
            else
            {
                if (promise == null)
                {
                    promise = new Promise<T>();
                }
                promise.SetResult(result);
            }
        }

        // 5. AwaitOnCompleted
        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (moveNext == null)
            {
                if (promise == null)
                {
                    promise = new Promise<T>(); // built future.
                }

                var runner = new MoveNextRunner();
                moveNext = runner.Run;

                var boxed = (IAsyncStateMachine)stateMachine;
                runner.StateMachine = boxed; // boxed
            }

            awaiter.OnCompleted(moveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        [DebuggerHidden]
        [SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (moveNext == null)
            {
                if (promise == null)
                {
                    promise = new Promise<T>(); // built future.
                }

                var runner = new MoveNextRunner();
                moveNext = runner.Run;

                var boxed = (IAsyncStateMachine)stateMachine;
                runner.StateMachine = boxed; // boxed
            }

            awaiter.UnsafeOnCompleted(moveNext);
        }

        // 7. Start
        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        // 8. SetStateMachine
        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }

        class MoveNextRunner
        {
            public IAsyncStateMachine StateMachine;

            public void Run()
            {
                StateMachine.MoveNext();
            }
        }
    }
}

#endif