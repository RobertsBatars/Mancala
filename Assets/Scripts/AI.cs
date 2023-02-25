using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private TileManager tileManager;
    private Tree tree;

    private void Start()
    {
        tree = new Tree();
        PlayingField.Init();
        tree.InitTree();
        tree.GenerateTree(3);
        Debug.Log(tree.root.delta);
    }

    static class PlayingField
    {
        public static int[] values;
        private static int[] savedValues;

        public static void MakeMove(int index)
        {
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
        }

        public static int EvaluateMove(int index)
        {
            //PlayingField.MakeMove(index);
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

    class Node
    {
        public Node[] nodes;
        public float delta;
    }

    class Tree
    {
        public Node root;

        public void InitTree()
        {
            root = new Node();
        }

        public void GenerateTree(int depth)
        {
            float currentDelta, a, b;
            a = -Mathf.Infinity;
            b = Mathf.Infinity;

            currentDelta = -Mathf.Infinity;
            root.nodes = new Node[6];
            for (int i = 0; i < 6; i++)
            {
                root.nodes[i] = new Node();
                int[] values = new int[14];
                PlayingField.Init();
                System.Array.Copy(PlayingField.values, values, 14);

                root.nodes[i].delta = Minimax(root.nodes[i], depth, a, b, true, values, i + 1);
                currentDelta = Mathf.Max(currentDelta, root.nodes[i].delta);
            }
            root.delta = currentDelta;
        }

        public float Minimax(Node node, int depth, float a, float b, bool isMaximizing, int[] values, int index)
        {
            float currentDelta;
            PlayingField.values = values;
            if (depth == 0 || PlayingField.CheckEnd())
            {
                //PlayingField.DEBUG_PrintValues(PlayingField.values);
                //Debug.Log(PlayingField.Evaluate());


                return PlayingField.EvaluateMove(index + 7);
            }
            PlayingField.MakeMove(index);
            values = PlayingField.values;
            node.nodes = new Node[6];

            if (isMaximizing)
            {
                currentDelta = -Mathf.Infinity;
                for (int i = 0; i < 6; i++)
                {
                    node.nodes[i] = new Node();
                    int[] valuesNext = new int[14];
                    System.Array.Copy(values, valuesNext, 14);

                    node.nodes[i].delta = Minimax(node.nodes[i], depth-1, a, b, false, valuesNext, i+8);
                    currentDelta = Mathf.Max(currentDelta, node.nodes[i].delta);
                    if (currentDelta > b)
                    {
                        break;
                    }
                    a = Mathf.Max(a, currentDelta);
                }
                return currentDelta;
            }
            else
            {
                currentDelta = Mathf.Infinity;
                for (int i = 0; i < 6; i++)
                {
                    node.nodes[i] = new Node();
                    int[] valuesNext = new int[14];
                    System.Array.Copy(values, valuesNext, 14);
                    
                    node.nodes[i].delta = Minimax(node.nodes[i], depth - 1, a, b, true, valuesNext, i + 1);
                    currentDelta = Mathf.Min(currentDelta, node.nodes[i].delta);
                    if (currentDelta < a)
                    {
                        break;
                    }
                    b = Mathf.Min(b, currentDelta);
                }
                //PlayingField.DEBUG_PrintValues(values);
                //Debug.Log("dawd:");
                //Debug.Log(currentDelta);
                return currentDelta;
            }
        }
        
    }
}
