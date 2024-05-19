using UnityEngine;

public class StartWaves : MonoBehaviour
{
    public WaveManager waveManager;
    public bool Activ = true;
    public WaveTimer waveTimer;
    public PlayerCtrl playerController;
    public GameObject port;
    public float activationDistance = 5f;

    public GameObject player;
    public AudioSource audioSource;
    public AudioClip shootSound;

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) >= activationDistance)
        {
                TogglePort();
        }
    }

    void TogglePort()
    {
        if (port != null)
        {
            port.SetActive(!port.activeSelf);
        }
    }
    private void OnMouseDown()
    {
        Debug.Log("Clopot");
        if (Activ == true)
        {
            if (waveManager != null)
            {
                waveManager.StartWaves();
            }

            if(waveTimer != null)
            {
                waveTimer.StartTimer();
            }

            if (playerController != null)
            {
                playerController.isInPort = false;
            }

            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
            Activ = false;
        }
    }
}