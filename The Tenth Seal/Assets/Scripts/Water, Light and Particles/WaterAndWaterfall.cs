using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAndWaterfall : MonoBehaviour, IOnPause {

    public static WaterAndWaterfall instance;

    public GameObject waterFloor1;
    public GameObject waterFloor2;

    public ParticleSystem waterfall1_p1;
    public ParticleSystem waterfall1_p2;
    public ParticleSystem waterfall1_p3;
    public ParticleSystem waterfall1_p4;

    public ParticleSystem waterfall2_p1;
    public ParticleSystem waterfall2_p2;
    public ParticleSystem waterfall2_p3;
    public ParticleSystem waterfall2_p4;

    public ParticleSystem waterfall3_p1;
    public ParticleSystem waterfall3_p2;
    public ParticleSystem waterfall3_p3;
    public ParticleSystem waterfall3_p4;

    public ParticleSystem waterfall4_p1;
    public ParticleSystem waterfall4_p2;
    public ParticleSystem waterfall4_p3;
    public ParticleSystem waterfall4_p4;

    public Material water;
    public AudioSource waterfallAudio;
    public bool startAnimation;
    public bool waterOn;
    private float volume = 5;

    Vector4 waveSpeed = new Vector4(4, 2, 1, 1);
    ModelCharacter model;
    bool onPause;

    bool waterfall1;
    bool waterfall2;
    bool waterfall3;
    bool waterfall4;

    void Start () {

        model = FindObjectOfType<ModelCharacter>();
        instance = this;
    }

    void Update () {

        volume -= Time.deltaTime * 0.5f;

        if (ObjectsCount.instance.getlever == 1)
            waterfall1 = true;
        if (ObjectsCount.instance.getlever == 2)
            waterfall2 = true;
        if (ObjectsCount.instance.getlever == 3)
            waterfall3 = true;
        if (ObjectsCount.instance.getlever == 4)
            waterfall4 = true;

        if (startAnimation)
        {
            if (waterFloor2 != null)
            {
                waterFloor2.transform.position += Vector3.down * Time.deltaTime * 2;
            }

            print("particles stop");

            waterfall1_p1.Stop();
            waterfall1_p2.Stop();
            waterfall1_p3.Stop();
            waterfall1_p4.Stop();

            waterfallAudio.volume -= Time.deltaTime;
            volume -= 0.00005f;
        }

        if (waterFloor2.transform.position.y <= -38)
        {
            waterOn = false;
        }else
            waterOn = true;

        if (waterfall1)
        {
            waterfall1_p1.Stop();
            waterfall1_p2.Stop();
            waterfall1_p3.Stop();
            waterfall1_p4.Stop();
        }
        if (waterfall2)
        {
            waterfall2_p1.Stop();
            waterfall2_p2.Stop();
            waterfall2_p3.Stop();
            waterfall2_p4.Stop();
        }
        if (waterfall3)
        {
            waterfall3_p1.Stop();
            waterfall3_p2.Stop();
            waterfall3_p3.Stop();
            waterfall3_p4.Stop();
        }
        if (waterfall4)
        {
            waterfall4_p1.Stop();
            waterfall4_p2.Stop();
            waterfall4_p3.Stop();
            waterfall4_p4.Stop();
        }

        if (onPause == false)
        {
            if (!startAnimation)
            {
                if (!waterfall1)
                {
                    waterfall1_p1.Play();
                    waterfall1_p2.Play();
                    waterfall1_p3.Play();
                    waterfall1_p4.Play();
                }
                if (!waterfall2)
                {
                    waterfall2_p1.Play();
                    waterfall2_p2.Play();
                    waterfall2_p3.Play();
                    waterfall2_p4.Play();
                }
                if (!waterfall3)
                {
                    waterfall3_p1.Play();
                    waterfall3_p2.Play();
                    waterfall3_p3.Play();
                    waterfall3_p4.Play();
                }
                if (!waterfall4)
                {
                    waterfall4_p1.Play();
                    waterfall4_p2.Play();
                    waterfall4_p3.Play();
                    waterfall4_p4.Play();
                }
            }
        }
        else
        {           
            waterfall1_p1.Pause();
            waterfall1_p2.Pause();
            waterfall1_p3.Pause();
            waterfall1_p4.Pause();

            waterfall2_p1.Pause();
            waterfall2_p2.Pause();
            waterfall2_p3.Pause();
            waterfall2_p4.Pause();

            waterfall3_p1.Pause();
            waterfall3_p2.Pause();
            waterfall3_p3.Pause();
            waterfall3_p4.Pause();

            waterfall4_p1.Pause();
            waterfall4_p2.Pause();
            waterfall4_p3.Pause();
            waterfall4_p4.Pause();
        }

	}

    public void OnPause(bool isOnPause)
    {
        onPause = isOnPause;
    }
}
