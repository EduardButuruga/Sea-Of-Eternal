using UnityEngine;

public class BarrelFactory : MonoBehaviour
{
    public GameObject barrelFactoryUI;

    void OnMouseDown()
    {
        OpenUI();
    }

    public void OpenUI()
    {
        if (barrelFactoryUI != null)
        {
            barrelFactoryUI.SetActive(true);
        }
    }

    public void CloseUI()
    {
        if (barrelFactoryUI != null)
        {
            barrelFactoryUI.SetActive(false);
        }
    }
}