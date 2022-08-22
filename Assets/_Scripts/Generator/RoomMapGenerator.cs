using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomMapGenerator : MapGenerator
{
    [SerializeField]
    private int minW = 4, minH = 4;

    [SerializeField]
    private int dungeonW = 20, dungeonH = 20;

    [SerializeField]
    [Range(0,10)]
    private int offset = 1;

    [SerializeField]
    private bool randomRooms = false;




    protected override void RunPG(bool newGame, DungeonConfig mapParams, AgentParams agentParams)
    {
        agents.totalEnemies = 0;
        if (agentParams != null)
        {
            agents.maxEnemies = agentParams.maxEnemies;
            items.powerUpAmount = agentParams.powerUpAmount;
        }

        if (mapParams != null)
        {
            mapParameters = mapParams;
        }
        CreateRooms(newGame);
        Debug.Log(agents.getTotalEnemies());
    }

    private void CreateRooms(bool newGame)
    {
        var roomsList = PGA.RoomPartioning(new BoundsInt((Vector3Int)startPos, new Vector3Int(dungeonW, dungeonH, 0)), minW, minH);

        var randomRoom = Random.Range(0, roomsList.Count);
        var roomCenter = new Vector2(Mathf.RoundToInt(roomsList[randomRoom].center.x), Mathf.RoundToInt(roomsList[randomRoom].center.y));

        agents.GeneratePlayer(roomCenter, newGame);



        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomRooms)
        {
            floor = CreateRandomRooms(roomsList);
        } else
        {
            floor = CreateSimpleRooms(roomsList);
        }

        agents.floor = floor;


        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach(var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> paths = ConnectRooms(roomCenters);
        floor.UnionWith(paths);

        visualizer.VisFloorTiles(floor);
        WallGenerator.GenerateWall(floor, visualizer);
    }

    private HashSet<Vector2Int> CreateRandomRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for(int i = 0; i< roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var tempRoom = new HashSet<Vector2Int>();
            var roomFloor = RandomWalk(mapParameters, roomCenter, false);

            foreach (var pos in roomFloor)
            {
                if(pos.x >= (roomBounds.xMin + offset) && pos.x <= (roomBounds.xMax - offset) && pos.y >= (roomBounds.yMin - offset) && pos.y <= (roomBounds.yMax + offset))
                {
                    floor.Add(pos);
                    tempRoom.Add(pos);
                }
            }

            agents.GenerateEnemies(new Vector2(startPos.x, startPos.y), tempRoom);
            items.GenerateItems(tempRoom, visualizer);


        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> paths = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[UnityEngine.Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0 )
        {
            Vector2Int closest = GetClosestPointToCenter(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newPath = CreatePath(currentRoomCenter, closest);
            currentRoomCenter = closest;
            paths.UnionWith(newPath);
        }
        return paths;
    }

    private HashSet<Vector2Int> CreatePath(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        var pos = currentRoomCenter;
        path.Add(pos);
        while(pos.y != destination.y)
        {
            if(destination.y > pos.y)
            {
                pos += Vector2Int.up;
            } else
            {
                pos += Vector2Int.down;
            }
            path.Add(pos);
        }
        while (pos.x != destination.x)
        {
            if (destination.x > pos.x)
            {
                pos += Vector2Int.right;
            }
            else
            {
                pos += Vector2Int.left;
            }
            path.Add(pos);
        }

        return path;
    }

    private Vector2Int GetClosestPointToCenter(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float dist = float.MaxValue;
        foreach (var pos in roomCenters)
        {
            float currentDistance = Vector2.Distance(pos, currentRoomCenter);
            if(currentDistance < dist)
            {
                dist = currentDistance;
                closest = pos;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();


        foreach (var room in roomsList)
        {

            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int pos = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(pos);

                }
            }
            agents.GenerateEnemies(new Vector2(startPos.x, startPos.y), floor);

        }
        return floor;
    }
}
