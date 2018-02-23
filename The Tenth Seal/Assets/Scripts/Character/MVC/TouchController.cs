using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchController : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public Image joystick;
    public Image joystickPad;
    float radio = 25;
    public Vector2 Axis;

    public Vector2 axis
    {
        get
        {
            return axis;
        }
    }
    public float Horizontal
    {
        get
        {
            return Axis.x;
        }
    }
    public float Vertical
    {
        get
        {
            return Axis.y;
        }
    }


    public bool touchActive;
    Vector3 initialPosition;    

    public void Start()
    {
        initialPosition = transform.position;
    }
    public void OnDrag(PointerEventData pointer)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystick.transform as RectTransform, pointer.position,
                FindObjectOfType<Canvas>().worldCamera, out position);

        Vector2 newPosition = joystick.transform.TransformPoint(position) - initialPosition;
        newPosition.x = Mathf.Clamp(newPosition.x, -radio, radio);
        newPosition.y = Mathf.Clamp(newPosition.y, -radio, radio);

        Axis = newPosition / radio;

        transform.localPosition = newPosition;   
    }

    public void OnEndDrag(PointerEventData pointer)
    {
        transform.position = initialPosition;
        Axis = Vector2.zero;
    }
}
