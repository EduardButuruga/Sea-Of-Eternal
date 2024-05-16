using UnityEngine;

public class CannonController : MonoBehaviour
{
    private Camera mainCamera;
    private Transform cannonPoint;
    public Transform cannonTip; // Referință către punctul de referință din vârful tunului
    public GameObject cannonballPrefab; // Prefab pentru ghiulea
    public float cannonballSpeed = 10f; // Viteza ghiulelei
    public Animator animator; // Animator pentru tun
    public PlayerCtrl playerController; // Referință către scriptul PlayerCtrl pentru a obține viteza bărcii

    private Vector3 lastMousePosition; // Ultima poziție a cursorului la momentul tragerii

    void Start()
    {
        mainCamera = Camera.main;
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

        // Verifică dacă jucătorul face clic stânga
        if (Input.GetMouseButtonDown(0))
        {
            // Salvează poziția cursorului la momentul tragerii
            lastMousePosition = mousePosition;
            // Activează trigger-ul pentru animația de tragere
            animator.SetTrigger("Shoot");
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
            rb.velocity = direction * cannonballSpeed + boatVelocity;
        }
    }

    public void SetCannonPoint(Transform point)
    {
        cannonPoint = point;
    }
}