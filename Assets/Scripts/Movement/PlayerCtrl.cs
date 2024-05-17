using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float rotationSpeed;
    public float maxSpeed;
    public Animator animator;
    public GameObject explosiveBarrelPrefab;
    public Transform barrelSpawnPoint;
    public float barrelLaunchSpeed = 2f;
    public bool areBarrelsUnlocked = false;
    public Transform[] cannonPoints; // Array cu punctele de ancorare ale tunului pentru fiecare sprite
    public CannonController cannonController; // Referință către scriptul CannonController

    private int horizontalParamHash;
    private int verticalParamHash;
    private Rigidbody2D rb;
    private Vector2 currentDirection;
    private Vector2 lastInputDirection;
    private float nextBarrelTime = 0f;
    PlayerStats playerStats;

    public AudioSource audioSource; // Componenta AudioSource
    public AudioClip shootSound; // Sunetul de împușcare

    public bool isInPort = true;

    void Start()
    {
        if (playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }
        rb = GetComponent<Rigidbody2D>();
        currentDirection = Vector2.right;
        rb.drag = 2;
        rb.angularDrag = 2;
        horizontalParamHash = Animator.StringToHash("Horizontal");
        verticalParamHash = Animator.StringToHash("Vertical");
        lastInputDirection = Vector2.right;

        // Activează temporar tunul pentru a obține referința la CannonController
        bool initialCannonActiveState = false;
        if (cannonController == null && cannonPoints.Length > 0 && cannonPoints[0].gameObject != null)
        {
            initialCannonActiveState = cannonPoints[0].gameObject.activeSelf;
            cannonPoints[0].gameObject.SetActive(true);
            cannonController = cannonPoints[0].GetComponentInChildren<CannonController>();
            cannonPoints[0].gameObject.SetActive(initialCannonActiveState);
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (isInPort) return;

        float speedX = Input.GetAxis("Horizontal");
        float speedY = Input.GetAxis("Vertical");
        Vector2 targetDirection = new Vector2(speedX, speedY).normalized;

        if (targetDirection != Vector2.zero)
        {
            lastInputDirection = targetDirection;
        }

        if (targetDirection != Vector2.zero)
        {
            currentDirection = Vector2.Lerp(currentDirection, targetDirection, rotationSpeed * Time.deltaTime).normalized;
        }

        animator.SetFloat(horizontalParamHash, lastInputDirection.x);
        animator.SetFloat(verticalParamHash, lastInputDirection.y);
        animator.SetFloat("Speed", rb.velocity.magnitude);

        UpdateCannonPoint();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextBarrelTime)
        {
            DropExplosiveBarrel();
            nextBarrelTime = Time.time + playerStats.barrelCooldown;
        }
        AttractCoins();
    }

    void FixedUpdate()
    {
        if (isInPort) return;

        rb.AddForce(currentDirection * playerStats.moveSpeed);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void DropExplosiveBarrel()
    {
        if (!areBarrelsUnlocked)
        {
            Debug.Log("Butoaiele nu sunt deblocate încă!");
            return;
        }

        var barrel = Instantiate(explosiveBarrelPrefab, barrelSpawnPoint.position, barrelSpawnPoint.rotation);
        Rigidbody2D barrelRb = barrel.GetComponent<Rigidbody2D>();
        barrelRb.velocity = -currentDirection * barrelLaunchSpeed;

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    void UpdateCannonPoint()
    {
        float angle = Mathf.Atan2(lastInputDirection.y, lastInputDirection.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;

        int index = 0;

        if (angle >= 337.5 || angle < 22.5) index = 0; // Dreapta
        else if (angle >= 22.5 && angle < 67.5) index = 1; // Sus-Dreapta
        else if (angle >= 67.5 && angle < 112.5) index = 2; // Sus
        else if (angle >= 112.5 && angle < 157.5) index = 3; // Sus-Stânga
        else if (angle >= 157.5 && angle < 202.5) index = 4; // Stânga
        else if (angle >= 202.5 && angle < 247.5) index = 5; // Jos-Stânga
        else if (angle >= 247.5 && angle < 292.5) index = 6; // Jos
        else if (angle >= 292.5 && angle < 337.5) index = 7; // Jos-Dreapta

        if (cannonController != null && index < cannonPoints.Length)
        {
            cannonController.SetCannonPoint(cannonPoints[index]);
        }
    }

    void AttractCoins()
    {
        Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, playerStats.pickupRadius, LayerMask.GetMask("Coin"));
        foreach (var coinCollider in coins)
        {
            Coin coin = coinCollider.GetComponent<Coin>();
            if (coin != null)
            {
                coin.StartAttraction(transform);
            }
        }
    }
}