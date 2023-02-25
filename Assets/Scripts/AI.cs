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
}
