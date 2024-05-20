using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public Transform bulletSpawnPointR; // Adaugă referința pentru al doilea punct de spawn

    public GameObject bulletPrefab;

    public Animator boatAnimator; // Referința la Animator-ul bărcii

    private bool isFiring = false;
    public AudioSource audioSource; // Componenta AudioSource
    public AudioClip shootSound; // Sunetul de împușcare
    public PlayerStats playerStats;

    // Adaugă variabilele pentru statusuri


    public PlayerCtrl playerController;


    private void Start()
    {
        if (playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }
        StartFiring();
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void Fire()
    {
        float horizontal = boatAnimator.GetFloat("Horizontal");
        float vertical = boatAnimator.GetFloat("Vertical");

        // Calculează unghiul de bază în funcție de direcția bărcii
        float baseAngle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;

        // Dacă direcția nu este definită clar (adică dacă barca nu se mișcă), folosește 0 ca unghi de bază
        if (horizontal == 0 && vertical == 0)
        {
            baseAngle = 0;
        }

        // Calculează unghiurile pentru ghiulele
        float angleStep = playerStats.bulletsPerSide > 1 ? 60f / (playerStats.bulletsPerSide - 1) : 0f;
        float startAngle = playerStats.bulletsPerSide > 1 ? -30f : 0f;

        for (int i = 0; i < playerStats.bulletsPerSide; i++)
        {
            float currentAngle = startAngle + i * angleStep;
            FireBullet(bulletSpawnPoint, baseAngle + currentAngle);
            FireBullet(bulletSpawnPointR, baseAngle + 180f + currentAngle);
        }

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void FireBullet(Transform spawnPoint, float angle)
    {
        // Setează rotația spawn point-ului și instanțiază ghiuleaua
        spawnPoint.rotation = Quaternion.Euler(0, 0, angle);

        var bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Calculează direcția și setează rotația ghiulelei în funcție de direcție
        Vector2 direction = spawnPoint.up;
        float bulletAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, bulletAngle));

        Bullet cannonballScript = bullet.GetComponent<Bullet>();
        if (cannonballScript != null)
        {
            float finalDamage = playerStats.damage * playerStats.dmgMultiplier;
            bool isCriticalHit = Random.value < playerStats.criticalStrikeChance;
            if (isCriticalHit)
            {
                finalDamage *= playerStats.criticalDamageMultiplier;
                Debug.Log("Critical hit!");
            }
            cannonballScript.SetDamage(finalDamage, isCriticalHit);

            // Setează parametrul isCritical în Animatorul cannonball-ului
            Animator cannonballAnimator = bullet.GetComponent<Animator>();
            if (cannonballAnimator != null)
            {
                cannonballAnimator.SetFloat("isCritical", isCriticalHit ? 1f : 0f);
            }
        }

        rb.velocity = direction * playerStats.cannonballSpeed;
    }

    private IEnumerator AutoFire()
    {
        while (isFiring)
        {
            if (!playerController.isInPort)
            Fire();
            yield return new WaitForSeconds(1f / playerStats.attackSpeed);
        }
    }

    public void StartFiring()
    {
        isFiring = true;
        StartCoroutine(AutoFire());
    }

    public void StopFiring()
    {
        isFiring = false;
        StopCoroutine(AutoFire());
    }
}