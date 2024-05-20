using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkboxCtrl : MonoBehaviour
{
    public Toggle DoubleCannon;
    public Toggle HandCannon;
    public Toggle Barrel;
    public CannonController cannonController;

    public bool isDoubleCannonEnabled;
    public bool isHandCannonEnabled;
    public bool isBarrelEnabled;
    void Start()
    {
        if (DoubleCannon != null)
        {
            DoubleCannon.onValueChanged.AddListener(OnDoubleCannonToggleValueChanged);
        }

        if (HandCannon != null)
        {
            HandCannon.onValueChanged.AddListener(OnHandCannonToggleValueChanged);
        }

        if (Barrel != null)
        {
            Barrel.onValueChanged.AddListener(OnBarrelToggleValueChanged);
        }
    }

    void OnDestroy()
    {
        if (DoubleCannon != null)
        {
            DoubleCannon.onValueChanged.RemoveListener(OnDoubleCannonToggleValueChanged);
        }

        if (HandCannon != null)
        {
            HandCannon.onValueChanged.RemoveListener(OnHandCannonToggleValueChanged);
        }

        if (Barrel != null)
        {
            Barrel.onValueChanged.RemoveListener(OnBarrelToggleValueChanged);
        }
    }

    void OnDoubleCannonToggleValueChanged(bool isOn)
    {
        isDoubleCannonEnabled = isOn;
        Debug.Log("DoubleCannon is " + (isOn ? "Enabled" : "Disabled"));
    }

    void OnHandCannonToggleValueChanged(bool isOn)
    {
        isHandCannonEnabled = isOn;
        Debug.Log("HandCannon is " + (isOn ? "Enabled" : "Disabled"));
    }

    void OnBarrelToggleValueChanged(bool isOn)
    {
        isBarrelEnabled = isOn;
        Debug.Log("Barrel is " + (isOn ? "Enabled" : "Disabled"));
    }

    void Update()
    {

    }
}
