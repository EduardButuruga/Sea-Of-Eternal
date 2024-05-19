using System.Collections;
using UnityEngine;

[System.Serializable]
public class CannonSettings
{
    public Transform bulletSpawnPoint;
    public int numberOfBullets = 5;
    public float bulletSpeed = 10f;
    public float attackSpeed = 1f; // Viteza de atac pentru tun
    public float damage = 10f; // Daunele tunului
    public float bulletSpread = 10f; // Unghiul de dispersie a ghiulelelor
}

public class BossCannon : MonoBehaviour
{
    public CannonSettings leftCannon;
    public CannonSettings rightCannon;
    public GameObject bulletPrefab;
    private bool isFiring = false;
    public Animator boatAnimator; // Referința la Animator-ul bărcii

    void Start()
    {
        StartFiring();
    }

    private void Fire(CannonSettings cannon, Vector2 baseDirection)
    {
        for (int i = 0; i < cannon.numberOfBullets; i++)
        {
            float angleOffset = (i - (cannon.numberOfBullets - 1) / 2f) * (cannon.bulletSpread / cannon.numberOfBullets); // Distribuie unghiul pe mai multe ghiulele
            Vector2 fireDirection = Quaternion.Euler(0, 0, angleOffset) * baseDirection;
            FireBullet(cannon.bulletSpawnPoint, fireDirection, cannon.bulletSpeed, cannon.damage);
        }
    }

    private void FireBullet(Transform spawnPoint, Vector2 direction, float bulletSpeed, float damage)
    {
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        Transform bulletTransform = Instantiate(bulletPrefab, spawnPoint.position, rotation).transform;

        Rigidbody2D rb = bulletTransform.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;

        EnemyBullet cannonballScript = bulletTransform.GetComponent<EnemyBullet>();
        if (cannonballScript != null)
        {
            cannonballScript.SetDamage(damage);          
        }
    }

    private IEnumerator AutoFire()
    {
        while (isFiring)
        {
            Vector2 leftDirection;
            Vector2 rightDirection;

            float horizontal = boatAnimator.GetFloat("Horizontal");
            float vertical = boatAnimator.GetFloat("Vertical");

            if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
            {
                if (horizontal > 0)
                {
                    leftDirection = Vector2.up;
                    rightDirection = Vector2.down;
                }
                else
                {
                    leftDirection = Vector2.down;
                    rightDirection = Vector2.up;
                }
            }
            else if (Mathf.Abs(vertical) > Mathf.Abs(horizontal))
            {
                if (vertical > 0)
                {
                    leftDirection = Vector2.left;
                    rightDirection = Vector2.right;
                }
                else
                {
                    leftDirection = Vector2.right;
                    rightDirection = Vector2.left;
                }
            }
            else
            {
                if (horizontal > 0 && vertical > 0)
                {
                    leftDirection = (Vector2.up + Vector2.left).normalized;
                    rightDirection = (Vector2.down + Vector2.right).normalized;
                }
                else if (horizontal > 0 && vertical < 0)
                {
                    leftDirection = (Vector2.down + Vector2.left).normalized;
                    rightDirection = (Vector2.up + Vector2.right).normalized;
                }
                else if (horizontal < 0 && vertical > 0)
                {
                    leftDirection = (Vector2.up + Vector2.right).normalized;
                    rightDirection = (Vector2.down + Vector2.left).normalized;
                }
                else
                {
                    leftDirection = (Vector2.down + Vector2.right).normalized;
                    rightDirection = (Vector2.up + Vector2.left).normalized;
                }
            }

            Fire(leftCannon, leftDirection);
            Fire(rightCannon, rightDirection);
            yield return new WaitForSeconds(1f / Mathf.Min(leftCannon.attackSpeed, rightCannon.attackSpeed));
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