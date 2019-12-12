using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace LargeNumberString
{
    public static class Tests
    {
        [UnityTest]
        public static IEnumerator Test()
        {
            PrintLargeNumberStringToDouble("123BC");
            PrintLargeNumberStringToDouble("-1.23CC");
            PrintLargeNumberStringToDouble("12.3DD");

            PrintDoubleToLargeNumberString(123456789012345678890.0);
            PrintDoubleToLargeNumberString(double.MinValue);
            PrintDoubleToLargeNumberString(double.MaxValue);

            yield return null;
        }

        static void PrintLargeNumberStringToDouble(string str)
        {
            Debug.LogFormat("LargeNumberStringToDouble {0} -> {1}", str, str.LargeNumberStringToDouble());
        }

        static void PrintDoubleToLargeNumberString(double value)
        {
            Debug.LogFormat("DoubleToLargeNumberString {0} -> {1}", value, value.DoubleToLargeNumberString());
        }
    }
}