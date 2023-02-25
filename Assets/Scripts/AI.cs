using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private TileManager tileManager;
    public int depth = 1;
    private Tree tree;

    private void Start()
    {
        tree = new Tree();
        PlayingField.Init();
        tree.InitTree();
        tree.GenerateTree(depth);
        Debug.Log(tree.root.delta);
    }
}
