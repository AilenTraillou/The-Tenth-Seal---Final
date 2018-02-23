using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farol : MonoBehaviour {

    public Animator farol;
    private float _speedAnimation;

    private int _counter;
    private int _limitCounter;
    private bool _startAnimation;

	// Use this for initialization
	void Start () {


    }
	
	// Update is called once per frame
	void Update () {

        _limitCounter = Random.Range(0, 20);
        RandomCounter(_limitCounter);

	}

    void RandomCounter(int limitRange)
    {
        

        if (_counter < _limitCounter)
        {
            farol.speed = 0;
            _counter++;

        }else
        {
            farol.speed = 5;
            farol.Play("Farol_iluminacion");
            _counter = 0;
        }
        
        

    }
}
