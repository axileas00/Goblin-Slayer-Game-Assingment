using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public Animator animator;
    public LayerMask EnemyLayers;
    public CharacterController2D playerMovementRef;
    

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(20);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
    void start()
    {
        currentHealth = maxHealth;
        StartCoroutine("DisableScript");
    }

    IEnumerator DisableScript()
    {
        playerMovementRef.enabled = false;
        yield return new WaitForSeconds(3f);
        playerMovementRef.enabled = true;
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
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
