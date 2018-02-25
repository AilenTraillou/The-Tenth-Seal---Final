using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeMovement : Enemy {

    public float degrees;
    public float radians;
    public float radiusX;
    public float radiusY;
    public float speedRotation;

    public GameObject prefabBullet;
    public Transform center;

    public int numBullets;
    public int invokeTime;

    public float a;
    public float b;
    public float h;
    bool invokeBlade;

    void Start()
    {
        Invoke("InvokeBlade", invokeTime);

        if (gameObject.tag == "fullBlade" || gameObject.tag == "verticalBlade")
            damage = 10;
        else
            damage = 0;
    }

    void Update () {

        if (invokeBlade)
        {
            degrees += speedRotation * Time.deltaTime;
            radians = degrees * Mathf.Deg2Rad;

            if (gameObject.tag == "fullBlade")
            {

                Vector3 posInEpitrocoide = transform.position;
                posInEpitrocoide.x = posInEpitrocoide.x + ((a - b) * Mathf.Cos(radians) - h * Mathf.Cos(((a + b) / b) * radians ));
                posInEpitrocoide.y = posInEpitrocoide.y + ((a - b) * Mathf.Sin(radians) - h * Mathf.Sin(((a + b) / b) * radians));

                transform.position = posInEpitrocoide;
            }

            if(gameObject.tag == "verticalBlade")
            {
                Vector3 posInEpitrocoide = transform.position;
                posInEpitrocoide.y = posInEpitrocoide.y + ((a - b) * Mathf.Sin(radians) - h * Mathf.Sin(((a + b) / b) * radians));

                transform.position = posInEpitrocoide;
            }
        }
    }

    void InvokeBlade()
    {
        invokeBlade = true;
    }
}
