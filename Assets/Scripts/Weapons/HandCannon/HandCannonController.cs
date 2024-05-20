using System.Collections;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    private Camera mainCamera;
    private Transform cannonPoint;
    public Transform cannonTip; // Referință către punctul de referință din vârful tunului
    public GameObject cannonballPrefab; // Prefab pentru ghiulea
    public Animator animator; // Animator pentru tun
    public PlayerCtrl playerController; // Referință către scriptul PlayerCtrl pentru a obține viteza bărcii
    public PlayerStats playerStats;

    private Vector3 lastMousePosition; // Ultima poziție a cursorului la momentul tragerii

    public AudioSource audioSource; // Componenta AudioSource
    public AudioClip shootSound; // Sunetul de împușcare

    private bool isFiring = false; // Indicator pentru a verifica dacă tunul trage
    private bool canFire = true;
    private Coroutine firingCoroutine;
    private bool isCriticalHit; // Variabilă pentru a stoca dacă lovitura este critică

    void Start()
    {
        if (playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }
        mainCamera = Camera.main;
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (playerController.isInPort) return;

        // Sincronizează poziția tunului cu poziția punctului de ancorare
        if (cannonPoint != null)
        {
            transform.position = cannonPoint.position;
        }

        // Obține poziția cursorului în lumea jocului
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Asigură-te că z-ul este 0 pentru calcul corect

        // Calculează direcția de la vârful tunului către cursor
        Vector3 direction = mousePosition - cannonTip.position;

        // Calculează unghiul pentru a roti tunul
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aplică rotația tunului, păstrând referința vârfului spre cursor
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Verifică dacă jucătorul apasă butonul stâng de mouse
        if (Input.GetMouseButtonDown(0))
        {
            if (canFire)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        if (canFire)
        {
            // Salvează poziția cursorului la momentul tragerii
            lastMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            lastMousePosition.z = 0; // Asigură-te că z-ul este 0 pentru calcul corect

            // Determină dacă lovitura este critică
            isCriticalHit = Random.value < playerStats.HandCannonCriticalStrikeChance;

            // Activează trigger-ul pentru animația de tragere
            animator.SetTrigger("Shoot");

            // Pornește cooldown-ul
            StartCoroutine(Cooldown());
        }
    }

    // Această metodă va fi apelată de evenimentul de animație
    public void FireCannonball()
    {
        // Instanțiază ghiuleaua la poziția vârfului tunului
        GameObject cannonball = Instantiate(cannonballPrefab, cannonTip.position, Quaternion.identity);

        // Calculează direcția de la vârful tunului către ultima poziție a cursorului
        Vector2 direction = (lastMousePosition - cannonTip.position).normalized;

        // Setează rotația ghiulelei în funcție de direcție
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        cannonball.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Adaugă o forță ghiulelei pentru a o lansa în direcția cursorului
        Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Compensează viteza bărcii
            Vector2 boatVelocity = playerController != null ? playerController.GetComponent<Rigidbody2D>().velocity : Vector2.zero;
            rb.velocity = direction * playerStats.HandCannonCannonballSpeed + boatVelocity;
        }

        // Setează parametrul isCritical în Animatorul cannonball-ului
        Animator cannonballAnimator = cannonball.GetComponent<Animator>();
        if (cannonballAnimator != null)
        {
            Debug.Log($"Critical Hit: {isCriticalHit}"); // Linie de debug
            cannonballAnimator.SetFloat("isCritical", isCriticalHit ? 1f : 0f);
        }

        // Aplică damage-ul și șansa de lovitură critică
        Bullet cannonballScript = cannonball.GetComponent<Bullet>();
        if (cannonballScript != null)
        {
            float finalDamage = playerStats.HandCannonDamage * playerStats.dmgMultiplier;
            if (isCriticalHit)
            {
                finalDamage *= playerStats.HandCannonCriticalDamageMultiplier;
                Debug.Log("Critical hit!");
            }
            cannonballScript.SetDamage(finalDamage, isCriticalHit);
        }

        // Redă sunetul de împușcare
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private IEnumerator Cooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(1f / playerStats.HandCannonAttackSpeed);
        canFire = true;
    }

    public void SetCannonPoint(Transform point)
    {
        cannonPoint = point;
    }
    public void OnDeath()
    {
        gameObject.SetActive(false);
    }

    public void ResetCannon()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(-0.968f, -0.58f, 0f);
        canFire = true;
    }
}
