using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public GameObject StatsUI;

    void OnMouseDown()
    {
        OpenUI();
    }

    public void OpenUI()
    {
        if (StatsUI != null)
        {
            StatsUI.SetActive(true);
        }
    }

    public void CloseUI()
    {
        if (StatsUI != null)
        {
            StatsUI.SetActive(false);
        }
    }
}
