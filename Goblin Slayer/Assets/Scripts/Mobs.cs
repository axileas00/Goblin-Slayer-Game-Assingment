using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mobs : MonoBehaviour
{
    [SerializeField] int c = 1;
    [SerializeField] GameObject target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GlobalConstables.GetGlobalConstables().GetEnemies().Remove(this.gameObject);
            GlobalConstables.GetGlobalConstables().DecreaseTotalEnemiesBy(1);
            Destroy(this.gameObject);
        }
    }//change this

    void Goto()
    {
        if((transform.position - target.transform.position) != Vector3.zero)
        {
            if(transform.position.x < target.transform.position.x)
            {
                transform.position += Vector3.right * c * Time.deltaTime; 
            }//right movement
            else if(transform.position.x > target.transform.position.x)
            {
                transform.position += Vector3.left * c * Time.deltaTime; 
            }//left movement
        }
    }// method so the enemies can track the player and follow him

    private void Update()
    {
        Goto();
    }
}