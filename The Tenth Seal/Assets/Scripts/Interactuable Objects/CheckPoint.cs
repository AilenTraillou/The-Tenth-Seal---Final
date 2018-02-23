using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour, ICheckPointObserver {

    public Transform begin;
    Transform characterPosition;
    Character _character;

    void Start ()
    {
        _character = FindObjectOfType<Character>();	
	}

    public void Notify(Transform player)
    {
        characterPosition = player;
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Respawn();
	}

    public void Respawn()
    {
        if(characterPosition != null)
            _character.transform.position = characterPosition.position;
        else
            _character.transform.position = begin.position;
    }
}
