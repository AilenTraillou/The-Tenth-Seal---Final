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

        if(gameObject.tag == "GoAway")
        {
            InvokeSlugs slugs = FindObjectOfType<InvokeSlugs>();
           //w slugs.Slugs();                 
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
