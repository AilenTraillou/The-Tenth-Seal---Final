using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessegeController : MonoBehaviour {

  
    public Text messege;
    public Image dialogBox;
    [HideInInspector]
    public string messageToWrite;

    public static MessegeController instance;

    void Awake () {

        instance = this;
        messege.enabled = false;
        dialogBox.enabled = false;
	}
	
    public void OpenDialog(string messegeContent)
    {  
        messege.enabled = true;
        dialogBox.enabled = true;
        messege.text = messegeContent;
        Invoke("CloseDialog", 2f);
    }

    public void CloseDialog()
    {
        messege.enabled = false;
        dialogBox.enabled = false; 
    }

}

public class MessageDictionary
{
    public const string GET_KEY = "Key taken";
    public const string CLOSE_DOOR = "Is closed";
    public const string GET_SUBSTANCE = "Substance taken";
    public const string GET_OIL = "Oil taken";
    public const string GET_GEMS = "Gem taken";
    public const string CANNOT_GET_OIL = "It looks like there is no more oil...";
    public const string CANNOT_GET_CHECKPOINT = "Haven't enough substance to invoke the spell...";
    public const string SPELL_DONE = "The spell is done.";
}
