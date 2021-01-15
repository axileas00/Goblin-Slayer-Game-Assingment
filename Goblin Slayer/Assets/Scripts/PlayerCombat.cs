using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public Animator animator;
    public LayerMask EnemyLayers;
    public CharacterController2D playerMovementRef;
    

    void start()
    {
        StartCoroutine("DisableScript");
    }

    IEnumerator DisableScript()
    {
        playerMovementRef.enabled = false;
        yield return new WaitForSeconds(3f);
        playerMovementRef.enabled = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
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
