using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : AbstractMapGenerator
{
    [SerializeField]
    protected DungeonConfig mapParameters;

    [SerializeField]
    protected AgentGenerator agents;

    [SerializeField]
    protected ItemsManager items;


    protected override void RunPG(bool newGame, DungeonConfig mapParams, AgentParams agentsParams)
    {
        agents.totalEnemies = 0;

        if(agentsParams != null)
        {
            agents.maxEnemies = agentsParams.maxEnemies;
            items.powerUpAmount = agentsParams.powerUpAmount;
        }

        HashSet<Vector2Int> floorPos = RandomWalk(mapParams ? mapParams : mapParameters, startPos, true);
        agents.floor = floorPos;



        agents.GeneratePlayer(new Vector2(startPos.x, startPos.y), newGame);


        visualizer.VisFloorTiles(floorPos);
        WallGenerator.GenerateWall(floorPos, visualizer);
        Debug.Log(agents.getTotalEnemies());

    }

    protected HashSet<Vector2Int> RandomWalk(DungeonConfig param, Vector2Int pos, bool generateExtra)
    {
        var currentPos = pos;
        HashSet <Vector2Int> floorPos = new HashSet<Vector2Int>();



        for (int i = 0; i < param.iter; i++)
        {
            var path = PGA.RandomWalk(currentPos, param.walkLen);
            floorPos.UnionWith(path);

            if (param.randomStart)
            {
                currentPos = floorPos.ElementAt(Random.Range(0,floorPos.Count));
            }

        }

        if (generateExtra)
        {
            agents.GenerateEnemies(new Vector2(startPos.x, startPos.y), floorPos);
            items.GenerateItems(floorPos, visualizer);
        }


        return floorPos;
    }

}
