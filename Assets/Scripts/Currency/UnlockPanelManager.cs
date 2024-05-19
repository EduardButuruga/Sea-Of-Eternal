using UnityEngine;

public class UnlockPanelManager : MonoBehaviour
{
    public GameObject unlockPanel; // Referința la panoul de deblocare
    public PlayerCtrl playerCtrl;

    // Metodă pentru a deschide panoul de deblocare
    public void OpenPanel()
    {
        if(playerCtrl.isInPort)
        unlockPanel.SetActive(true);
    }

    // Metodă pentru a închide panoul de deblocare
    public void ClosePanel()
    {

        unlockPanel.SetActive(false);
    }

    private void OnMouseDown()
    {
        OpenPanel();
    }
}