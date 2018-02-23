using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWall : ObservableInterface, IObserver, IOnPause {

    IObservable observableList;
    bool activateSpikeWalls;
    float distance = 16.2f;
    float initialPostition;
    bool changeDirection;
    public bool havePartner;
    public int timeToStart;
    float speed = 5f;
    float auxSpeed;
    bool onPause;

    public void Notify(GameObject _object)
    {
        if (!havePartner)
        {
            var random = UnityEngine.Random.Range(0, 5);
            Invoke("ChangePosition", random);
        }
        else
        {
            Invoke("ChangePosition", timeToStart);
        }
        
    }

    void Awake()
    {
        initialPostition = transform.position.x;
        auxSpeed = speed;
    }

    void Start() {

        observableList = FindObjectOfType<ActivateSpikeWall>();
        observableList.Suscribe(this);
	}
	
	void Update () {
		
        if (activateSpikeWalls)
        {
            ActivateSpikeWall();
        }

        if (onPause == false)
            speed = auxSpeed;
        else
            speed = 0;
	}

    void ActivateSpikeWall()
    {
        if(gameObject.name == "Spike Wall left")
        {
            if (transform.position.x > initialPostition + distance)
            {
                changeDirection = true;
            }

            if(transform.position.x <= initialPostition)
            {
                changeDirection = false;
                
            }

            if(changeDirection)
                transform.position -= Vector3.right * Time.deltaTime * speed;
            else
                transform.position += Vector3.right * Time.deltaTime * speed;
        }

        if (gameObject.name == "Spike Wall right")
        {
            if (transform.position.x < initialPostition - distance)
            {
                changeDirection = true;
            }

            if (transform.position.x >= initialPostition)
            {
                changeDirection = false;

            }

            if (changeDirection)
                transform.position += Vector3.right * Time.deltaTime * speed;
            else
                transform.position -= Vector3.right * Time.deltaTime * speed;
        }
    }

    void ChangePosition()
    {
        activateSpikeWalls = true;
    }

    public void OnPause(bool isOnPause)
    {
        onPause = isOnPause;
    }
}
