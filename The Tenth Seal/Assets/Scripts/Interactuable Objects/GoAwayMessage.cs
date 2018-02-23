using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoAwayMessage : MonoBehaviour {

    public Image message;
    public bool activateMessage;

	void Start ()
    {
        message.fillAmount = 0;	
	}
	
	void Update ()
    {
        if (activateMessage)
            message.fillAmount += 0.01f;	
	}
}
