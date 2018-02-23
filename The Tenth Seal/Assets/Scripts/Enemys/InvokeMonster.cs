using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeMonster : MonoBehaviour {

    public GameObject monster;

    void Start()
    {
        monster.SetActive(false);
    }

	public void InvokeSkeletonMonster()
    {
        monster.SetActive(true);
	}
}
