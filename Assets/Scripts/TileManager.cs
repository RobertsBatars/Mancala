using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<Transform> tiles;
    public float GemMovementSpeed = 1;

    [SerializeField] private float gemRadius = 10;
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private GameObject gemGroupPrefab;
    public float distanceUntilReached = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGems();
        SpreadGems();
    }

    public Transform GetTileTranformFromIndex(int index)
    {
        index = index % 14;

        return tiles[index];
    }

    void GenerateGems()
    {
        GameObject gem;
        GameObject gemGroup;
        for (int i = 1; i < tiles.Count; i++)
        {
            if (i == 7) { continue; }
            gemGroup = Instantiate(gemGroupPrefab, tiles[i]);
            for (int j = 0; j < 4; j++)
            {
                gem = Instantiate(gemPrefab, gemGroup.transform);
                gem.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); ;
            }
        }
    }

    public void SpreadGems()
    {
        float angle;
        foreach (Transform tile in tiles)
        {
            angle = 360f / tile.GetChild(0).childCount / 2;

            foreach (Transform gem in tile.GetChild(0))
            {
                gem.position =  tile.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * gemRadius, Mathf.Sin(Mathf.Deg2Rad * angle) * gemRadius, transform.position.z);

                angle += 360f / tile.GetChild(0).childCount;
                
            }
        }
    }
}
