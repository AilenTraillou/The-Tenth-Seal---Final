using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour {

    public AudioClip[] clips;
    public AudioSource[] channels;
    public List<IObservable> observableList = new List<IObservable>();

    public void Play(int soundID, float volume = 1, bool loop = false)
    {
        if (channels[soundID].isPlaying) return;

        channels[soundID].Play();
        channels[soundID].volume = volume;
        channels[soundID].loop = loop;

    }

    public void Stop(int soundID)
    {

        if (channels[soundID].isPlaying) return;

        channels[soundID].Stop();

    }

    public void Mute(int soundID, bool mute)
    {
        if (!channels[soundID].isPlaying) return;
        channels[soundID].mute = mute;
    }

    public void StopAll()
    {
        for (int i = 0; i < channels.Length; i++)
        {
            channels[i].Stop();

        }
    }


    public void NewObservable()
    {

    }


}

public enum SoundID
{
    Jump,
    Land,
    Footsteps,
    Cementery_Music,
    Sream,
    Chains,
    Sonido_bicho,
    waterfall,
    water_footsteps,
    pulse_1,
    door,
    agua_drenando,
    monster_screamer,
    ghost_screamer, 
    pickUpItem


}

