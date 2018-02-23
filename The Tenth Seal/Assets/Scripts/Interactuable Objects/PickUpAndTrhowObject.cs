using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndTrhowObject : MonoBehaviour {

    public Transform player;
    public Transform _camera;
    public float _throwForce = 10;
    bool hasPlayer = false;
    bool beingCarried = false;
    public AudioClip[] soundsToPlay;
    private AudioSource audio;
    public int dmg;
    private bool touched = false;

	void Start ()
    {
        audio = GetComponent<AudioSource>();
	}
	
	void Update ()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 10f)
            hasPlayer = true;
        else
            hasPlayer = false;

        if(hasPlayer && Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = _camera;
            beingCarried = true;
        }
        if (beingCarried)
        {
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(_camera.forward * _throwForce);
                RandomAudio();
            }
            else
            {
                if (Input.GetMouseButtonDown(1))
                {
                    GetComponent<Rigidbody>().isKinematic = false;
                    transform.parent = null;
                    beingCarried = false;
                }
            }
        }
	}

    void RandomAudio()
    {
        
    }

    void OnTriggerEnter()
    {
        if (beingCarried)
            touched = true;
    }
}
