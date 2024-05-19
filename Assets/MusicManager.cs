using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip portMusic;
    public AudioClip seaMusic;
    public PlayerCtrl playerCtrl;
    void Start()
    {
        UpdateMusic();
    }

    void Update()
    {
        UpdateMusic();
    }

    void UpdateMusic()
    {
        if (playerCtrl.isInPort && audioSource.clip != portMusic)
        {
            audioSource.clip = portMusic;
            audioSource.Play();
        }
        else if (!playerCtrl.isInPort && audioSource.clip != seaMusic)
        {
            audioSource.clip = seaMusic;
            audioSource.Play();
        }
    }
}
