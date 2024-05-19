using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers : MonoBehaviour
{
    public PlayerCtrl playerCtrl;
    void Update()
    {
        if (playerCtrl.isInPort)
        {
            EnableAllChildren();
        }
        else
        {
            DisableAllChildren();
        }
    }
    public void DisableAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    public void EnableAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
