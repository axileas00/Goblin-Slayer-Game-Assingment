using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalConstables : MonoBehaviour
{
    public static int spawnedRooms, totalEnemies;
    public static List<GameObject> enemies = new List<GameObject>();
    public static Queue<GameObject> Rooms = new Queue<GameObject>();
    public static int Highscore = 0;
}
