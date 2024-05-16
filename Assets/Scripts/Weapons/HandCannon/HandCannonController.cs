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

    private Vector3 lastMousePosition; // Ultima poziție a cursorului la momentul tragerii

    public AudioSource audioSource; // Componenta AudioSource
    public AudioClip shootSound; // Sunetul de împușcare

    [Header("Cannon Stats")]
    public float damage = 10f; // Damage-ul tunului
    public float attackSpeed = 1f; // Viteza de atac
    public float cannonballSpeed = 10f; // Viteza ghiulelei
    public float criticalStrikeChance = 0.1f; // Șansa de lovitură critică
    public float criticalDamageMultiplier = 2f; // Multiplicatorul de damage pentru lovitura critică

    private bool isFiring = false; // Indicator pentru a verifica dacă tunul trage
    private bool canFire = true;
    private Coroutine firingCoroutine;


    void Start()
    {
        mainCamera = Camera.main;
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (cannonPoint != null)
        {
            // Sincronizează poziția tunului cu poziția punctului de ancorare
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

        // Verifică dacă jucătorul ține apăsat butonul stâng de mouse
       
            if (Input.GetMouseButtonDown(0))
            {
                if (!isFiring)
                {
                    isFiring = true;
                        if (canFire)
                            firingCoroutine = StartCoroutine(FireContinuously());
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (isFiring)
                {
                    isFiring = false;
                        if (canFire)
                            StopCoroutine(firingCoroutine);
                }
            }
    }

    private IEnumerator FireContinuously()
    {
        if (canFire)
        {
            while (isFiring)
            {

                // Salvează poziția cursorului la momentul tragerii
                lastMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                lastMousePosition.z = 0; // Asigură-te că z-ul este 0 pentru calcul corect

                // Activează trigger-ul pentru animația de tragere
                animator.SetTrigger("Shoot");

                // Așteaptă până când tunul poate trage din nou, în funcție de viteza de atac
                yield return new WaitForSeconds(1f / attackSpeed);

            }
        }
    }

    // Această metodă va fi apelată de evenimentul de animație
    public void FireCannonball()
    {
        if (canFire)
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
                rb.velocity = direction * cannonballSpeed + boatVelocity;
            }

            // Aplică damage-ul și șansa de lovitură critică
            Bullet cannonballScript = cannonball.GetComponent<Bullet>();
            if (cannonballScript != null)
            {
                float finalDamage = damage;
                if (Random.value < criticalStrikeChance)
                {
                    finalDamage *= criticalDamageMultiplier;
                    Debug.Log("Critical hit!");
                }
                cannonballScript.SetDamage(finalDamage);
            }

            // Redă sunetul de împușcare
            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }

            // Setează canFire la false și așteaptă intervalul de cooldown
            canFire = false;
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f / attackSpeed);
        canFire = true;
    }

    public void SetCannonPoint(Transform point)
    {
        cannonPoint = point;
    }
}