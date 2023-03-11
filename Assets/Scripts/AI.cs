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
            tree = new Tree();
            PlayingField.Init();
            tree.InitTree();
            tree.GenerateTree(depth);
            PlayingField.Init();
            currentNode = tree.root;
            Debug.Log(tree.root.bestIndex);
        }
    }

    private void Start()
    {
        
    }

    public void MakeMove(int index)
    {
        index--;
        if (index >= 7)
        {
            index -= 7;
        }
        currentNode = currentNode.nodes[index];
    }

    public int BestMove()
    {
        return currentNode.bestIndex;
    }
}
