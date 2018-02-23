using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsCount : MonoBehaviour {

    public static ObjectsCount instance;

    public int getlever;
    public int gems;
    public int mana;
    public int totalManaFound;

    void Start ()
    {
        instance = this;
	}
}
