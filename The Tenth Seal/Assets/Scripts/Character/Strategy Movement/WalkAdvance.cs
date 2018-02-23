using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAdvance : IAdvance {

    float _speed;
    Transform _transform;
    Rigidbody _rb;

    public void Advance(float dir, bool horizontal)
    {
        if (horizontal == false)
            _rb.transform.Translate(Vector3.forward * _speed * Time.deltaTime * dir);        
        if (horizontal)
            _rb.transform.Translate(Vector3.right * _speed * Time.deltaTime * dir);

    }

    public WalkAdvance(Rigidbody rb, float speed)
    {
        _speed = speed;
        _rb = rb;
    }
}
