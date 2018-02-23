using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public event Action<bool> OnPause = delegate { };

    public GameObject exitScreen;
    public GameObject optionsScreen;
    public GameObject confirmQuitGameScreen;
    public GameObject confirmBackToMenuScreen;

    public Text resumeGame;
    public Text backToMenu;
    public Text options;
    public Text exitGame;

    public Text aceptQuitGame;
    public Text cancelQuitGame;
    public Text aceptBackToMenu;
    public Text cancelBackToMenu;
    public Text goBack;

    public Button menu;

    public bool onPause;

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
        chair = FindObjectOfType<Chair>();
        levelManager = FindObjectOfType<LevelManager>();
        followMouse.AddRange(FindObjectsOfType<FollowMouse>());
        waterParticles = FindObjectOfType<WaterAndWaterfall>();
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

        exitScreen.SetActive(false);
        optionsScreen.SetActive(false);
        confirmQuitGameScreen.SetActive(false);
        confirmBackToMenuScreen.SetActive(false);

        resumeGame.GetComponent<Button>().onClick.AddListener(GetPause);
        options.GetComponent<Button>().onClick.AddListener(GetOptions);
        goBack.GetComponent<Button>().onClick.AddListener(GetExitScreen);
        exitGame.GetComponent<Button>().onClick.AddListener(QuitGame);
        aceptQuitGame.GetComponent<Button>().onClick.AddListener(AceptQuitGame);
        cancelQuitGame.GetComponent<Button>().onClick.AddListener(CancelQuitGame);
        aceptBackToMenu.GetComponent<Button>().onClick.AddListener(AceptBackToMenu);
        cancelBackToMenu.GetComponent<Button>().onClick.AddListener(CancelBackToMenu);
        backToMenu.GetComponent<Button>().onClick.AddListener(BackToMenu);
        menu.onClick.AddListener(GetExitScreen);
    }

    private void BackToMenu()
    {
        exitScreen.SetActive(false);
        confirmBackToMenuScreen.SetActive(true);
    }

    private void CancelBackToMenu()
    {
        confirmBackToMenuScreen.SetActive(false);
        exitScreen.SetActive(true);
    }

    private void AceptBackToMenu()
    {
        confirmBackToMenuScreen.SetActive(false);
        ChangeScene(Scenes.MAIN_MENU);        
    }

    private void CancelQuitGame()
    {
        confirmQuitGameScreen.SetActive(false);
        exitScreen.SetActive(true);
    }

    private void AceptQuitGame()
    {
        Application.Quit();
    }

    private void QuitGame()
    {
        exitScreen.SetActive(false);
        confirmQuitGameScreen.SetActive(true);
    }

    private void GetExitScreen()
    {
        exitScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }

    private void GetPause()
    {
        OnPause(false);
        onPause = false;
        exitScreen.SetActive(false);
    }

    private void GetOptions()
    {
        exitScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void OnExit()
    {
        if (exitScreen.activeSelf)
        {
            OnPause(false);
            onPause = false;
            exitScreen.SetActive(false);
        }
        else
        {
            OnPause(true);
            onPause = true;
            exitScreen.SetActive(true);
        }
    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    void Update()
    {
        if(exitScreen.activeSelf || optionsScreen.activeSelf 
            || confirmQuitGameScreen.activeSelf || confirmBackToMenuScreen.activeSelf)
        {
            onPause = true;
        }else
        {
            onPause = false;
        }   
    }

}

public class Scenes
{
    public const int MAIN_MENU = 1;
    public const int HOUSE = 2;
    public const int HOSPITAL = 3;
    public const int GRAVEYARD = 4;
}
