using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsCount : MonoBehaviour {

    public static ObjectsCount instance;

    public int getlever;
    public int gems;
    public int mana;
    public int totalManaFound;
    public int lifeRecovered;
    public float totalOilCharged;
    public int itemsOnLevel1 = 5;
    public int totalItemsOnLevelFound;
    public float damageTaken;
    public bool getRedSubstances;
    public int redJarRemaining = 5;
    

    void Start ()
    {
        instance = this;
	}
}
