using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompuertaAutomatica : MonoBehaviour {


    public Transform reference;
    public Transform player;
    public GameObject door;
    private float _distance;
    private float targetDistance = 10f;
    private float minDist = 10f;
    private int _followRange = 100;
    public bool _detected;
    public static CompuertaAutomatica instance;

    void Start()
    {
        reference = this.transform;
        instance = this;
    }

    void Update()
    {
        var distance = (player.transform.position - reference.transform.position).magnitude;
        _distance = distance;


        if (_distance <= _followRange)
        {
            _detected = true;
        }
        else _detected = false;

        if (_detected)
        {
            if (this.gameObject.tag == "reja" && door.transform.position.y > -60)
            {
                door.transform.position += Vector3.down * Time.deltaTime * 20;
            }
            else
            {
                if (door.transform.position.y > 23)
                {
                    door.transform.position += Vector3.down * Time.deltaTime * 15;

                }

            }

        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _followRange);

    }


}
