using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour, IObserver {


    float life = 3;
    public IObservable lightBlinderObservable;
    bool RestLifeisactive = false;

    public Material enemyMaterial;
    bool changeAlpha = false;


    void Awake ()
    {
        lightBlinderObservable = FindObjectOfType<LightBlinder>();
        lightBlinderObservable.Suscribe(this);

    }
	
	void Update () {

        if (RestLifeisactive)
        {
            life -= 0.01f;
        }

        if (life <= 0)
        {
            Destroy(gameObject);
        }

        if (changeAlpha)
        {
            var auxAlpha = enemyMaterial.color.a;
            auxAlpha--;
            ChangeAlpha((byte)auxAlpha);

        }

    }

    public void Notify(GameObject _object)
    {
        RestLifeisactive = !RestLifeisactive;
        changeAlpha = !changeAlpha;
    }


    public void ChangeAlpha(byte newcolor)
    {
        Color newAlpha = new Color32((byte)enemyMaterial.color.r, 
                        (byte)enemyMaterial.color.g, (byte)enemyMaterial.color.b, (byte)enemyMaterial.color.a);
        enemyMaterial.color = newAlpha;

    }
}
