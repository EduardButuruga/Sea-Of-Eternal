using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private Animator animator;
    private Vector2 currentDirection;

    void Start()
    {
        animator = GetComponent<Animator>();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    void Update()
    {     
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;       
        currentDirection = directionToPlayer;      
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + currentDirection, speed * Time.deltaTime);      
        UpdateAnimator(currentDirection);
    }

    void UpdateAnimator(Vector2 direction)
    {             
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.magnitude);
    }
}