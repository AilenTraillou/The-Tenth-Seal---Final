using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour {

    public float degrees;
    public float radians;
    public float radiusX;
    public float radiusY;
    public float speedRotation;

    void Start () {
		
	}
	
	void Update () {

        degrees += speedRotation * Time.deltaTime;
        radians = degrees * Mathf.Deg2Rad;

        Vector3 posInCircle = transform.position;
        posInCircle.x = (Mathf.Cos(radians) * radiusX);
        posInCircle.y = (Mathf.Sin(radians) * radiusY);

        transform.position = posInCircle;
    }
}
