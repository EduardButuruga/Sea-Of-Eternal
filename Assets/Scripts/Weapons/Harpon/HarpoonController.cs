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
    private Vector3 initialPosition; // Declarația variabilei initialPosition
    private LineRenderer lineRenderer; // Referință la LineRenderer
    private DistanceJoint2D distanceJoint; // Referință la DistanceJoint2D

    void Start()
    {
        harpoon.SetActive(false);
        lineRenderer = harpoon.GetComponent<LineRenderer>();
        distanceJoint = harpoon.GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false; // Dezactivează joint-ul inițial
        lineRenderer.enabled = false; // Dezactivează LineRenderer inițial

        // Setează Layer-ul și Order-ul pentru LineRenderer
        lineRenderer.sortingLayerName = "Foreground"; // Asigură-te că acest strat există
        lineRenderer.sortingOrder = 5; // O valoare mai mare pentru a fi desenat deasupra altor obiecte
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

            // Actualizează linia sforii
            UpdateLineRenderer();

            // Detectarea coliziunii cu OverlapCircle
            Collider2D detectedTreasure = Physics2D.OverlapCircle(harpoon.transform.position, detectionRadius, treasureLayer);
            if (detectedTreasure != null)
            {
                Debug.Log("Harpoon hit a treasure!"); // Debug message specific

                targetTreasure = detectedTreasure.transform;
                detectedTreasure.GetComponent<TreasureController>().AttachToHarpoon(harpoon.transform);
                StartReturning();
            }

            // Verifică dacă distanța maximă relativă la barcă a fost atinsă
            if (Vector3.Distance(boat.position, harpoon.transform.position) >= maxDistance)
            {
                StartReturning();
            }
        }

        if (isReturning)
        {
            Vector3 returnPosition = boat.position;
            harpoon.transform.position = Vector3.MoveTowards(harpoon.transform.position, returnPosition, returnSpeed * Time.deltaTime);

            // Actualizează linia sforii
            UpdateLineRenderer();

            // Actualizează rotația harponului cu spatele la barcă
            Vector3 directionToBoat = boat.position - harpoon.transform.position;
            float angle = Mathf.Atan2(directionToBoat.y, directionToBoat.x) * Mathf.Rad2Deg;
            harpoon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));

            if (harpoon.transform.position == returnPosition)
            {
                isReturning = false;
                harpoon.SetActive(false);
                lineRenderer.enabled = false; // Dezactivează LineRenderer când harponul este inactiv
                distanceJoint.enabled = false; // Dezactivează DistanceJoint2D când harponul este inactiv
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

        launchDirection = (mousePosition - boat.position).normalized;
        targetPosition = boat.position + launchDirection * maxDistance;

        harpoon.transform.position = boat.position; // Lansează harponul din poziția bărcii

        // Setează rotația harponului în direcția de lansare
        float angle = Mathf.Atan2(launchDirection.y, launchDirection.x) * Mathf.Rad2Deg;
        harpoon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        harpoon.SetActive(true);
        isLaunched = true;
        isReturning = false;

        // Activează DistanceJoint2D și LineRenderer
        distanceJoint.enabled = true;
        distanceJoint.connectedBody = boat.GetComponent<Rigidbody2D>();

        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2; // Setează numărul de puncte la 2
        UpdateLineRenderer(); // Actualizează linia sforii imediat după lansare
    }

    void StartReturning()
    {
        isLaunched = false;
        isReturning = true;
    }

    void UpdateLineRenderer()
    {
        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(0, harpoon.transform.position);
            lineRenderer.SetPosition(1, boat.position);
        }
    }
}