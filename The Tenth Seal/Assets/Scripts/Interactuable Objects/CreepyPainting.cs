using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreepyPainting : MonoBehaviour, IInteractuableObjects {

    public GameObject originalPainting;
    public Texture painting;
    public Texture painting2;
    public AudioClip sound;
    public Image blood;
    bool activateBlood;
    AudioSource audioSource;

    public void ActivateObject()
    {
        audioSource.Play();
        NextStage();
        activateBlood = true;
    }

    void Update()
    {
        if (activateBlood)
            blood.fillAmount += 0.002f;
    }

    void NextStage()
    {
        originalPainting.GetComponent<MeshRenderer>().materials[0].mainTexture = painting;
        Invoke("NextStage2", 2f);
    }

    void NextStage2()
    {
        originalPainting.GetComponent<MeshRenderer>().materials[0].mainTexture = painting2;
    }

    public void ActivateOnTrigger()
    {

    }

    public void DesactivateObject()
    {

    }

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = sound;
        blood.fillAmount = 0;
    }
}
