﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    [SerializeField] private GameObject[] SpawnPoints;

    [SerializeField] private int NumberOfEnemies;

    private GameObject enemyToAdd;

    public bool enable = false;

    private int x;
    
    private async void Update()
    {
        if (enable == true)
        {
            NumberOfEnemies = Random.Range(5, 10);
            GlobalConstables.totalEnemies = NumberOfEnemies + 1;
            enable = false;

            for (int i = 0; i <= NumberOfEnemies; i++)
            {
                x = Random.Range(0, SpawnPoints.Length);
                enemyToAdd = Instantiate(enemy, SpawnPoints[x].transform.position, Quaternion.identity);
                GlobalConstables.enemies.Add(enemyToAdd);
                await Task.Delay(500);
            }// spawn mechanism
        }//bool to enable spawners
        enable = false;// disacle the spawner once more
    }
}