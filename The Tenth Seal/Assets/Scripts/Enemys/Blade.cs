using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour {

	void Start()
    {
		    
	}
	
	void Update()
    {
        transform.Rotate(Vector3.up * 1000 * Time.deltaTime, Space.Self);
    }
}
