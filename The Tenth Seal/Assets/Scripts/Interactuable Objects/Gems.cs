using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour, IInteractuableObjects
{
    public Light _light;

    public void ActivateObject()
    {
        MessegeController.instance.OpenDialog(MessageDictionary.GET_GEMS);
        ObjectsCount.instance.gems++;
        print(ObjectsCount.instance.gems);
        _light.enabled = false;
    }

    public void DesactivateObject()
    {
        _light.enabled = false;
    }

    public void ActivateOnTrigger()
    {
        _light.enabled = true;
    }
}
