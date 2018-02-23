using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidWater : MonoBehaviour {

    public float damage = 0.00000001f;
    public bool touchingWater;

    //void OnTriggerEnter(Collider c)
    //{
    //    if (c.GetComponent<Character>() && c.GetType() == typeof(CapsuleCollider))
    //        touchingWater = true;
    //    if (c.GetComponent<Character>() && c.GetType() == typeof(SphereCollider) && c.GetType()
    //        != typeof(CapsuleCollider))
    //        touchingWater = false;
    //}
}
