using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeGhost : MonoBehaviour {

    public GameObject ghost;

    public void Ghost()
    {
        ghost.SetActive(true);
    }
}
