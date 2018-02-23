using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractuableObjects
{
    public bool getKey;
    public bool openDoor = false;
    public new Light light;

    private void Update()
    {
        if (openDoor && transform.position.y <= 59)
        transform.position += Vector3.up * 10 * Time.deltaTime;
    }

    public void ActivateObject()
    {
        if (getKey)
        {
            openDoor = true;          
        }
        else
        {
            openDoor = false;
            MessegeController.instance.OpenDialog(MessageDictionary.CLOSE_DOOR);
        }

        
    }

    public void DesactivateObject()
    {
        light.enabled = false;

    }

    public void ActivateOnTrigger()
    {
        light.enabled = true;

    }
}
