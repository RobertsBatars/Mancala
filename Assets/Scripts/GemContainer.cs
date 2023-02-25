using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemContainer : MonoBehaviour
{
    [HideInInspector] public int gemCount = 4;
    private AI ai;
    public int index = 0;

    private void Start()
    {
        ai = FindObjectOfType<AI>();
    }

    private void OnMouseDown()
    {
        gemCount = 0;

        transform.GetChild(0).GetComponent<GemMover>().MoveTo(index+1);

        ai.MakeMove(index);
        Debug.Log(ai.BestMove());
    }

    public void AddGem()
    {
        gemCount++;
    }
}
