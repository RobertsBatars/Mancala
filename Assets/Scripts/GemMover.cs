using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMover : MonoBehaviour
{
    private float movementSpeed;
    private Transform target;
    private bool hasReachedEnd = true;
    private Vector3 origin;
    private TileManager tileManager;
    private int moveIndex;

    private float distanceUntilReached = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = FindObjectOfType<TileManager>().GemMovementSpeed;
        origin = transform.position;
        tileManager = FindObjectOfType<TileManager>();
        distanceUntilReached = tileManager.distanceUntilReached;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasReachedEnd)
        {
            transform.Translate((target.position - transform.position).normalized * movementSpeed * Time.deltaTime);

            if ((target.position - transform.position).magnitude <= distanceUntilReached)
            {
                HasReachedTile();
            }
        }
    }

    public void MoveTo(int index)
    {
        target = tileManager.GetTileTranformFromIndex(index);
        hasReachedEnd = false;
        moveIndex = index;
    }

    private void HasReachedTile()
    {
        target.GetComponent<GemContainer>().AddGem();
        Transform escapee = transform.GetChild(0);
        escapee.parent = target.GetChild(0);
        escapee.position = escapee.parent.position;
        if (transform.childCount > 0)
        {
            MoveTo(moveIndex+1);
        }
        else
        {
            hasReachedEnd = true;
            transform.position = origin;
            tileManager.SpreadGems();
        }
    }
}
