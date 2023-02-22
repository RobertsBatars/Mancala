using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemContainer : MonoBehaviour
{
    [HideInInspector] public int gemCount = 4;
    public int index = 0;

    private void OnMouseDown()
    {
        gemCount = 0;

        transform.GetChild(0).GetComponent<GemMover>().MoveTo(index+1);
    }

    public void AddGem()
    {
        gemCount++;
    }
}
