using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemContainer : MonoBehaviour
{
    private int gemCount = 4;
    private AI ai;
    public int index = 0;

    private void Start()
    {
        ai = FindObjectOfType<AI>();
    }

    private void OnMouseDown()
    {
        if (gemCount == 0 || !PlayingField.readyToMakeMove)
        {
            return;
        }
        gemCount = 0;

        transform.GetChild(0).GetComponent<GemMover>().MoveTo(index+1, false);

        PlayingField.readyToMakeMove = false;
        ai.MakeMove(index);
    }

    public void AddGem(int n)
    {
        gemCount += n;
    }

    public void Move()
    {
        transform.GetChild(0).GetComponent<GemMover>().MoveTo(index + 1, false);
        ai.MakeMove(index);
    }

    public void MoveAll(int index)
    {
        transform.GetChild(0).GetComponent<GemMover>().MoveTo(index, true);
    }
}
