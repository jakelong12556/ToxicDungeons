using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMapGenerator : MonoBehaviour
{
    [SerializeField]
    protected TileMapVis visualizer = null;

    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero;

    public void GenerateMapDungeon()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in players)
        {
            DestroyImmediate(obj);
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in enemies)
        {
            DestroyImmediate(obj);
        }

        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject obj in items)
        {
            DestroyImmediate(obj);
        }

        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        foreach (GameObject obj in powerUps)
        {
            DestroyImmediate(obj);
        }





        visualizer.Clean();
        RunPG(true, null, null);
    }


    public void GenerateMapDungeonGame(bool newGame, DungeonConfig mapParams, AgentParams agentParams)
    {
        if (newGame)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject obj in players)
            {
                Destroy(obj);
            }
        }


        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in enemies)
        {
            Destroy(obj);
        }

        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject obj in items)
        {
            Destroy(obj);
        }

        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        foreach (GameObject obj in powerUps)
        {
            Destroy(obj);
        }

        GameObject[] exit = GameObject.FindGameObjectsWithTag("Finish");
        foreach (GameObject obj in exit)
        {
            Destroy(obj);
        }


        visualizer.Clean();
        RunPG(newGame, mapParams, agentParams);
    }

    protected abstract void RunPG(bool newGame, DungeonConfig mapParams, AgentParams agentParams);
}
