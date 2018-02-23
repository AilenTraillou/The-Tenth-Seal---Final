using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowMouse : MonoBehaviour, IOnPause
{
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    public bool rotateX;
    public bool rotateY;

    public bool onDraggDoor;
    public bool onPause;
    public bool onMobileDevice;

    DepurationConsole _depConsole;
    GameManager gameManager;
    Scene currentScene;
    ControllerCharacter controller;

    void Start()
    {
        controller = FindObjectOfType<ControllerCharacter>();
        controller.OnMobileDevice += OnPC;
        currentScene = SceneManager.GetActiveScene();
        gameManager = FindObjectOfType<GameManager>();
        _depConsole = FindObjectOfType<DepurationConsole>();
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void Update()
    {
        if (onMobileDevice == false)
        {
            if(currentScene.name != "Menu")
            {
                if (onPause == false)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    if(onDraggDoor == false)
                        MouseRotation();
                }
                if(onPause)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
            }     
        }else
            Cursor.lockState = CursorLockMode.None;

        print(onDraggDoor);


    }

    public void OnPause(bool isOnPause)
    {
        onPause = isOnPause;
    }

    public void OnPC(bool _onPC)
    {
        onMobileDevice = _onPC;
    }

    public void OnDraggDoor(bool draggDoor)
    {
        onDraggDoor = draggDoor;
    }

    void MouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        if (rotateY) rotY += mouseX * mouseSensitivity * Time.deltaTime;
        else rotY = transform.eulerAngles.y;

        if (rotateX) rotX += mouseY * mouseSensitivity * Time.deltaTime;
        else rotX = transform.eulerAngles.x;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }


    public void TouchRotation(float hor, float ver, GameObject character)
    {
        float mouseX = hor;
        float mouseY = -ver;

        rotY += mouseX * mouseSensitivity * Time.deltaTime * 0.7f;
        rotX += mouseY * mouseSensitivity * Time.deltaTime * 0.7f;
        
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(0, rotY, 0.0f);
        character.transform.rotation = localRotation;

    }

}
