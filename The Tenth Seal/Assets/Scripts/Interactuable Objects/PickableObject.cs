using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour {

    public event Action<bool> pickUpCursor = delegate { };
    CustomCursor customCursor;

    void Start ()
    {
        customCursor = FindObjectOfType<CustomCursor>();
        pickUpCursor += customCursor.PickUpObject;
    }
	
	void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<Character>())
            pickUpCursor(true);
	}

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.GetComponent<Character>())
            pickUpCursor(false);
    }
}
