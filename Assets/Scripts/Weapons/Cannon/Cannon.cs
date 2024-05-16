using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public Transform bulletSpawnPointR; // Adaugă referința pentru al doilea punct de spawn

    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public float fireInterval = 1f;

    public Animator boatAnimator; // Referința la Animator-ul bărcii

    private bool isFiring = false;
    public AudioSource audioSource; // Componenta AudioSource
    public AudioClip shootSound; // Sunetul de împușcare

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
        // Obține direcția bărcii din Animator
        float horizontal = boatAnimator.GetFloat("Horizontal");
        float vertical = boatAnimator.GetFloat("Vertical");

        // Calculează unghiul de tragere în funcție de direcția bărcii
        float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;

        // Setează rotația punctelor de tragere
        bulletSpawnPoint.rotation = Quaternion.Euler(0, 0, angle);
        bulletSpawnPointR.rotation = Quaternion.Euler(0, 0, angle + 180f);

        // Creează gloanța pentru tunul stâng
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;

        // Creează gloanța pentru tunul drept
        var bulletR = Instantiate(bulletPrefab, bulletSpawnPointR.position, bulletSpawnPointR.rotation);
        bulletR.GetComponent<Rigidbody2D>().velocity = bulletSpawnPointR.up * bulletSpeed;

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private IEnumerator AutoFire()
    {
        while (isFiring)
        {
            Fire();
            yield return new WaitForSeconds(fireInterval);
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