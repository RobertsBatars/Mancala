using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayingField
{
    public static int[] values;
    private static int[] savedValues;

    public static bool doubleMove = false;
    public static int capturedIndex = -1;
    [HideInInspector] public static bool readyToMakeMove = true;
    [HideInInspector] public static bool playerMove = true;

    public static void MakeMove(int n)
    {
        doubleMove = false;
        capturedIndex = -1;
        int index = n;
        index %= 14;
        int value = values[index];
        values[index] = 0;
        index++;
        while (value > 0)
        {
            index %= 14;
            values[index]++;
            index++;
            value--;
        }
        index--;
        if ((index == 0 && n > 7) || (index == 7 && n < 7))
        {
            doubleMove = true;
        }

        if (values[index] == 1)
        {
            if (n < 7 && index < 7 && index != 0)
            {
                values[7] += values[14 - index];
                values[14 - index] = 0;
                capturedIndex = 14 - index;
            }
            if (n > 7 && index > 7)
            {
                values[0] += values[14 - index];
                values[14 - index] = 0;
                capturedIndex = 14 - index;
            }
        }
    }

    public static int Evaluate()
    {
        return values[7] - values[0];
    }

    public static bool CheckEnd()
    {
        for (int i = 1; i < values.Length; i++)
        {
            if (values[i] > 0 && i != 7)
            {
                return false;
            }
        }
        return true;
    }

    public static void SaveState()
    {
        savedValues = values;
    }

    public static void RestoreSavedState()
    {
        values = savedValues;
    }

    public static void Init()
    {
        values = new int[14];
        for (int i = 1; i < 14; i++)
        {
            values[i] = 4;
        }
        values[7] = 0;
    }

    public static void DEBUG_PrintValues(int[] values)
    {
        Debug.Log(" ");
        Debug.Log("Red Pit: ");
        Debug.Log(values[0]);
        Debug.Log(" ");
        for (int i = 1; i < 14; i++)
        {
            if (i == 7)
            {
                Debug.Log(" ");
                Debug.Log("Blue Pit: ");
                Debug.Log(values[7]);
                Debug.Log(" ");
            }
            else
            {
                Debug.Log(values[i]);
            }
        }

        Debug.Log(" ");
    }
}
