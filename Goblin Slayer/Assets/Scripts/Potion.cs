using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField]GameObject manager;
    private void Start()
    {
        manager = GameObject.Find("Health bar");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            manager.GetComponentInChildren<HealthBar>().SetHealth(collision.gameObject.GetComponent<PlayerCombat>().maxHealth);
            collision.gameObject.GetComponent<PlayerCombat>().currentHealth = collision.gameObject.GetComponent<PlayerCombat>().maxHealth;
            gameObject.SetActive(false);
        }
    }
}
