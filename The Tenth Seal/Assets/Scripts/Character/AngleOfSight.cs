using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleOfSight : MonoBehaviour {

    public event Action<bool> GetCursor = delegate { };
    public ViewCharacter view;

    public GameObject target;
    public float angleOfSight;
    public float distance;

    Vector3 _directionToTarget;
    float _angleToTarget;
    float _distanceToTarget;
    bool _targetOnSight;

	void Start () {

        //GetCursor += view.GetCursor;

	}
	
	void Update () {

        _directionToTarget = (target.transform.position - transform.position).normalized;
        _angleToTarget = Vector3.Angle(transform.forward, _directionToTarget);

        _distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (_angleToTarget <= angleOfSight && _distanceToTarget <= distance)
        {
            RaycastHit rayCast;
            bool obstacles = false;
            if (Physics.Raycast(transform.position, _directionToTarget, out rayCast, _distanceToTarget))
            {
                if (rayCast.collider.gameObject.layer == 9)
                    obstacles = true;
                if (!obstacles)
                {
                    _targetOnSight = true;
                    GetCursor(true);
                }
                else
                {
                    _targetOnSight = false;
                    GetCursor(false);

                }
            }
            
        }else
        {
            _targetOnSight = false;

        }

        //if (_distanceToTarget <= distance && _angleToTarget > angleOfSight)
        //{
        //    _targetOnSight = false;
        //    GetCursor(false);
        //}
   
	}

    void OnDrawGizmos()
    {
        /*
        Dibujamos una línea desde el NPC hasta el enemigo.
        Va a ser de color verde si lo esta viendo, roja sino.
        */
        if (_targetOnSight)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.transform.position);

        /*
        Dibujamos los límites del campo de visión.
        */
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * distance));

        Vector3 rightLimit = Quaternion.AngleAxis(angleOfSight, transform.up) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + (rightLimit * distance));

        Vector3 leftLimit = Quaternion.AngleAxis(-angleOfSight, transform.up) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + (leftLimit * distance));
    }

}
