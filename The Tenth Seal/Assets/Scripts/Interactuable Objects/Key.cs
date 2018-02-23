using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractuableObjects
{
    
    public Door door;
    public Light light;

    public void Start()
    {
        light.enabled = false;
    }

    public void ActivateObject()
    {
        door.getKey = true;
        MessegeController.instance.OpenDialog(MessageDictionary.GET_KEY);

    }

    public void DesactivateObject()
    {
    }

    public void ActivateOnTrigger()
    {
        light.enabled = true;
    }
}
