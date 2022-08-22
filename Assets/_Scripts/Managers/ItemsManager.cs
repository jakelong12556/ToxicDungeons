using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemsManager : MonoBehaviour
{
    public Transform cam;


    [SerializeField]
    protected GameObject health;

    [SerializeField]
    protected GameObject food;


    [SerializeField]
    protected GameObject exit;

    [SerializeField]
    protected GameObject torch1;


    [SerializeField]
    protected GameObject torch2;

    public int normalItem = 7;

    public int powerUpAmount = 2;

    public static ItemsManager manager;

    // Start is called before the first frame update
    void Awake()
    {
        manager = this;
    }

    public void GenerateExit(Vector2Int exitPos)
    {
        Instantiate(exit, new Vector2(exitPos.x + 0.5f, exitPos.y + 0.5f), Quaternion.identity);
    }


    public void GenerateItems(HashSet<Vector2Int> floorPos, TileMapVis visualizer)
    {
        var maxItems = normalItem + powerUpAmount;
        for (int i = 0; i < maxItems; i++)
        {
            var currentTile = new Vector2Int(0, 0);

            //Add space around player
            while (true)
            {
                var floorTileIDX = Random.Range(0, floorPos.Count());

                currentTile = floorPos.ElementAt(floorTileIDX);


                float distanceFromPlayer = Vector2.Distance(new Vector2(0, 0), currentTile);

                if (distanceFromPlayer > 2.0f)
                {
                    break;
                }
            }

            if ((maxItems - i) < powerUpAmount)
            {
                if (Random.Range(0.0f, 1.0f) < 0.3)
                {
                    Instantiate(health, new Vector2(currentTile.x + 0.5f, currentTile.y + 0.5f), Quaternion.identity);
                    //visualizer.VisItemTiles(currentTile, "health");
                }
                else if (Random.Range(0.0f, 1.0f) < 0.7)
                {
                    Instantiate(food, new Vector2(currentTile.x + 0.5f, currentTile.y + 0.5f), Quaternion.identity);

                }
            }
            else
            {
                if (Random.Range(0.0f, 1.0f) < 0.5)
                {
                    Instantiate(torch1, new Vector2(currentTile.x + 0.5f, currentTile.y + 0.5f), Quaternion.identity);
                }
                else
                {
                    Instantiate(torch2, new Vector2(currentTile.x + 0.5f, currentTile.y + 0.5f), Quaternion.identity);
                }
            }


        }


    }
}
