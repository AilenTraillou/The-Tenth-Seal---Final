using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchCamera : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Canvas canvas;
    float radio = 25;
    public Vector2 _Axis;

    public Vector2 _axis
    {
        get
        {
            return _axis;
        }
    }
    public float Horizontal
    {
        get
        {
            return _Axis.x;
        }
    }
    public float Vertical
    {
        get
        {
            return _Axis.y;
        }
    }

    public bool touchActive;

    public Vector3 padDirection { set; get; }
    Vector3 initialPosition;

    public void Start()
    {
        initialPosition = transform.position;
    }
    public void OnDrag(PointerEventData pointer)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, pointer.position,
                FindObjectOfType<Canvas>().worldCamera, out position);

        Vector2 newPosition = canvas.transform.TransformPoint(position) - initialPosition;
        newPosition.x = Mathf.Clamp(newPosition.x, -radio, radio);
        newPosition.y = Mathf.Clamp(newPosition.y, -radio, radio);

        _Axis = newPosition / radio;     
    }

    public void OnEndDrag(PointerEventData pointer)
    {
        _Axis = Vector2.zero;
    }
 
}
