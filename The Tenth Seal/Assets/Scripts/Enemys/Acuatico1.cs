using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acuatico1 : MonoBehaviour {

    public StreiAI ai;
    public GameObject ph;
    private float _distance;

	
	void Update () {

        var distance = (ph.transform.position - this.transform.position).magnitude;
        _distance = distance;

        if (WaterAndWaterfall.instance.startAnimation)
        {
            ai.enabled = false;

            transform.rotation = Quaternion.Slerp(this.transform.rotation, 
            Quaternion.LookRotation(ph.transform.position - this.transform.position), 5f * Time.deltaTime);

            if(_distance > 2)
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                transform.position += transform.forward * 25 * Time.deltaTime;
            }else
            {
                transform.position += Vector3.down * 25 * Time.deltaTime;
            }

            if (_distance < 5) Destroy(gameObject);
        }
	}
}
