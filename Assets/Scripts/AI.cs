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
        tree.GenerateTree(2);
        Debug.Log(tree.root.delta);
    }

    static class PlayingField
    {
        public static int[] values;
        private static int[] savedValues;

        public static void MakeMove(int index)
        {
            int value = values[index];
            while (value > 0)
            {
                values[index % 14]++;
                index++;
                value--;
            }
        }

        public static int Evaluate()
        {
            Debug.Log(values[7] - values[0]);
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
                root.nodes[i].delta = Minimax(root.nodes[i], depth-1, a, b, true, values, i + 1);
                currentDelta = Mathf.Max(currentDelta, root.nodes[i].delta);
            }
            root.delta = currentDelta;
        }

        public float Minimax(Node node, int depth, float a, float b, bool isMaximizing, int[] values, int index)
        {
            float currentDelta;
            PlayingField.values = values;
            PlayingField.MakeMove(index);
            if (depth == 0 || PlayingField.CheckEnd())
            {
                return PlayingField.Evaluate();
            }
            node.nodes = new Node[6];
            if (isMaximizing)
            {
                currentDelta = -Mathf.Infinity;
                for (int i = 0; i < 6; i++)
                {
                    node.nodes[i] = new Node();
                    int[] valuesNext = new int[14];
                    System.Array.Copy(PlayingField.values, valuesNext, 14);

                    node.nodes[i].delta = Minimax(node.nodes[i], depth-1, a, b, false, valuesNext, i+1);
                    currentDelta = Mathf.Min(currentDelta, node.nodes[i].delta);
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
                    System.Array.Copy(PlayingField.values, valuesNext, 14);

                    node.nodes[i].delta = Minimax(node.nodes[i], depth - 1, a, b, true, valuesNext, i + 8);
                    currentDelta = Mathf.Min(currentDelta, node.nodes[i].delta);
                    if (currentDelta < a)
                    {
                        break;
                    }
                    b = Mathf.Min(b, currentDelta);
                }
                return currentDelta;
            }
        }
        
    }
}
