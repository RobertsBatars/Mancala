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
    private AI ai;
    private bool moveAll = false;

    private float distanceUntilReached;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = FindObjectOfType<TileManager>().GemMovementSpeed;
        origin = transform.position;
        tileManager = FindObjectOfType<TileManager>();
        distanceUntilReached = tileManager.distanceUntilReached;
        ai = FindObjectOfType<AI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasReachedEnd)
        {
            if ((target.position - transform.position).magnitude <= distanceUntilReached)
            {
                HasReachedTile();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!hasReachedEnd)
        {
            transform.Translate((target.position - transform.position).normalized * movementSpeed * Time.fixedDeltaTime);
        }
    }

    public void MoveTo(int index, bool all)
    {
        target = tileManager.GetTileTranformFromIndex(index);
        hasReachedEnd = false;
        moveIndex = index;
        moveAll = all;
    }

    private void HasReachedTile()
    {
        if (moveAll)
        {
            hasReachedEnd = true;
            target.GetComponent<GemContainer>().AddGem(transform.childCount);
            while (transform.childCount > 0)
            {
                Transform child = transform.GetChild(0);
                child.parent = target.GetChild(0);
                child.position = child.parent.position;
            }
            transform.position = origin;
            tileManager.SpreadGems();
            return;
        }
        target.GetComponent<GemContainer>().AddGem(1);
        Transform escapee = transform.GetChild(0);
        escapee.parent = target.GetChild(0);
        escapee.position = escapee.parent.position;
        if (transform.childCount > 0)
        {
            MoveTo(moveIndex + 1, false);
        }
        else
        {
            hasReachedEnd = true;
            if (PlayingField.capturedIndex != -1)
            {
                if (PlayingField.capturedIndex > 7)
                {
                    tileManager.tiles[PlayingField.capturedIndex].GetComponent<GemContainer>().MoveAll(7);

                }
                else
                {
                    tileManager.tiles[PlayingField.capturedIndex].GetComponent<GemContainer>().MoveAll(0);
                }
            }
            if (PlayingField.playerMove == PlayingField.doubleMove)
            {
                ai.GenerateTreeForward(PlayingField.values);
                PlayingField.readyToMakeMove = true;
                PlayingField.playerMove = true;
            }
            else
            {
                PlayingField.playerMove = false;
                tileManager.tiles[ai.BestMove()].GetComponent<GemContainer>().Move();
            }
            transform.position = origin;
            tileManager.SpreadGems();
        }
    }
}
