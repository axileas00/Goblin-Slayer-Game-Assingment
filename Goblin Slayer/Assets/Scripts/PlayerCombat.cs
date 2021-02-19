using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class PlayerCombat : MonoBehaviour
{
    public int maxHealth = 100;
    public float attackRange = 0.5f;
    public int currentHealth;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    public Transform attackPoint;
    public Animator animator;
    public HealthBar healthBar;
    public CharacterController2D playerMovementRef;
    public LayerMask EnemyLayers;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    /*async*/ void Update()
    {
        //Player attackRate restriction
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetMouseButton(0))
            {
                GetComponent<CharacterController2D>().enabled = false;
            }
            else
            {
                GetComponent<CharacterController2D>().enabled = true;
            }
        }

        //inflict damage on the healthbar
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }

        if(currentHealth < 1)
        {
            animator.SetBool("IsDead", true);
            //await Task.Delay(3000);
           // GetComponent<CharacterController2D>().enabled = false;
            //GetComponent<PlayerCombat>().enabled = false;
            
        }
    }

    void DoDmg()
    {

    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
    void Attack()
    {
        //Play the attack animation
        animator.SetTrigger("IsAttacking");

        //Detect enemies within range of the attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, EnemyLayers);

        //Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit " + enemy.name);
            //enemy.GetComponent<>
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
