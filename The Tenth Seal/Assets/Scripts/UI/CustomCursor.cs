using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour, IObserver {

    public List<IObservable> interactuableObjects = new List<IObservable>();
    public Image interactuableCursor;
    public Image pickupCursorImage;

    bool pickUpCursor;

    public void Notify(GameObject _object)
    {
        if (interactuableCursor != null)
        interactuableCursor.enabled = !interactuableCursor.enabled;
    }

    void Start() {

        interactuableCursor.enabled = false;
        interactuableObjects.AddRange(FindObjectsOfType<InteractionCursor>());

        foreach (var item in interactuableObjects)
        {
            item.Suscribe(this);
        }
	}

    void Update()
    {
        if (pickUpCursor)
            pickupCursorImage.enabled = true;
        else
            pickupCursorImage.enabled = false;
    }

    public void PickUpObject(bool pickingObject)
    {
        pickUpCursor = pickingObject;
    }
}
