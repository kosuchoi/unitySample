using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace BurstCompilerSample
{
    public static class Tests
    {
        static int arraySize = 100000;
        static int repeatCount = 10000;

        [UnityTest]
        public static IEnumerator Test()
        {
            CopyJob copyJob = new CopyJob();

            copyJob.Input = new int[arraySize];
            copyJob.Output = new int[arraySize];
            copyJob.repeatCount = repeatCount;

            for (int i = 0; i < arraySize; ++i)
            {
                copyJob.Input[i] = i;
            }

            copyJob.Execute();

            yield return null;
        }

        [UnityTest]
        public static IEnumerator Test2()
        {
            CopyJob2 copyJob = new CopyJob2();

            copyJob.Input = new int[arraySize];
            copyJob.Output = new int[arraySize];
            copyJob.repeatCount = repeatCount;

            for (int i = 0; i < arraySize; ++i)
            {
                copyJob.Input[i] = i;
            }

            copyJob.Execute();

            yield return null;
        }

        [UnityTest]
        public static IEnumerator NativeTest()
        {
            NativeCopyJob nativeCopyJob = new NativeCopyJob();

            nativeCopyJob.Input = new NativeArray<int>(arraySize, Allocator.Persistent);
            nativeCopyJob.Output = new NativeArray<int>(arraySize, Allocator.Persistent);
            nativeCopyJob.repeatCount = repeatCount;

            for (int i = 0; i < arraySize; ++i)
            {
                nativeCopyJob.Input[i] = i;
            }

            nativeCopyJob.Execute();

            nativeCopyJob.Input.Dispose();
            nativeCopyJob.Output.Dispose();

            yield return null;
        }


        [UnityTest]
        public static IEnumerator NativeTest2()
        {
            NativeCopyJob2 nativeCopyJob2 = new NativeCopyJob2();

            nativeCopyJob2.Input = new NativeArray<int>(arraySize, Allocator.Persistent);
            nativeCopyJob2.Output = new NativeArray<int>(arraySize, Allocator.Persistent);
            nativeCopyJob2.repeatCount = repeatCount;

            for (int i = 0; i < arraySize; ++i)
            {
                nativeCopyJob2.Input[i] = i;
            }

            nativeCopyJob2.Execute();

            nativeCopyJob2.Input.Dispose();
            nativeCopyJob2.Output.Dispose();

            yield return null;
        }
    }
}