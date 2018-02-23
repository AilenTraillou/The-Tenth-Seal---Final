using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningPainting : MonoBehaviour {

    public ParticleSystem steam;
    public GameObject burningPainting;
    public GameObject originalPainting;
    public Texture burning;
    public AudioClip sound;
    AudioSource audioSource;

    void Start ()
    {
        steam.Stop();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = sound;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<AcidWater>())
        {
            steam.Play();
            audioSource.Play();
            Invoke("ExtinctSteam", 17f);
        }
    }

    void ExtinctSteam()
    {
        steam.Stop();
        burningPainting.GetComponent<MeshRenderer>().materials[1].mainTexture = burning;
        audioSource.Pause();
        audioSource.Stop();
    }


}
