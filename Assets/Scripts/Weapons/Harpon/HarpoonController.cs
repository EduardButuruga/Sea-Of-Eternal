using UnityEngine;

public class HarpoonController : MonoBehaviour
{
    public GameObject harpoon; // Referința la harpon
    public Transform boat; // Referința la barcă
    public float launchSpeed = 10f; // Viteza de lansare a harponului
    public float returnSpeed = 5f; // Viteza de întoarcere a harponului
    public float maxDistance = 10f; // Distanța maximă de lansare a harponului
    public float detectionRadius = 0.5f; // Raza de detecție pentru OverlapCircle
    public LayerMask treasureLayer; // Layer-ul pentru cufăr

    private bool isLaunched = false;
    private bool isReturning = false;
    private Transform targetTreasure;
    private Vector3 launchDirection;
    private Vector3 targetPosition;
    private Vector3 boatInitialPosition;
    private Vector3 initialPosition; // Declarația variabilei initialPosition

    void Start()
    {
        harpoon.SetActive(false);
    }

    void Update()
    {
        // Verifică dacă harponul este dezactivat înainte de a permite o nouă lansare
        if (Input.GetMouseButtonDown(1) && !isLaunched && !harpoon.activeSelf)
        {
            LaunchHarpoon();
        }

        if (isLaunched)
        {
            harpoon.transform.position += launchDirection * launchSpeed * Time.deltaTime;

            // Detectarea coliziunii cu OverlapCircle
            Collider2D detectedTreasure = Physics2D.OverlapCircle(harpoon.transform.position, detectionRadius, treasureLayer);
            if (detectedTreasure != null)
            {
                Debug.Log("Harpoon hit a treasure!"); // Debug message specific

                targetTreasure = detectedTreasure.transform;
                detectedTreasure.GetComponent<TreasureController>().AttachToHarpoon(harpoon.transform);
                StartReturning();
            }

            if (Vector3.Distance(boatInitialPosition, harpoon.transform.position) >= maxDistance)
            {
                StartReturning();
            }
        }

        if (isReturning)
        {
            Vector3 boatCurrentPosition = boat.position;
            Vector3 returnPosition = boatCurrentPosition + (initialPosition - boatInitialPosition);
            harpoon.transform.position = Vector3.MoveTowards(harpoon.transform.position, returnPosition, returnSpeed * Time.deltaTime);

            if (harpoon.transform.position == returnPosition)
            {
                isReturning = false;
                harpoon.SetActive(false);
                if (targetTreasure != null)
                {
                    targetTreasure.GetComponent<TreasureController>().DetachFromHarpoon(); // Dezactivează urmărirea harponului
                    targetTreasure = null;
                }
            }
        }
    }

    void LaunchHarpoon()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Asigură-te că poziția mouse-ului este pe planul 2D

        boatInitialPosition = boat.position;
        launchDirection = (mousePosition - boatInitialPosition).normalized;
        targetPosition = boatInitialPosition + launchDirection * maxDistance;

        initialPosition = harpoon.transform.position;
        harpoon.transform.position = initialPosition;

        // Setează rotația harponului în direcția de lansare
        float angle = Mathf.Atan2(launchDirection.y, launchDirection.x) * Mathf.Rad2Deg;
        harpoon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        harpoon.SetActive(true);
        isLaunched = true;
        isReturning = false;
    }

    void StartReturning()
    {
        isLaunched = false;
        isReturning = true;
    }
}