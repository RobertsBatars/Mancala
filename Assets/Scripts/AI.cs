using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private TileManager tileManager;
    public int depth = 1;
    private Tree tree;
    private Node currentNode;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            InitGame();
        }
    }

    private void InitGame()
    {
        tree = new Tree();
        PlayingField.Init();
        tree.InitTree();
        tree.GenerateTree(depth, PlayingField.values, tree.root);
        PlayingField.Init();
        currentNode = tree.root;
    }

    private void Start()
    {
        InitGame();
    }

    public void GenerateTreeForward(int[] values)
    {
        int[] valuescp = (int[])values.Clone();
        bool doubleMove = PlayingField.doubleMove;
        tree.GenerateTree(depth, PlayingField.values, currentNode);
        PlayingField.values = valuescp;
        PlayingField.doubleMove = doubleMove;
    }

    public void MakeMove(int index)
    {
        PlayingField.MakeMove(index);
        index--;
        if (index >= 7)
        {
            index -= 7;
        }
        currentNode = currentNode.nodes[index];
        PlayingField.DEBUG_PrintValues(PlayingField.values);
    }

    public int BestMove()
    {
        return currentNode.bestIndex;
    }
}
