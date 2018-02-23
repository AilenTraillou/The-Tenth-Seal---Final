using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSubstance : MonoBehaviour, IInteractuableObjects {

    public Light _light;

    public void ActivateObject()
    {
        _light.enabled = false;
        MessegeController.instance.OpenDialog(MessageDictionary.GET_RED_SUBSTANCES);
        ObjectsCount.instance.getRedSubstances = true;
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
