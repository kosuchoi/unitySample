using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using System;

namespace BurstCompilerSample
{
    public struct CopyJob
    {
        public int[] Input;
        public int[] Output;
        public int repeatCount;

        public void Execute()
        {
            for (int j = 0; j < repeatCount; ++j)
            {
                for (int i = 0; i < Input.Length; ++i)
                {
                    Output[i] = Input[i];
                }
            }
        }
    }

    public struct CopyJob2
    {
        public int[] Input;
        public int[] Output;
        public int repeatCount;

        public void Execute()
        {
            for (int j = 0; j < repeatCount; ++j)
            {
                Buffer.BlockCopy(Input, 0, Output, 0, Input.Length);
            }
        }
    }

    [BurstCompile]
    public struct NativeCopyJob : IJob
    {
        public NativeArray<int> Input;
        public NativeArray<int> Output;
        public int repeatCount;

        public unsafe void Execute()
        {
            int* inputPtrBegin = (int*)NativeArrayUnsafeUtility.GetUnsafePtr(Input);
            int* outputPtrBegin = (int*)NativeArrayUnsafeUtility.GetUnsafePtr(Output);

            for (int j = 0; j < repeatCount; ++j)
            {
                int* inputPtr = inputPtrBegin;
                int* outputPtr = outputPtrBegin;

                for (int i = 0; i < Input.Length; ++i)
                {
                    *(outputPtr++) = *(inputPtr++);
                }
            }
        }
    }

    [BurstCompile]
    public struct NativeCopyJob2 : IJob
    {
        public NativeArray<int> Input;
        public NativeArray<int> Output;
        public int repeatCount;

        public unsafe void Execute()
        {
            int* inputPtrBegin = (int*)NativeArrayUnsafeUtility.GetUnsafePtr(Input);
            int* outputPtrBegin = (int*)NativeArrayUnsafeUtility.GetUnsafePtr(Output);

            for (int j = 0; j < repeatCount; ++j)
            {
                Buffer.MemoryCopy(inputPtrBegin, outputPtrBegin, Input.Length, sizeof(int));
            }
        }
    }    
}