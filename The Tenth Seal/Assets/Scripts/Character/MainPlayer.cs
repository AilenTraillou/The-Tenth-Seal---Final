using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class MainPlayer : MonoBehaviour
{
    //variable del miedo
    public float fear;
    // miedo maximo
    public float maxFear;
    // factor que hacer que pierda x miedo por segundo
    public float relaxFactor;

    //blur del efecto de la camara
    public MotionBlur blur;

    public static MainPlayer instance;

	// Use this for initialization
	void Start ()
    {
        //obtengo el efecto de la camara
        blur = Camera.main.GetComponent<MotionBlur>();

        //cantidad de blur, valores entre 0 y 1
        blur.blurAmount = 0;

        instance = this;
    }
	
    //sumo miedo
    public void AddFear(float amount)
    {
        
        fear += amount;
        fear = Mathf.Clamp(fear, 0, maxFear);
    }
    //saca miedo por segundo
    public void ClearFear()
    {
        fear -= relaxFactor * Time.deltaTime;
        fear = Mathf.Clamp(fear, 0, maxFear);
    }


    // Update is called once per frame
    void Update ()
    {
        ClearFear();
        blur.blurAmount = fear / maxFear;

        //if(Input.GetKeyDown(KeyCode.T))
        //{
        //    AddFear(50);
        //}
 

    }
}
