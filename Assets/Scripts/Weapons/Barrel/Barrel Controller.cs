using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    public float stopTime = 2f; // Timpul după care butoiul se oprește
    public float explosionRadius = 2f; // Raza exploziei
    public LayerMask destroyableLayer; // Layer-ul obiectelor ce pot fi distruse

    public AudioSource audioSource; // Componenta AudioSource
    public AudioClip shootSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(StopAfterTime());

        // Declanșează animația de splash când butoiul este aruncat
        animator.SetTrigger("Splash");

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Declanșează animația de plutire dacă butoiul s-a oprit
        if (rb.velocity.magnitude < 0.1f && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Float"))
        {
            animator.SetTrigger("Float");
        }
    }

    private IEnumerator StopAfterTime()
    {
        yield return new WaitForSeconds(stopTime);
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || ((1 << collision.gameObject.layer) & destroyableLayer) != 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        // Declanșează animația de explozie
        animator.SetTrigger("Explode");

        // Adaugă logica pentru explozie (efecte vizuale, sunete, damage)
        Collider2D[] objectsToDestroy = Physics2D.OverlapCircleAll(transform.position, explosionRadius, destroyableLayer);

        foreach (var obj in objectsToDestroy)
        {
            Destroy(obj.gameObject); // Distruge obiectele din raza exploziei
        }

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        Destroy(gameObject, 0.7f); // Distruge butoiul după explozie după un mic delay pentru a permite animația să ruleze
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}