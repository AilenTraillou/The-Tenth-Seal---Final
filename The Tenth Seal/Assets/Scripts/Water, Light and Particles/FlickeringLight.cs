using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour, IOnPause
{
    Light myLight;
    float currentFloat;
    bool onPause;

	void Awake ()
    {
        myLight = GetComponent<Light>();
        StartCoroutine(LightOnOff());
	}

    public void OnPause(bool isOnPause)
    {
        onPause = isOnPause;
        if(onPause == false)
        {
            StartCoroutine(LightOnOff());
        }
    }

    IEnumerator LightOnOff()
    {
        if(onPause == false)
        {
            yield return new WaitForSeconds(currentFloat);
            myLight.enabled = false;
            yield return new WaitForSeconds(currentFloat);
            myLight.enabled = true;
            currentFloat = Random.Range(0.1f, 0.4f);
            StartCoroutine(LightOnOff());
        }
    }
	
}
