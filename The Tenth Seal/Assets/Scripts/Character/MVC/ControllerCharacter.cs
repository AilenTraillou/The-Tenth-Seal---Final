using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControllerCharacter : MonoBehaviour, IOnPause {

    public event Action<bool> OnMobileDevice = delegate { };

    ModelCharacter model;
    public ViewCharacter view;
    public TouchController characterTouchController;
    public TouchController cameraTouchController;
    public FollowMouse touchCamera;
    public Button interact;
    public Button jump;
    public GameObject mobileUI;

    DepurationConsole depConsole;
    GameManager gameManager;
    Ads ads;
    bool onPause;

    bool isOnPC;
    bool isOnAndroid;

    public ControllerCharacter(ModelCharacter m, ViewCharacter v)
    {
        model = m;
        view = v;

        view.GetLife(model.life, 0);
        view.GetOil(model.oil, false);

        model.GetLife += view.GetLife;
        model.TakeOil += view.GetOil;
        model.DestroyGameObject += view.DestroyObject;
        model.ManaManage += view.ManageCheckpoint;
        model.OnWalk += view.Walking;
        model.OnIdle += view.Idle;
        model.OnJump += view.Jumping;
        model.TakeKey += view.GetKey;
        model.InteractCursor += view.GetCursor;
        model.AcidBurn += view.AcidBurn;
        model.ConsumeOil += view.LightOnOff;
    }

    void Awake()
    {
        ///We verify which platform is used to run our game.
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            isOnPC = true;
            isOnAndroid = false;
            mobileUI.SetActive(false);

        }
        if (Application.platform == RuntimePlatform.Android)
        {
            isOnPC = false;
            isOnAndroid = true;
            mobileUI.SetActive(true);
        }
    }

    void Start()
    {        
        model = GetComponent<ModelCharacter>();
        depConsole = FindObjectOfType<DepurationConsole>();
        gameManager = FindObjectOfType<GameManager>();
        ads = FindObjectOfType<Ads>();
        //view.OnDeath += ads.OnDeath;

        interact.onClick.AddListener(model.Interact);
        jump.onClick.AddListener(model.Jump);
    }
   
    void Update()
    {
        
        if(isOnPC)
        {
            GetPC_UIInputs();
            if(onPause == false)   
                GetPCCharacterInputs();
            OnMobileDevice(false);
        }
        if (isOnAndroid)
        {
            GetTouchInputs();
            OnMobileDevice(true);
        }
        
    }

    public void OnPause(bool isOnPause)
    {
        onPause = isOnPause;
    }

    void GetPC_UIInputs()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && depConsole.isConsoleActive == false)
        {
            gameManager.OnExit();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && gameManager.exitScreen.activeSelf == false)
        {
            depConsole.ActiveDepurationConsole();
        }
    }
    
    void GetPCCharacterInputs()
    {
        
        if (Input.GetButtonDown("Jump") && model.isJumping == false)
        {
            model.Jump();
        }
        else
        {
            if (Input.GetAxis("Vertical") != 0 && Input.GetKey(KeyCode.LeftShift))
            {
                model.Run(false, Input.GetAxis("Vertical"));
            }

            if (Input.GetAxis("Horizontal") != 0 && Input.GetKey(KeyCode.LeftShift))
            {
                model.Run(true, Input.GetAxis("Horizontal"));
            }

            if (model.isRuning == false || Input.GetKey(KeyCode.LeftShift) == false)
            {
                if (Input.GetAxis("Vertical") != 0)
                {
                    model.Walk(false, Input.GetAxis("Vertical"));
                }

                if (Input.GetAxis("Horizontal") != 0)
                {
                    model.Walk(true, Input.GetAxis("Horizontal"));
                }
            }
        

        }

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            model.Idle();

        if (Input.GetMouseButtonUp(0))
        {
            model.Interact();
        }

        if (Input.GetMouseButtonUp(1))
        {
            model.ConsumeMana();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            model.consumeOil = !model.consumeOil;
        }

    }

    void GetTouchInputs()
    {
        characterTouchController.touchActive = true;

        if (characterTouchController.Horizontal != 0 || characterTouchController.Vertical != 0)
            model.WalkWithJoystick(characterTouchController.Horizontal, characterTouchController.Vertical);
        else
            model.Idle();
        touchCamera.TouchRotation(cameraTouchController.Horizontal, cameraTouchController.Vertical, gameObject);
           
    }
   
}
