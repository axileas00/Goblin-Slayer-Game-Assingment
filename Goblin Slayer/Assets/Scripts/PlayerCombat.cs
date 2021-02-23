using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public int maxHealth = 100;
    public float attackRange = 0.5f;
    public int currentHealth = 100;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public bool tookDmg = false;
    public Transform attackPoint;
    public Animator animator;
    public HealthBar healthBar;
    public CharacterController2D playerMovementRef;
    public LayerMask EnemyLayers;
    bool x = true;
    [SerializeField]GameObject endGamePanel;
    [SerializeField]Text text;
    int c;
    string[] messagges = new string[] { "YOU LOSE!", "Maybe next time.", "Thats rough buddy...", "What are you gonna do?", "WASTED" };
    void Start()
    {
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        c = Random.Range(0, messagges.Length);
    }
    async void Update()
    {
        //Player attackRate restriction
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0) && x)
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
        if (tookDmg)
        {
            TakeDamage(10);
            tookDmg = false;
        }

        if(currentHealth < 1)
        {
            x = false;
            GetComponent<CharacterController2D>().able = false;
            animator.SetBool("IsDead", true);
            await Task.Delay(2000);
            endGamePanel.SetActive(true);
            animator.enabled = false;
            text.text = messagges[c];
            GlobalConstables.GetGlobalConstables().SetSpawnedRoomsTo(0);
            GlobalConstables.GetGlobalConstables().SetTotalEnemiesTo(0);
            GlobalConstables.GetGlobalConstables().GetEnemies().Clear();
            
            Time.timeScale = 0;
        }
    }

    void DoDmg()//on animation event
    {
        if(GlobalConstables.GetGlobalConstables().GetEnemies().Count > 0)
        {
            foreach (GameObject t in GlobalConstables.GetGlobalConstables().GetEnemies())
            {
                if (Vector2.Distance(t.transform.position, transform.position) < 2)
                {
                    t.GetComponent<Mobs>().tookHit = true;
                }
                else
                {
                    Debug.Log("Empty strike");
                }
            }
        }
        else
        {
            Debug.Log("Empty strike");
        }
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
    }//enables animator

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
