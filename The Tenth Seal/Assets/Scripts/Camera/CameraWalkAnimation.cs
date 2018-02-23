using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWalkAnimation : MonoBehaviour
{


    public float movementDistance;
    public float animSpeed;
    private float upLimit;
    private float downLimit;
    public int dir = 1;
    public bool walk;

    void Start ()
    {
        upLimit = transform.localPosition.y + movementDistance;
        downLimit = transform.localPosition.y - movementDistance;

    }

	void Update ()
    {
        if (walk)
        {
            transform.localPosition += Vector3.up * animSpeed * Time.deltaTime * dir;

            if (transform.localPosition.y > upLimit) dir = -1;
            else if (transform.localPosition.y < downLimit) dir = 1;

        }
	}
}
