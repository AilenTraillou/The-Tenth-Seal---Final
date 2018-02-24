using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour, IInteractuableObjects, IChechPointObservable {

    public ParticleSystem steam;
    public Transform ph;
    public AudioClip sound;
    public Light _light;
    bool activeJar;
    bool stop;
    ICheckPointObserver checkpointManager;
    AudioSource audioSource;

    void Start ()
    {
        _light.enabled = false;
        steam.Stop();
        checkpointManager = FindObjectOfType<CheckPoint>();
        Suscribe(checkpointManager);
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = sound;
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
      //  MessegeController.instance.OpenDialog(MessageDictionary.SPELL_DONE);
    }

    public void ActivateObject()
    {
        if (!stop)
        {
            FindObjectOfType<ModelCharacter>().usedCheckpoint = true;

            audioSource.volume = 1;
            audioSource.Play();
            ActivateSteam();
            checkpointManager.Notify(ph);
            activeJar = false;

            MessegeController.instance.OpenDialog(MessageDictionary.SPELL_DONE);

            if (gameObject.tag == "GoAway")
            {
                GoAwayMessage goAway = FindObjectOfType<GoAwayMessage>();
                if (goAway != null)
                    goAway.activateMessage = true;
            }
            stop = true;
        }

       /* if(ObjectsCount.instance.mana == 0 && activeJar == false)
        {
                MessegeController.instance.OpenDialog(MessageDictionary.CANNOT_GET_CHECKPOINT);
        }
        */
      
    }

    public void DesactivateObject()
    {

    }

    public void ActivateOnTrigger()
    {
        
    }

    public void Suscribe(ICheckPointObserver observer)
    {
        checkpointManager = observer;
    }

    public void Unsuscribe(ICheckPointObserver observer)
    {
        checkpointManager = null;
    }
}
