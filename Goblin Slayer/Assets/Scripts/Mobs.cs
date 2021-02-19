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

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            move = false;
            GetComponent<Animator>().SetBool("isAttacking", true);
            //GlobalConstables.GetGlobalConstables().GetEnemies().Remove(this.gameObject);
            //GlobalConstables.GetGlobalConstables().DecreaseTotalEnemiesBy(1);
            //GetComponent<BoxCollider2D>().enabled = false;
            //Destroy(this.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            move = true;
            GetComponent<Animator>().SetBool("isAttacking", false);
        }
    }

    public void SetTookHitToTrue()
    {
        tookHit = true; 
    }

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
    }

    private async void Update()
    {
        Goto();


        if (tookHit)
        {
            hp -= 25;
            tookHit = false;
        }

        if (hp <= 1)
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