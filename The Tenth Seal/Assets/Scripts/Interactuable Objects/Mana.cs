using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour, IInteractuableObjects {

    public Light _light;

    void Start()
    {
        _light.enabled = false;
    }

    public void ActivateObject()
    {      
                
    }

    public void ActivateOnTrigger()
    {
        _light.enabled = true;
    }

    public void DesactivateObject()
    {
        _light.enabled = false;
    }
}
