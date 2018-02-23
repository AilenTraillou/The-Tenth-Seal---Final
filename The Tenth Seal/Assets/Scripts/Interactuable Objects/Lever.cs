using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractuableObjects
{

    public Light leverLight;
    Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
        leverLight.enabled = false;
    }

    public void ActivateObject()
    {
        leverLight.enabled = false;
        GetComponent<Animation>().Play("Lever");
        ObjectsCount.instance.getlever++;
        if (ObjectsCount.instance.getlever == 4)
        {
            WaterAndWaterfall.instance.startAnimation = true;
        }

        Destroy(this);
    }

    public void DesactivateObject()
    {
        leverLight.enabled = false;
    }

    public void ActivateOnTrigger()
    {
        leverLight.enabled = true;
    }

}
