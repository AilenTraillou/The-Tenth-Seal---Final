using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarcoSangre : MonoBehaviour {

    public Image marco;
    private byte alpha = 0;

	void Update () {

        alpha = (byte)((MainPlayer.instance.fear * 255) / MainPlayer.instance.maxFear);
        ChangeAlpha(alpha);

	}


    public void ChangeAlpha(byte newcolor)
    {

        Color newAlpha = new Color32((byte)marco.color.r, (byte)marco.color.g, (byte)marco.color.b, newcolor);

        marco.color = newAlpha;

    }
}
