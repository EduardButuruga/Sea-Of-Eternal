using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float life = 3;
    private float damage;
    public PlayerHealth playerHealth; // Referință la componenta PlayerHealth a jucătorului

    private void Awake()
    {    
            // Dacă referința nu este setată în Inspector, încearcă să o găsești automat
            if (playerHealth == null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    playerHealth = player.GetComponent<PlayerHealth>();
                }
            }       
        Destroy(gameObject, life);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {          
            if (playerHealth != null)
            {
                playerHealth.TakeDamage((int)this.damage, transform.position);
            }
            Destroy(gameObject);
        }
    }
}