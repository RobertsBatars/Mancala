using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree
{
    public Node root;

    public void InitTree()
    {
        root = new Node();
    }

    public void GenerateTree(int depth, int[] values, Node rootNode)
    {
        float a, b;
        a = -Mathf.Infinity;
        b = Mathf.Infinity;
        int[] valuesNext = new int[14];
        System.Array.Copy(values, valuesNext, 14);
        rootNode.delta = Minimax(rootNode, depth, a, b, true, valuesNext);
    }

    public float Minimax(Node node, int depth, float a, float b, bool isMaximizing, int[] values)
    {
        float currentDelta;
        bool doubleMove;
        int bestIndex = -100;
        PlayingField.values = values;
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
                if (values[i+1] == 0) continue;
                node.nodes[i] = new Node();

                PlayingField.values = (int[])values.Clone();
                PlayingField.MakeMove(i+1);
                doubleMove = PlayingField.doubleMove;

                int[] valuesNext = new int[14];
                System.Array.Copy(PlayingField.values, valuesNext, 14);

                node.nodes[i].delta = Minimax(node.nodes[i], depth - 1, a, b, doubleMove, valuesNext);

                if (node.nodes[i].delta > currentDelta)
                {
                    currentDelta = node.nodes[i].delta;
                    bestIndex = i + 1;
                }
                if (currentDelta > b)
                {
                    break;
                }
                a = Mathf.Max(a, currentDelta);
            }
            node.bestIndex = bestIndex;
            return currentDelta;
        }
        else
        {
            currentDelta = Mathf.Infinity;
            for (int i = 0; i < 6; i++)
            {
                if (values[i+8] == 0) continue;
                node.nodes[i] = new Node();

                PlayingField.values = (int[])values.Clone();
                PlayingField.MakeMove(i + 8);
                doubleMove = PlayingField.doubleMove;

                int[] valuesNext = new int[14];
                System.Array.Copy(PlayingField.values, valuesNext, 14);

                node.nodes[i].delta = Minimax(node.nodes[i], depth - 1, a, b, !doubleMove, valuesNext);

                if (node.nodes[i].delta < currentDelta)
                {
                    currentDelta = node.nodes[i].delta;
                    bestIndex = i + 8;
                }
                if (currentDelta < a)
                {
                    break;
                }
                b = Mathf.Min(b, currentDelta);
            }
            node.bestIndex = bestIndex;
            return currentDelta;
        }
    }
}
