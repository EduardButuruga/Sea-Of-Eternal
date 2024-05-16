using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    private float damage;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Bullet hit enemy with damage: " + this.damage); // Debug message
            maxHealth enemyHealth = collision.GetComponent<maxHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(this.damage);
            }
            Destroy(gameObject);
        }
    }
}