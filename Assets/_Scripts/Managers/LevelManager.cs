using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using Random = UnityEngine.Random;


public class LevelManager : MonoBehaviour
{
    public Text textManager;

    public CorridorMapGenerator corridorMapGenerator;
    public MapGenerator mapGenerator;
    public RoomMapGenerator roomMapGenerator;

    private int currentLevel;
    public static LevelManager levelManager;
    //public AgentGenerator agentManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = this;
        currentLevel = 1;
        StartCoroutine(Notification("Level " + currentLevel, 2f, LevelSelector, null));
    }

    public void NextLevel()
    {
        currentLevel += 1;
        if(currentLevel == 7)
        {
            StartCoroutine(Notification("Game Complete", 5f, null, ReturnToMenu));
        }
        else
        {
            StartCoroutine(Notification("Level " + currentLevel, 2f, LevelSelector, null));
        }
    }

    public void GameEnd()
    {
        StartCoroutine(Notification("Game Over", 3f, null, ReturnToMenu));
    }

    

    public void GenerateExit()
    {
        var floor = AgentGenerator.manager.floor;
        var pos = AgentGenerator.manager.currentPlayer.transform.position;

        var posX = Mathf.RoundToInt(pos.x);
        var posY = Mathf.RoundToInt(pos.y);

        var maxDist = 10;


        List<Vector2Int> tiles = AgentGenerator.manager.floor.ToList();

        var exit = new Vector2Int(posX, posY);



        List<Vector2Int> test = new List<Vector2Int>();

        while (true)
        {
            var potExitX = Random.Range(-maxDist, maxDist);
            var potExitY = Random.Range(-maxDist, maxDist);
            exit = new Vector2Int(posX + potExitX, posY + potExitY);
            if (tiles.Contains(exit))
            {
                float distanceFromPlayer = Vector2.Distance(exit, new Vector2Int(posX,posY));

                if (distanceFromPlayer > 4.0f)
                {
                    ItemsManager.manager.GenerateExit(exit);
                    break;
                }
            }
        }

    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    void LevelSelector()
    {
        var newSetting = new AgentParams();


        var newMap = new DungeonConfig();


        switch (currentLevel)
        {
            case 1:
                Debug.Log("Generating new level");
                mapGenerator.GenerateMapDungeonGame(true, null, null);
                break;
            case 2:
                newSetting.maxEnemies = 5;
                newSetting.powerUpAmount = 5;

                newMap.iter = 50;
                newMap.walkLen = 50;
                newMap.randomStart = false;
                mapGenerator.GenerateMapDungeonGame(false, newMap, newSetting);
                break;
            case 3:
                newSetting.maxEnemies = 10;
                newSetting.powerUpAmount = 5;

                newMap.iter = 80;
                newMap.walkLen = 80;
                newMap.randomStart = false;
                mapGenerator.GenerateMapDungeonGame(false, newMap, newSetting);
                break;
            case 4:
                newSetting.maxEnemies = 4;
                newSetting.powerUpAmount = 2;
                newSetting.corridorCount = 3;
                newSetting.corridorLen = 20;
                corridorMapGenerator.GenerateMapDungeonGame(false, null, newSetting);
                break;
            case 5:
                newSetting.maxEnemies = 4;
                newSetting.powerUpAmount = 2;
                newSetting.corridorCount = 5;
                newSetting.corridorLen = 25;
                corridorMapGenerator.GenerateMapDungeonGame(false, null, newSetting);
                break;
            case 6:
                newSetting.maxEnemies = 3;
                roomMapGenerator.GenerateMapDungeonGame(false, null, null);
                break;

        }
    }

    public IEnumerator Notification(string level, float length, Action actionBefore, Action actionAfter)
    {
        if (actionBefore != null)
        {
            actionBefore();
        }

        textManager.text = level;
        Time.timeScale = 0f;

        float pauseEndTime = Time.realtimeSinceStartup + length;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return true;
        }

        Time.timeScale = 1f;
        textManager.text = "";

        if (actionAfter != null)
        {
            actionAfter();
        }
    }
}
