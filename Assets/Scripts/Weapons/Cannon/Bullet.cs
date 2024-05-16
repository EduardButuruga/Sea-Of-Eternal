using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    private float damage;

    private void Awake()
    {
        Destroy(gameObject,life);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log(this.damage);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
