using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


public class TileMapVis : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTileMap, wallTileMap, itemTileMap;

    [SerializeField]
    private TileBase floorTile, wallUp, wallRight, wallLeft, wallDown, wallFull, wallDownRight, wallDownLeft, wallUpRight, wallUpLeft, wallInnerDownLeft, wallInnerDownRight, exitLadder, healthPotion, hungerPotion, torch1, torch2;


    public void VisFloorTiles(IEnumerable<Vector2Int> floorPos)
    {
        VisTiles(floorPos, floorTileMap, floorTile);
    }

    public void VisItemTiles(Vector2Int pos, string type)
    {
        TileBase item = null;
        if (type == "health")
        {
            item = healthPotion;
        }
        else if (type == "exit")
        {
            item = exitLadder;
        }
        else if (type == "food")
        {
            item = hungerPotion;
        }
        else if (type == "torch1")
        {
            item = torch1;
        }
        else if (type == "torch2")
        {
            item = torch2;
        }

        if (item != null)
            Debug.Log("adding");
            VisSingleTile(itemTileMap, item, pos);
    }



    internal void PatchBasicWall(Vector2Int pos, string binary)
    {
        int binToInt = Convert.ToInt32(binary, 2);
        TileBase wall = null;

        if (WallGenerator.wallUp.Contains(binToInt))
        {
            wall = wallUp;
        }
        else if (WallGenerator.wallRight.Contains(binToInt))
        {
            wall = wallRight;
        }
        else if (WallGenerator.wallLeft.Contains(binToInt))
        {
            wall = wallLeft;
        }
        else if (WallGenerator.wallDown.Contains(binToInt))
        {
            wall = wallDown;
        }
        else if (WallGenerator.wallFull.Contains(binToInt))
        {
            wall = wallFull;
        }

        if (wall!=null)
            VisSingleTile(wallTileMap, wall, pos);

    }

    internal void PatchCornerWall(Vector2Int pos, string binary)
    {
        int binToInt = Convert.ToInt32(binary, 2);
        TileBase wall = null;

        if (WallGenerator.wallInnerDownLeft.Contains(binToInt))
        {
            wall = wallInnerDownLeft;
        }
        else if (WallGenerator.wallInnerDownRight.Contains(binToInt))
        {
            wall = wallInnerDownRight;
        }
        else if (WallGenerator.wallDownLeft.Contains(binToInt))
        {
            wall = wallDownLeft;
        }
        else if (WallGenerator.wallDownRight.Contains(binToInt))
        {
            wall = wallDownRight;
        }
        else if (WallGenerator.wallUpRight.Contains(binToInt))
        {
            wall = wallUpRight;
        }
        else if (WallGenerator.wallUpLeft.Contains(binToInt))
        {
            wall = wallUpLeft;
        }
        else if (WallGenerator.wallFullEightDirections.Contains(binToInt))
        {
            wall = wallFull;
        }
        else if (WallGenerator.wallBottmEightDirections.Contains(binToInt))
        {
            wall = wallDown;
        }

        if (wall != null)
            VisSingleTile(wallTileMap, wall, pos);


    }

    private void VisTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase tile)
    {
        foreach(var pos in positions)
        {
            VisSingleTile(tileMap, tile, pos);
        }
    }

    private void VisSingleTile(Tilemap tileMap, TileBase tile, Vector2Int pos)
    {
        var tilePos = tileMap.WorldToCell((Vector3Int)pos);
        tileMap.SetTile(tilePos, tile);

    }

    public void Clean()
    {
        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
        itemTileMap.ClearAllTiles();
    }


}
