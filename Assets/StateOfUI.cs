using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateOfUI : MonoBehaviour
{
    public bool LvlInterface = false;
    public bool MainMenu = false;
    public bool Game = false;

    public void SetLvlInterfaceTrue()
    {
        LvlInterface = true;
        MainMenu = false;
        Game = false;
    }

    public void SetMainMenuTrue()
    {
        LvlInterface = false;
        MainMenu = true;
        Game = false;
    }

    public void SetGameTrue()
    {
        LvlInterface = false;
        MainMenu = false;
        Game = true;
    }
}
