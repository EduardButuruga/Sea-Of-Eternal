using UnityEngine;

public class SharkBossHealth : MonoBehaviour
{
    public float baseHealth = 100f;
    public int xpValue = 20;
    private PlayerXp playerXp;
    public float currentHealth;
    public GameObject coinPrefab;
    public int minCoins = 1;
    public int maxCoins = 5;
    public float dropRadius = 0.5f;
    public Vector3 damagePopupOffset = new Vector3(0, 1, 0);

    private GameObject healthBarParent;
    private BossHealthBar healthBar;
    private bool isDead = false;

    void Start()
    {
        playerXp = GameObject.FindWithTag("Player").GetComponent<PlayerXp>();
        currentHealth = baseHealth;

        healthBarParent = GameObject.FindWithTag("ShBossBar");
        if (healthBarParent != null)
        {
            SetChildrenActive(healthBarParent, true);
            Transform healthBarTransform = healthBarParent.transform.Find("BossHealthBarBlack");
            if (healthBarTransform != null)
            {
                healthBar = healthBarTransform.GetComponent<BossHealthBar>();
                if (healthBar != null)
                {
                    healthBar.SetMaxHealth((int)currentHealth);
                    healthBar.SetHealth((int)currentHealth);
                }
            }
        }
    }

    public void TakeDamage(float amount, bool isCriticalHit)
    {
        if (isDead) return;

        currentHealth -= amount;
        DamagePopUP.Create(transform.position, (int)amount, isCriticalHit, damagePopupOffset);

        if (healthBar != null)
        {
            healthBar.SetHealth((int)currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeFlatDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        DamagePopUP.Create(transform.position, (int)amount, false, damagePopupOffset);

        if (healthBar != null)
        {
            healthBar.SetHealth((int)currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void IncreaseHealth(int waveNumber)
    {
        currentHealth = baseHealth * Mathf.Pow(1.2f, waveNumber);

        if (healthBar != null)
        {
            healthBar.SetMaxHealth((int)currentHealth);
            healthBar.SetHealth((int)currentHealth);
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;

        if (playerXp != null)
        {
            playerXp.AddXP(xpValue);
        }

        DropCoins();

        if (healthBarParent != null)
        {
            SetChildrenActive(healthBarParent, false);
        }

        Destroy(gameObject);
    }

    void DropCoins()
    {
        int coinDropCount = Random.Range(minCoins, maxCoins + 1);
        for (int i = 0; i < coinDropCount; i++)
        {
            Vector2 dropPosition = GetRandomDropPosition();
            Instantiate(coinPrefab, dropPosition, Quaternion.identity);
        }
    }

    Vector2 GetRandomDropPosition()
    {
        Vector2 randomOffset = Random.insideUnitCircle * dropRadius;
        return (Vector2)transform.position + randomOffset;
    }

    private void SetChildrenActive(GameObject parent, bool active)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}