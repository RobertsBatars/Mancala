using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private TileManager tileManager;
    public int depth = 1;
    private Tree tree;
    private Node currentNode;

    private void Start()
    {
        tree = new Tree();
        PlayingField.Init();
        tree.InitTree();
        tree.GenerateTree(depth);
        PlayingField.Init();
        currentNode = tree.root;
        Debug.Log(tree.root.bestIndex);
    }

    public void MakeMove(int index)
    {
        if (index > 7)
        {
            index -= 8;
        }
        currentNode = currentNode.nodes[index];
    }

    public int BestMove()
    {
        return currentNode.bestIndex;
    }
}
