using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.GetComponent<Character>())
        {
            Transform character = c.gameObject.transform;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(character.position - transform.position),
                5 * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }
}
