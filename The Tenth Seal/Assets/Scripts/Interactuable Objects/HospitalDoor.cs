using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalDoor : MonoBehaviour, IInteractuableObjects
{
    public event Action<string> HospitalLevelCompleted = delegate { };

    void Start()
    {
        ViewCharacter view = FindObjectOfType<ViewCharacter>();
        HospitalLevelCompleted += view.HospitalLevelCompleted;
    }

    public void ActivateObject()
    {
        if (gameObject.tag == "hospitalDoor")
            HospitalLevelCompleted("hospital");
        if (gameObject.tag == "houseDoor")
            HospitalLevelCompleted("house");
    }

    public void ActivateOnTrigger()
    {
    }

    public void DesactivateObject()
    {
    }
}
