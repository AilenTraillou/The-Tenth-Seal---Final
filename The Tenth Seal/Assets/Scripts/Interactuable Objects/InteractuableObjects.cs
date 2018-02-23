using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuableObjects : MonoBehaviour, IInteractuableObjects {

    public event Action<bool> GetCursor = delegate { };
    public ViewCharacter view;

    public GameObject target;
    public float angleOfSight;
    public float distance;

    Vector3 _directionToTarget;
    float _angleToTarget;
    float _distanceToTarget;
    bool _targetOnSight;

    public virtual void Start()
    {
        view = FindObjectOfType<ViewCharacter>();
    }

    public virtual void ActivateObject()
    {

    }

    public virtual void ActivateOnTrigger()
    {
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
                    print("OKEY");
                    _targetOnSight = true;
                    GetCursor(true);
                }
                else
                {
                    _targetOnSight = false;
                    GetCursor(false);

                }
            }

        }
        
        if(_angleToTarget > angleOfSight && _distanceToTarget <= distance)
        {
            print("sasasasa");
            _targetOnSight = false;
            GetCursor(false);

        }
    }

    public virtual void DesactivateObject()
    {
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
