using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour {

    public float distance;
    public float smooth;
    
    GameObject _camera;
    GameObject carriedObject;
    bool carrying;
    bool touched;

    void Start () {

        _camera = Camera.main.gameObject;
	}
	
	void Update () {

        if (carrying)
        {
            Carry(carriedObject);
            CheckDrop();
        }
        else
            PickUp();
	}

    void Carry(GameObject _gameObject)
    {    
        _gameObject.transform.position = Vector3.Lerp(_gameObject.transform.position, _camera.transform.position + 
            _camera.transform.forward * distance, Time.deltaTime * smooth);
    }

    void PickUp()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = _camera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                PickableObject p = hit.collider.GetComponent<PickableObject>();
                if(p != null)
                {
                    if(p.gameObject.tag != "Door")
                    {
                        carrying = true;
                        carriedObject = p.gameObject;                
                        p.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
            }       
        }      
    }

    void CheckDrop()
    {
        if (Input.GetMouseButtonUp(0))
        {
            DropObject();
        }
    }

    void DropObject()
    {
        carrying = false;
        carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject = null;
    }

    void OnTriggerEnter(Collider c)
    {
        if (carrying)
            touched = true;
    }
}
