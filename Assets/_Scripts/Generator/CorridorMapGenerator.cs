using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorMapGenerator : MapGenerator
{
    [SerializeField]
    private int corridorLen = 20, corridorCount = 3;
    [Range(0.1f,1)]
    private float roomPercentage = 1f;

    private Dictionary<Vector2Int, HashSet<Vector2Int>> roomDict = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
    private HashSet<Vector2Int> floorPos, corridorPos;


    public void setParams(AgentParams agentParams)
    {
        corridorLen = agentParams.corridorLen;
        corridorCount = agentParams.corridorCount;
    }

    protected override void RunPG(bool newGame, DungeonConfig mapParams, AgentParams agentParams)
    {
        agents.totalEnemies = 0;

        if (agentParams != null)
        {
            agents.maxEnemies = agentParams.maxEnemies;
            items.powerUpAmount = agentParams.powerUpAmount;
            setParams(agentParams);
        }

        if (mapParams != null)
        {
            mapParameters = mapParams;
        }

        HashSet<Vector2Int> floorPosS = new HashSet<Vector2Int>();
        HashSet<Vector2Int> roomPosPotential = new HashSet<Vector2Int>();

        CreateCorridor(floorPosS, roomPosPotential);

        HashSet<Vector2Int> roomPosS = CreateRooms(roomPosPotential);

        List<Vector2Int> deadEnds = FindDeadEnds(floorPosS);

        CreateDeadEndsRooms(deadEnds, roomPosS);


        agents.GeneratePlayer(startPos, newGame);


        floorPosS.UnionWith(roomPosS);

        agents.floor = floorPosS;

        visualizer.VisFloorTiles(floorPosS);
        WallGenerator.GenerateWall(floorPosS, visualizer);

        Debug.Log(agents.getTotalEnemies());

    }

    private void CreateDeadEndsRooms(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPosS)
    {
        foreach(var pos in deadEnds)
        {
            if(roomPosS.Contains(pos) == false)
            {
                var room = RandomWalk(mapParameters, pos, true);
                roomPosS.UnionWith(room); 
            }
        }
    }

    private List<Vector2Int> FindDeadEnds(HashSet<Vector2Int> floorPosS)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        
        foreach(var pos in floorPosS)
        {
            int neighbourCounts = 0;
            foreach(var direction in PGA.directionList)
            {
                if (floorPosS.Contains(pos + direction))
                {
                    neighbourCounts++;
                }
            }
            if(neighbourCounts == 1)
            {
                deadEnds.Add(pos);
            }
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> roomPosPotential)
    {
        HashSet<Vector2Int> roomPosS = new HashSet<Vector2Int>();
        int roomCount = Mathf.RoundToInt(roomPosPotential.Count * roomPercentage);

        List<Vector2Int> rooms = roomPosPotential.OrderBy(x => Guid.NewGuid()).Take(roomCount).ToList();
        roomDict.Clear();
        foreach(var roomPos in rooms)
        {
            var roomFloor = RandomWalk(mapParameters, roomPos, true);
            roomPosS.UnionWith(roomFloor);
            roomDict[roomPos] = roomFloor;
        }

        return roomPosS;


    }

    private void CreateCorridor(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> roomPosPotential)
    {
        var currentPos = startPos;
        roomPosPotential.Add(currentPos);

        for (int i = 0; i < corridorCount; i++)
        {
            var path = PGA.RandomWalkCorridor(currentPos, corridorLen);
            currentPos = path[path.Count - 1];
            roomPosPotential.Add(currentPos);
            floorPos.UnionWith(path);
        }
        corridorPos = new HashSet<Vector2Int>(floorPos);
    }
}
