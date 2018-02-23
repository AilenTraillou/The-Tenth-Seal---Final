using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileKeyboard : MonoBehaviour {

    public InputField inputFiled;
    TouchScreenKeyboard keyboard;

	void Start () {
		
	}
	
	void Update () {

        if (inputFiled.isFocused && Application.platform == RuntimePlatform.Android)
        {
            TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        }
	}

    void OnGUI()
    {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        if (inputFiled.isFocused && Application.platform == RuntimePlatform.Android)
        {
            TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            inputFiled.text = keyboard.text;
        }
    }
}
