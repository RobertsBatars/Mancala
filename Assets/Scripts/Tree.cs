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

    public void GenerateTree(int depth)
    {
        float currentDelta, a, b;
        int bestIndex = 0;
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
            if (root.nodes[i].delta > currentDelta)
            {
                currentDelta = root.nodes[i].delta;
                bestIndex = i + 1;
            }
        }
        root.bestIndex = bestIndex;
        root.delta = currentDelta;
    }

    public float Minimax(Node node, int depth, float a, float b, bool isMaximizing, int[] values, int index)
    {
        float currentDelta;
        int bestIndex = 0;
        PlayingField.values = values;
        if (depth == 0 || PlayingField.CheckEnd())
        {
            //PlayingField.DEBUG_PrintValues(PlayingField.values);
            //Debug.Log(PlayingField.Evaluate());


            return PlayingField.EvaluateMove();
        }
        PlayingField.MakeMove(index);
        bool doubleMove = PlayingField.doubleMove;
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

                node.nodes[i].delta = Minimax(node.nodes[i], depth - 1, a, b, doubleMove, valuesNext, i + 8);
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
                node.nodes[i] = new Node();
                int[] valuesNext = new int[14];
                System.Array.Copy(values, valuesNext, 14);

                node.nodes[i].delta = Minimax(node.nodes[i], depth - 1, a, b, !doubleMove, valuesNext, i + 1);
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
            //PlayingField.DEBUG_PrintValues(values);
            //Debug.Log("dawd:");
            //Debug.Log(currentDelta);
            node.bestIndex = bestIndex;
            return currentDelta;
        }
    }
}