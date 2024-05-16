using UnityEngine;

public class TreasureController : MonoBehaviour
{
    private Transform harpoon;

    public void AttachToHarpoon(Transform harpoonTransform)
    {
        Debug.Log("Treasure attached to harpoon!"); // Debug message

        harpoon = harpoonTransform;
        // Dezactivează fizica obiectului pentru a preveni interferențele
        GetComponent<Rigidbody2D>().isKinematic = true;
        // Atașează obiectul de harpon
        transform.parent = harpoon;
    }

    public void DetachFromHarpoon()
    {
        Debug.Log("Treasure detached from harpoon!"); // Debug message

        // Eliberează referința la harpon
        harpoon = null;
        // Reactivatează fizica obiectului
        GetComponent<Rigidbody2D>().isKinematic = false;
        // Dezparentă cufărul
        transform.parent = null;
    }

    void Update()
    {
        if (harpoon != null)
        {
            // Urmărește harponul
            transform.position = harpoon.position;
        }
    }
}