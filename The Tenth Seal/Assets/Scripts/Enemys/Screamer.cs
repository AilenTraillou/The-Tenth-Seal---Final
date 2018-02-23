using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screamer : Enemy {

    public Screamer_ScriptObject screamerData;
    [HideInInspector]
    public Sprite screamerImage;
    AudioClip sound;
    AudioSource audioSource;

    void Start()
    {
        damage = screamerData.damage;
        screamerImage = screamerData.screamerImage;
        sound = screamerData.screamerSound;

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = sound;

    }

    public void ActivateScreamer()
    {
        audioSource.volume = 1;
        audioSource.Play();
    }
}
