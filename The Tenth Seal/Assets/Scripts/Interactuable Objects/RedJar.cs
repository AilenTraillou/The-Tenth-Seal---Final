using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedJar : MonoBehaviour, IInteractuableObjects {

    public event Action FirstJarActivated = delegate { };
    public event Action AllRedJarsActivated = delegate { };

    public ParticleSystem steam;
    public AudioClip sound;
    public Light _light;
    AudioSource audioSource;
    InvokeMonster invokeMonster;
    public SkeletonMonster monster;

    void Start()
    {
        _light.enabled = false;
        steam.Stop();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = sound;

        invokeMonster = FindObjectOfType<InvokeMonster>();
        FirstJarActivated += invokeMonster.InvokeSkeletonMonster;
        AllRedJarsActivated += monster.MonsterDead;
        
    }

    public void ActivateObject()
    {
        if (ObjectsCount.instance.getRedSubstances)
        {
            steam.Play();
            _light.enabled = true;
            ActivateSteam();
            audioSource.volume = 1;
            audioSource.Play();
            ObjectsCount.instance.redJarRemaining--;
            MessegeController.instance.OpenDialog(ObjectsCount.instance.redJarRemaining + " remaining...");
        }
        else
        {
            MessegeController.instance.OpenDialog(MessageDictionary.CANNOT_ACTIVATE_RED_JAR);
        }

        if (ObjectsCount.instance.redJarRemaining != 4)
            FirstJarActivated();

        if (ObjectsCount.instance.redJarRemaining == 0)
            AllRedJarsActivated();
    }

    public void ActivateOnTrigger()
    {
        
    }

    public void DesactivateObject()
    {

    }

    void StopSteam()
    {
        steam.Stop();
    }

    void ActivateSteam()
    {
        steam.Play();
        _light.enabled = true;
        Invoke("StopSteam", 10f);
    }

    void Update()
    {
		
	}
}
