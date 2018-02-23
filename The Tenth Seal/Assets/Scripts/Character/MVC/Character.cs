using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public ModelCharacter model;
    public ViewCharacter view;
    ControllerCharacter controller;
    
	void Awake () {
		
        controller = new ControllerCharacter(model, view);
	}

}
