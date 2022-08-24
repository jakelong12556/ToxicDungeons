using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AgentParams
{
    public int maxEnemies;
    public int powerUpAmount;
    public int corridorLen;
    public int corridorCount;
}
public class AgentGenerator : MonoBehaviour
{

    [SerializeField]
    private Player player;

    public Transform cam;



    [SerializeField]
    protected GameObject enemy1;

    [SerializeField]
    protected GameObject enemy2;


    [SerializeField]
    protected GameObject enemy3;

    protected int minEnemies = 2;

    public int maxEnemies = 3;


    [Range(0.0f, 1.0f)]
    protected float enemy1Probability = 0.5f;


    [Range(0.0f, 1.0f)]
    protected float enemy2Probability = 0.5f;


    [Range(0.0f, 1.0f)]
    protected float enemy3Probability = 0.4f;

    public int totalEnemies = 0;


    public static AgentGenerator manager;
    public Player currentPlayer;

    public HashSet<Vector2Int> floor;
    


    void Awake()
    {
        manager = this;
    }

    internal void GeneratePlayer(Vector2 roomCenter, bool newGame)
    {
        if (newGame)
        {
            currentPlayer = Instantiate(player, new Vector2(roomCenter.x + 0.5f, roomCenter.y + 0.5f), Quaternion.identity);
        } else
        {
            currentPlayer.transform.position = new Vector2(roomCenter.x + 0.5f, roomCenter.y + 0.5f);
        }
    }

    internal void GenerateEnemies(Vector2 roomCenter, HashSet<Vector2Int> floorPos)
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            var currentTile = new Vector2Int(0,0);

            //Add space around player
            while (true)
            {
                var floorTileIDX = Random.Range(0, floorPos.Count());

                currentTile = floorPos.ElementAt(floorTileIDX);


                float distanceFromPlayer = Vector2.Distance(roomCenter, currentTile);

                if (distanceFromPlayer > 4.0f)
                {
                    break;
                }
            }



            if (Random.Range(0.0f, 1.0f) < enemy1Probability)
            {
                Instantiate(enemy1, new Vector2(currentTile.x + 0.5f, currentTile.y + 0.5f), Quaternion.identity);
            }
            else if(Random.Range(0.0f, 1.0f) < enemy2Probability)
            {
                Instantiate(enemy2, new Vector2(currentTile.x + 0.5f, currentTile.y + 0.5f), Quaternion.identity);

            }
            else
            {
                Instantiate(enemy3, new Vector2(currentTile.x + 0.5f, currentTile.y + 0.5f), Quaternion.identity);
            }
            totalEnemies += 1;

        }
    }

    public int getTotalEnemies()
    {
        return totalEnemies;
    }

    //Takes in dur, amount and intensity to create a camera shake.
    public void Shake(float duration, float amount, float intensity)
    {
        StartCoroutine(ShakeCam(duration, amount, intensity));
    }

    //Shakes the camera over time.
    IEnumerator ShakeCam(float dur, float amount, float intensity)
    {
        float t = dur;
        Vector3 originalPos = cam.position;
        Vector3 targetPos = Vector3.zero;

        while (t > 0.0f)
        {
            if (targetPos == Vector3.zero)
            {
                targetPos = Random.insideUnitCircle * amount;
                targetPos = new Vector3(targetPos.x, targetPos.y, -10);
            }

            cam.position = Vector3.Lerp(cam.position, targetPos, intensity * Time.deltaTime);

            if (Vector3.Distance(cam.position, targetPos) < 0.02f)
            {
                targetPos = Vector3.zero;
            }

            t -= Time.deltaTime;
            yield return null;
        }

        cam.position = originalPos;
    }







}
