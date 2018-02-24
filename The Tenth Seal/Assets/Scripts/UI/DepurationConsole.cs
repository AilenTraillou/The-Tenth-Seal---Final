using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepurationConsole : MonoBehaviour {

    public event Action<bool> OnPause = delegate { };

    public static DepurationConsole _depConsoleInstance;
    public delegate void depurationFunction();
    depurationFunction _depurationFunction;
    Dictionary<string, depurationFunction> commandList = new Dictionary<string, depurationFunction>();

    public Text log;
    public InputField commandToWrite;
    public Scrollbar scroll;
    public GameObject console;
    public GameObject cheats;
    public bool isConsoleActive;
    int auxMana;

    ModelCharacter model;
    ControllerCharacter controller;
    ViewCharacter view;
    WaterAndWaterfall waterParticles;
    Chair chair;
    LevelManager levelManager;
    List<FollowMouse> followMouse = new List<FollowMouse>();
    List<FlickeringLight> lights = new List<FlickeringLight>();
    List<SpikeWall> spikeWall = new List<SpikeWall>();
    List<StreiAI> enemies = new List<StreiAI>();

    void Start() {

        model = FindObjectOfType<ModelCharacter>();
        controller = FindObjectOfType<ControllerCharacter>();
        view = FindObjectOfType<ViewCharacter>();
        waterParticles = FindObjectOfType<WaterAndWaterfall>();
        chair = FindObjectOfType<Chair>();
        levelManager = FindObjectOfType<LevelManager>();
        followMouse.AddRange(FindObjectsOfType<FollowMouse>());
        lights.AddRange(FindObjectsOfType<FlickeringLight>());
        spikeWall.AddRange(FindObjectsOfType<SpikeWall>());
        enemies.AddRange(FindObjectsOfType<StreiAI>());


        foreach (var _object in followMouse)
        {
            OnPause += _object.OnPause;
        }
        foreach (var _object in lights)
        {
            OnPause += _object.OnPause;
        }
        foreach (var _object in spikeWall)
        {
            OnPause += _object.OnPause;
        }
        foreach (var _object in enemies)
        {
            OnPause += _object.OnPause;
        }
        OnPause += model.OnPause;
        OnPause += controller.OnPause;
        OnPause += view.OnPause;
        OnPause += levelManager.OnPause;

        if (chair != null)
            OnPause += chair.OnPause;
        if(waterParticles != null)
            OnPause += waterParticles.OnPause;

        if (_depConsoleInstance == null)
            _depConsoleInstance = this;
        else
            Destroy(this);

        _depurationFunction = Start;

        GetCommand("get all gems", GetAllGems);
        GetCommand("get all keys", GetAllKeys);
        GetCommand("infinite life = on", GetInfiniteLife);
        GetCommand("infinite life = off", GetInfiniteLife);
        GetCommand("infinite mana = on", GetInfiniteMana);
        GetCommand("infinite mana = off", GetInfiniteMana);
        GetCommand("infinite oil = on", GetInfiniteOil);
        GetCommand("infinite oil = off", GetInfiniteOil);
        GetCommand("light = on", GetIllumination);
        GetCommand("light = off", GetIllumination);


        cheats.SetActive(false);
    }

    public void GetCommand(string commandName, depurationFunction depFunction)
    {
        commandList[commandName] = depFunction;
    }

    void Update() {

        scroll.value = 0;
        string getCommand = commandToWrite.text;

        if(console.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            if (commandList.ContainsKey(getCommand))
                commandList[getCommand].Invoke();
            else
                Write("The command doesn't exist. Please try again.");

            commandToWrite.text = "";
        }

        //if (isConsoleActive == false)
        //{
        //    OnPause(false);
        //}
        //else
        //    OnPause(true);


    }

    public void ActiveDepurationConsole()
    {
        if (console.activeSelf)
        {
            console.SetActive(false);
            isConsoleActive = false;
            OnPause(false);
        }
        else
        {
            OnPause(true);
            console.SetActive(true);
            isConsoleActive = true;
        }
    }

    void Write(string textToWrite)
    {
        log.text += textToWrite + "\n";
    }

    void GetAllKeys()
    {
        Write("Command succesfull.");
        List<Door> _doorList = new List<Door>();
        _doorList.AddRange(FindObjectsOfType<Door>());
        foreach (var door in _doorList)
        {
            door.getKey = true;
        }
    }

    void GetAllGems()
    {
        Write("Command succesfull.");
        ObjectsCount.instance.gems = 3;
    }

    void GetInfiniteLife()
    {
        Write("Command succesfull."); 

        if(commandToWrite.text == "infinite_life = on")
            FindObjectOfType<ModelCharacter>().lifeToRecover = 100;
        if(commandToWrite.text == "infinite_life = off")
            FindObjectOfType<ModelCharacter>().lifeToRecover = 0.0001f;
    }

    void GetInfiniteMana()
    {
        Write("Command succesfull.");

        
        if (commandToWrite.text == "infinite_mana = on")
        {
            auxMana = ObjectsCount.instance.mana;
            ObjectsCount.instance.mana = 999;
        }
        if (commandToWrite.text == "infinite_mana = off")
        {
            ObjectsCount.instance.mana = auxMana;
        }
    }

    void GetInfiniteOil()
    {
        Write("Command succesfull.");
        if (commandToWrite.text == "infinite_oil = on")
            FindObjectOfType<ModelCharacter>().oilToRecover = 100;
        if (commandToWrite.text == "infinite_oil = off")
            FindObjectOfType<ModelCharacter>().oilToRecover = 0f;

    }

    void GetIllumination()
    {
        Write("Command succesfull.");
        List<Light> _light = new List<Light>();
        _light.AddRange(FindObjectsOfType<Light>());
        foreach (var light in _light)
        {        
            if (light.name == "DirectionalGlobalLight")
            {
                if (commandToWrite.text == "light = on")
                    light.enabled = true;
                if (commandToWrite.text == "light = off")
                    light.enabled = false;
            }
        }
        _light.Clear();
    }


}
