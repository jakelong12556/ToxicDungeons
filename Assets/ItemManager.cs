using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemManager : MonoBehaviour
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

    public static ItemManager manager;

    // Start is called before the first frame update
    void Awake()
    {
        manager = this;
    }


    public void GenerateItems(HashSet<Vector2Int> floorPos, TileMapVis visualizer)
    {
        var maxItems = 7;
        for (int i = 0; i < maxItems; i++)
        {
            var currentTile = new Vector2Int(0, 0);

            //Add space around player
            while (true)
            {
                var floorTileIDX = Random.Range(0, floorPos.Count());

                currentTile = floorPos.ElementAt(floorTileIDX);


                float distanceFromPlayer = Vector2.Distance(new Vector2(0, 0), currentTile);

                if (distanceFromPlayer > 4.0f)
                {
                    break;
                }
            }

            if ((maxItems - i) < 2)
            {
                if (Random.Range(0.0f, 1.0f) < 0.3)
                {
                    visualizer.VisItemTiles(currentTile, "health");
                }
                else if (Random.Range(0.0f, 1.0f) < 0.7)
                {
                    visualizer.VisItemTiles(currentTile, "food");
                }
            }
            else
            {
                if (Random.Range(0.0f, 1.0f) < 0.5)
                {
                    visualizer.VisItemTiles(currentTile, "torch1");
                }
                else
                {
                    visualizer.VisItemTiles(currentTile, "torch2");
                }
            }


        }


    }
}
