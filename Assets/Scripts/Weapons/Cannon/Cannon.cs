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

    // Adaugă variabilele pentru statusuri
    public float damage = 10f; // Damage-ul tunului
    public float attackSpeed = 1f; // Viteza de atac
    public float criticalStrikeChance = 0.1f; // Șansa de lovitură critică
    public float criticalDamageMultiplier = 2f; // Multiplicatorul de damage pentru lovitura critică
    public int bulletsPerSide = 1; // Numărul de gloanțe pe fiecare parte
    public float cannonballSpeed = 10f;
    public PlayerCtrl playerController;


    private void Start()
    {
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
        float angleStep = bulletsPerSide > 1 ? 60f / (bulletsPerSide - 1) : 0f;
        float startAngle = bulletsPerSide > 1 ? -30f : 0f;

        for (int i = 0; i < bulletsPerSide; i++)
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
            float finalDamage = damage;
            bool isCriticalHit = Random.value < criticalStrikeChance;
            if (isCriticalHit)
            {
                finalDamage *= criticalDamageMultiplier;
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

        rb.velocity = direction * cannonballSpeed;
    }

    private IEnumerator AutoFire()
    {
        while (isFiring)
        {
            if (!playerController.isInPort)
            Fire();
            yield return new WaitForSeconds(1f / attackSpeed);
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