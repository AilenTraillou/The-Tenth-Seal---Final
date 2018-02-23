using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : MonoBehaviour, IInteractuableObjects
{
    public bool canGetOil = true;
    public float oilRecover = 25f;

    public void ActivateObject()
    {
        if(canGetOil)
            MessegeController.instance.OpenDialog(MessageDictionary.GET_OIL);
        else
            MessegeController.instance.OpenDialog(MessageDictionary.CANNOT_GET_OIL);

    }

    public void DesactivateObject()
    {

    }

    public void ActivateOnTrigger()
    {
        
    }
}
