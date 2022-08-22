using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PGA 
{
    public static HashSet<Vector2Int> RandomWalk(Vector2Int startPos, int walkLen)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPos);
        var prevPos = startPos;
        for(int i = 0; i < walkLen; i++){
            var newPos = prevPos + RandomDirection();
            path.Add(newPos);
            prevPos = newPos;
        }
        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLen)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = RandomDirection();
        var currentPos = startPos;
        corridor.Add(currentPos);

        for (int i = 0; i < corridorLen; i++)
        {
            currentPos += direction;
            corridor.Add(currentPos);

        }
        return corridor ;
    }

    public static List<BoundsInt> RoomPartioning(BoundsInt splittingSpace, int minW, int minH)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(splittingSpace);


        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();

            if (room.size.y >= minH && room.size.x >= minW)
            {
                if(Random.value < 0.5f)
                {
                    if (room.size.y >= minH * 2)
                    {
                        HorizontalSplit(minW, roomsQueue, room);
                    }
                    else if (room.size.x >= minW * 2)
                    {
                        VerticalSplit(minH, roomsQueue, room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }
                } else
                {
                    if (room.size.x >= minW * 2)
                    {
                        VerticalSplit(minW, roomsQueue, room);
                    }
                    else if (room.size.y >= minH * 2)
                    {
                        HorizontalSplit(minH, roomsQueue, room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }

        return roomsList;
    }


    private static void VerticalSplit(int minW, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt roomA = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt roomB = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), 
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(roomA);
        roomsQueue.Enqueue(roomB);

    }

    private static void HorizontalSplit(int minH, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt roomA = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt roomB = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int( room.size.x,room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(roomA);
        roomsQueue.Enqueue(roomB);
    }


    // Up, Right, Down, Left
    public static List<Vector2Int> directionList = new List<Vector2Int>{
            new Vector2Int(0,1), //UP
            new Vector2Int(1,0), //RIGHT
            new Vector2Int(0, -1), // DOWN
            new Vector2Int(-1, 0) //LEFT
    };

    // Up-Right, Right-Down, Down-Left, Left-Up
    public static List<Vector2Int> diagList = new List<Vector2Int>{
            new Vector2Int(1,1), //UP-RIGHT
            new Vector2Int(1,-1), //RIGHT-DOWN
            new Vector2Int(-1, -1), // DOWN-LEFT
            new Vector2Int(-1, 1) //LEFT-UP
    };

    // Up, Up-Right, Right, Right-Down, Down, Down-Left, Left, Left-Up
    public static List<Vector2Int> eightWayList = new List<Vector2Int>
    {
            new Vector2Int(0,1), //UP
            new Vector2Int(1,1), //UP-RIGHT
            new Vector2Int(1,0), //RIGHT
            new Vector2Int(1,-1), //RIGHT-DOWN
            new Vector2Int(0, -1), // DOWN
            new Vector2Int(-1, -1), // DOWN-LEFT
            new Vector2Int(-1, 0), //LEFT
            new Vector2Int(-1, 1) //LEFT-UP
    };



    public static Vector2Int RandomDirection(){

        return directionList[Random.Range(0,directionList.Count)];
    }



}
