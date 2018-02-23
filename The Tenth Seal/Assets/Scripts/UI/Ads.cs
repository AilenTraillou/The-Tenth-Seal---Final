using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour {

    void OnDeath()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
            print("asasasasasasasasasasa adssssss");
        }
    }
}
