using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLight : MonoBehaviour, IInteractuableObjects
{

    public Light light;

    void Start()
    {
        light.enabled = false;
    }

    public void ActivateObject()
    {
        MessegeController.instance.OpenDialog(MessageDictionary.GET_SUBSTANCE);
    }

    public void DesactivateObject()
    {

    }

    public void ActivateOnTrigger()
    {
        light.enabled = true;
    }
}
