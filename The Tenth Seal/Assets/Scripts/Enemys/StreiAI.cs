using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreiAI : MonoBehaviour, IOnPause {

    public Transform enemy;
    public Transform player;
    public float speed = 15f;
    public int _followRange = 50;
    public int _attackRange = 20;

    public GameObject particles;
    public GameObject ph_particles;
    public Light pointlight;

    private float _distance;  
    private bool _detected;
    private float _rotationSpeed = 5f;

    float auxSpeed;
    bool onPause;

    void Start()
    {
        if (pointlight != null)
        {
            pointlight.enabled = false;
        }
        enemy = transform;
        auxSpeed = speed;       
    }

    void Update()
    {
        var distance = (player.transform.position - enemy.position).magnitude;
        _distance = distance;


        if (_distance <= _followRange)
        {
            _detected = true;
        }
        else _detected = false;

        if (_detected)
        {
            FollowThePlayer();
        }
        else
        {
            if (gameObject.name == "Monstruo Esqueleto 2")
            {
                GetComponent<Animation>().Stop("Run");
                GetComponent<Animation>().Play("Idle");
            }
            pointlight.enabled = false;
        }

        if (onPause == false)
            speed = auxSpeed;
        else
            speed = 0;

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _followRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    public void FollowThePlayer()
    {
       
        if(_distance >= _attackRange)
        {
            if (gameObject.name == "Monstruo Esqueleto 2")
            {
                GetComponent<Animation>().Stop("Idle");
                GetComponent<Animation>().Play("Run");

            }
            enemy.position += enemy.forward * speed * Time.deltaTime;
        }

        if (_distance <= _attackRange)
        {
            
            Attack();
        }

        enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(player.position - enemy.position), _rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (pointlight != null)
        {
            pointlight.enabled = true;
        }

    }

    void Attack()
    {

        if (gameObject.tag == "hiena")
        {
            MainPlayer.instance.AddFear(90f);
            Destroy(gameObject);

        }else
        {
            if(particles != null)
            {
                GameObject attack_effect = GameObject.Instantiate(particles);
                attack_effect.transform.position = ph_particles.transform.position;
                attack_effect.transform.forward = ph_particles.transform.forward;
            }
        }

        if (gameObject.name == "Monstruo Esqueleto 2")
        {
            GetComponent<Animation>().Play("Attack");
        }
    }

    public void OnPause(bool isOnPause)
    {
        onPause = isOnPause;
    }
}
