using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientMusicManager : MonoBehaviour {

    public AudioClip[] clips;
    public AudioSource[] channels;

    void Start () {

        Play(AmbientMusic.OLD_HOUSE, 0.4f, true);
     
    }

    void Awake()
    {
        channels = new AudioSource[clips.Length];

        for (int i = 0; i < clips.Length; i++)
        {
            channels[i] = gameObject.AddComponent<AudioSource>();
            channels[i].clip = clips[i];
        }
    }

    void Play(int soundID, float volume = 1, bool loop = false)
    {
        if (channels[soundID].isPlaying) return;

        channels[soundID].Play();
        channels[soundID].volume = volume;
        channels[soundID].loop = loop;

    }

    public class AmbientMusic
    {
        public const int OLD_HOUSE = 0;
        public const int HOSPITAL = 1;
        public const int GRAVEYARD= 2;

    }
}
