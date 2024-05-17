using UnityEngine;

public class ArtilleryBarrack : MonoBehaviour
{
    public GameObject artilleryUI;

    void OnMouseDown()
    {
        OpenUI();
    }

    public void OpenUI()
    {
        if (artilleryUI != null)
        {
            artilleryUI.SetActive(true);
        }
    }

    public void CloseUI()
    {
        if (artilleryUI != null)
        {
            artilleryUI.SetActive(false);
        }
    }
}