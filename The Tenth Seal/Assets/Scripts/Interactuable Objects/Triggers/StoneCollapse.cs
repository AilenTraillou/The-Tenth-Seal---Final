using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCollapse : MonoBehaviour {

    public GameObject stones;

	void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<Character>())
            stones.SetActive(true);
	}
}
