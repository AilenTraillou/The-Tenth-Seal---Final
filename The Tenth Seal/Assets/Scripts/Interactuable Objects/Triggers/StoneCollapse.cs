using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCollapse : MonoBehaviour {

    public GameObject stones;
    bool stop;


	void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<Character>() && !stop)
        {
            stones.SetActive(true);
            SoundsManager.instancia.Play(1, 0.7f, false);
            stop = true;
        }
            
	}
}
