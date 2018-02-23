using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour, IInteractuableObjects {

    [HideInInspector]
    public float hpRecover = 25f;
    public Light _light;
    public AudioClip sound;
    AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = sound;
    }

    public void ActivateObject()
    {
        _light.enabled = false;
        MessegeController.instance.OpenDialog(MessageDictionary.GET_MEDICINE);
        audioSource.volume = 1;
        audioSource.Play();
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
