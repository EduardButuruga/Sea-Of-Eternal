using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxiliarObjects : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject foam;
    public PlayerCtrl playerCtrl;
    void Start()
    {

    }

    void Update()
    {
        if(playerCtrl != null && !playerCtrl.isInPort)
        {
            foam.SetActive(true);
        }else
        {
            foam.SetActive(false);
        }
    }
}
