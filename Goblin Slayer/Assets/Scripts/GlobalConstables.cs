using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalConstables : MonoBehaviour
{
    public static int spawnedRooms, totalEnemies;//constants that keep track of gameobjects on scene that might need manipulation autonimously somewhere else
    public static List<GameObject> enemies = new List<GameObject>();//list of enemies in the rooms
    public static Queue<GameObject> Rooms = new Queue<GameObject>();//list of total rooms
    public static int Highscore = 0;//highscore
}
