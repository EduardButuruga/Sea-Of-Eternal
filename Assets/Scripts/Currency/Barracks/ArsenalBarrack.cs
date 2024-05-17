using UnityEngine;

public class ArsenalHub : MonoBehaviour
{
    public GameObject arsenalHubUI;

    void OnMouseDown()
    {
        OpenUI();
    }

    public void OpenUI()
    {
        if (arsenalHubUI != null)
        {
            arsenalHubUI.SetActive(true);
        }
    }

    public void CloseUI()
    {
        if (arsenalHubUI != null)
        {
            arsenalHubUI.SetActive(false);
        }
    }
}