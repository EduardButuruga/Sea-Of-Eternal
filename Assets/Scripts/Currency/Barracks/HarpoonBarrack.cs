using UnityEngine;

public class HarpoonStation : MonoBehaviour
{
    public GameObject harpoonStationUI;

    void OnMouseDown()
    {
        OpenUI();
    }

    public void OpenUI()
    {
        if (harpoonStationUI != null)
        {
            harpoonStationUI.SetActive(true);
        }
    }

    public void CloseUI()
    {
        if (harpoonStationUI != null)
        {
            harpoonStationUI.SetActive(false);
        }
    }
}