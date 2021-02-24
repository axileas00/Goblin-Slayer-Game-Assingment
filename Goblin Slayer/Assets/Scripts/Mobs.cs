using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Mobs : MonoBehaviour
{
    [SerializeField] int c = 1;
    [SerializeField] GameObject target;
    [SerializeField] bool move = true;
    public bool tookHit = false;
    [SerializeField] float hp = 100;
    [SerializeField] float dmg = 25;

    public void SetHpTo(int x)
    {
        hp = x;
    }//setter for max hp

    public void SetDmgTo(int x)
    {
        dmg = x;
    }//Setter for inflicted damage

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        
    }// set the target as the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            move = false;
            GetComponent<Animator>().SetBool("isAttacking", true);            
        }
    }//Switch from moving to attacking when you reach the player

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            move = true;
            GetComponent<Animator>().SetBool("isAttacking", false);
        }
    }//Switch from attacking to moving when you reach the player

    void Goto()
    {
        if (move)
        {
            if ((transform.position - target.transform.position) != Vector3.zero)
            {
                if (transform.position.x < target.transform.position.x)
                {
                    transform.position += Vector3.right * c * Time.deltaTime;
                    GetComponent<SpriteRenderer>().flipX = false;
                }//right movement
                else if (transform.position.x > target.transform.position.x)
                {
                    transform.position += Vector3.left * c * Time.deltaTime;
                    GetComponent<SpriteRenderer>().flipX = true;
                }//left movement
            }
        }
    }// method so the enemies can track the player and follow him

    void Attacking()
    {
        Debug.Log(target.name);
        if (Vector2.Distance(transform.position, target.transform.position) < 2) { target.GetComponent<PlayerCombat>().tookDmg = true; }
    }//method called omn animation for mobs attack. it calls a boolean on the player so he'll get hit

    private async void Update()
    {
        Goto();


        if (tookHit)
        {
            hp -= dmg;
            tookHit = false;
        }

        if (hp < 1)
        {
            move = false;
            GetComponent<Animator>().SetBool("isDead", true);
            GlobalConstables.GetGlobalConstables().GetEnemies().Remove(this.gameObject);
            GlobalConstables.GetGlobalConstables().DecreaseTotalEnemiesBy(1);
            GetComponent<BoxCollider2D>().enabled = false;
            await Task.Delay(2200);
            Destroy(this.gameObject);
        }
    }
}