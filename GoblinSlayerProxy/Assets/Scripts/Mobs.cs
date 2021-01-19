using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobs : MonoBehaviour
{
    [SerializeField] int c;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GlobalConstables.enemies.Remove(this.gameObject);
            GlobalConstables.totalEnemies -= 1;
            Debug.Log(GlobalConstables.totalEnemies);
            //if (GlobalConstables.enemies.Count == 0)
            //{
            //    GlobalConstables.totalEnemies = 0;
            //}
            Destroy(this.gameObject);
        }
    }
}
