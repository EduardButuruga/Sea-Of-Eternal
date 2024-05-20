using System;
using UnityEngine;

public class WeaponUnlocks : MonoBehaviour
{
    public bool isHandCannonUnlocked = false;
    public bool isHarpoonUnlocked = false;
    public bool isDoubleCannonUnlocked = false;
    public bool isBarrelUnlocked = false;

    internal static WeaponUnlocks FindWithTag(string v)
    {
        throw new NotImplementedException();
    }
}