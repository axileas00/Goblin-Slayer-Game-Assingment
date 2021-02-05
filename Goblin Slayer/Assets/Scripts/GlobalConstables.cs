using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalConstables
{
    private static int spawnedRooms, totalEnemies;//constants that keep track of gameobjects on scene that might need manipulation autonimously somewhere else
    private static List<GameObject> enemies = new List<GameObject>();//list of enemies in the rooms
    private static Queue<GameObject> Rooms = new Queue<GameObject>();//list of total rooms
    private static int Highscore = 0;//highscore

    private static GlobalConstables inst = null;

    private GlobalConstables() { }

    public static GlobalConstables GetGlobalConstables()
    {
        if (inst == null)
        {
            inst = new GlobalConstables();
        }

        return inst;
    }

    public void SetSpawnedRoomsTo(int i)
    {
        spawnedRooms = i;
    }

    public void DecreaseSpawnedRoomsBy(int i)
    {
        spawnedRooms -= i;
    }

    public void IncreaseSpawnedRoomsBy(int i)
    {
        spawnedRooms += i;
    }

    public int GetSpawnedRooms()
    {
        return spawnedRooms;
    }

    public Queue<GameObject> GetRooms()
    {
        return Rooms;
    }

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }

    public int GetHighscore()
    {
        return Highscore;
    }

    public void IncreaseHighscoreBy(int i)
    {
        Highscore += i;
    }

    public void SetHighscoreTo(int i)
    {
        Highscore = i;
    }

    public int GetTotalEnemies()
    {
        return totalEnemies;
    }

    public void InsreaseTotalEnemiesBy(int i)
    {
        totalEnemies += i;
    }

    public void DecreaseTotalEnemiesBy(int i)
    {
        totalEnemies = totalEnemies - i;
    }

    public void SetTotalEnemiesTo(int i)
    {
        totalEnemies = i;
    }
}
