using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class LargeNumberStringExtensions
{
    enum  Suffix
    { 
        K, M, B, T,
        AA, AB, AC, AD, AE, AF, AG, AH, AI, AJ, AK, AL, AN, AM, AO, AP, AQ, AR, AS, AT, AU, AV, AW, AX, AY, AZ,
        BA, BB, BC, BD, BE, BF, BG, BH, BI, BJ, BK, BL, BN, BM, BO, BP, BQ, BR, BS, BT, BU, BV, BW, BX, BY, BZ,
        CA, CB, CC, CD, CE, CF, CG, CH, CI, CJ, CK, CL, CN, CM, CO, CP, CQ, CR, CS, CT, CU, CV, CW, CX, CY, CZ,
        DA, DB, DC, DD, DE, DF, DG, DH, DI, DJ, DK, DL, DN, DM, DO, DP, DQ, DR, DS, DT, DU
    }

    public static string DoubleToLargeNumberString(this double value)
    {
        if (value == double.NaN)
        {
            return value.ToString();
        }

        int i = -1;

        while (value > 1000.0)
        {
            value /= 1000.0;
            ++i;
        }

        StringBuilder sb = new StringBuilder();

        // 3자리로 제한함
        if (value >= 100.0)
        {
            sb.AppendFormat("{0:#.}", value);
        }
        else if (value >= 10.0)
        {
            sb.AppendFormat("{0:#.#}", value);
        }
        else
        {
            sb.AppendFormat("{0:#.##}", value);
        }

        // 단위가 필요한 경우 덧붙임
        if (i >= 0)
        {
            sb.Append((Suffix)i);
        }

        return sb.ToString();
    }
    public static double LargeNumberStringToDouble(this string str)
    {
        var array = str.ToCharArray();

        int pos = -1;
        for (int i = 1; i < array.Length; ++i)
        {
            char c = array[i];
            if ('A' <= c && 'Z' >= c)
            {
                pos = i;

                break;
            }
        }

        double value;

        if (0 < pos)
        {
            string digitStr = str.Substring(0, pos);
            string letterStr = str.Substring(pos);
            Suffix suffix = (Suffix)Enum.Parse(typeof(Suffix), letterStr);

            value = double.Parse(digitStr) * Math.Pow(1000.0, (int)(suffix + 1));
        }
        else
        {
            value = double.Parse(str);
        }

        return value;
    }
}
