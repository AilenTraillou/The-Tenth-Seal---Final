using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepConsoleCheats : MonoBehaviour {

    public GameObject cheats;

    public void GetCheats()
    {
        cheats.SetActive(!cheats.activeSelf);
    }
}
