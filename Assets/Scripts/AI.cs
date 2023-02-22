using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private TileManager tileManager;

    private void Start()
    {
        
    }

    static class PlayingField
    {
        public static int[] values;

        public static void MakeMove(int index)
        {
            int value = values[index];
            while (value > 0)
            {
                values[index % 14]++;
                value--;
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
                if (values[i] > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }

    class Node
    {
        public Node[] nodes;
        public int delta;
    }

    class Tree
    {
        public Node Root;

        public void  InitTree()
        {
            Root = new Node();
        }


        
    }
}
