using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour, IOnPause {

    Animator anim;
    bool startAnim;
    bool onPause;

    public AudioClip[] clips;
    public AudioSource[] channels;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0;
        channels = new AudioSource[clips.Length];
        for (int i = 0; i < clips.Length; i++)
        {
            channels[i] = gameObject.AddComponent<AudioSource>();
            channels[i].clip = clips[i];
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.name == "Personaje")
        {
            startAnim = true;
        }else
        {
            startAnim = false;
        }
    }

    void Update()
    {
        if (startAnim)
        {
            Play(0, 0.5f);         
            startAnim = false;
            Destroy(this);
        }    
    }

    void Play(int soundID, float volume = 1, bool loop = false)
    {
        if (channels[soundID].isPlaying) return;
        anim.speed = 0.1f;
        channels[soundID].Play();
        channels[soundID].volume = volume;
        channels[soundID].loop = loop;
        channels[soundID].pitch = 0.5f;
    }

    public void OnPause(bool isOnPause)
    {
        onPause = isOnPause;
        if (onPause == false)
        {
            if(startAnim)
                anim.speed = 0.2f;
        }
        else
            anim.speed = 0.0f;

    }
}
