using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelStatue : MonoBehaviour, IInteractuableObjects {

    public event Action FreeSoul = delegate { };
    public event Action EndOfGame = delegate { };

    public Light _light;
    public ParticleSystem steam;

    ViewCharacter view;

    void Start()
    {
        view = FindObjectOfType<ViewCharacter>();
        EndOfGame += view.EndOfGame;
    }

    public void ActivateObject()
    {
        _light.enabled = false;

        if(ObjectsCount.instance.gems != 3)
        {
            MessegeController.instance.OpenDialog(MessageDictionary.CANNOT_ACTIVATE_STATUE);
        }
        else
        {
            ActivateSteam();
        }
    }

    void ActivateSteam()
    {
        steam.Play();
        _light.enabled = true;
        Invoke("StopSteam", 10f);
    }

    void StopSteam()
    {
        steam.Stop();
        MessegeController.instance.OpenDialog(MessageDictionary.ACTIVATE_STATUE);
        EndOfGame();
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
