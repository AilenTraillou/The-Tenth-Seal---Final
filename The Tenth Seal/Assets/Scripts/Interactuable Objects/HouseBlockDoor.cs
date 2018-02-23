using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBlockDoor : MonoBehaviour, IInteractuableObjects {

    public Light _light;

    public void ActivateObject()
    {
        MessegeController.instance.OpenDialog(MessageDictionary.BLOCK_DOOR);
        _light.enabled = false;
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
