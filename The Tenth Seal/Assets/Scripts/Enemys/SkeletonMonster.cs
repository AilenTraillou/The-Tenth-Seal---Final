using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMonster : Enemy {

    public float _followRange;
    public float _attackRange;
    public bool isAttacking;
    public Transform character;
    public GameObject gem;

    float rotationSpeed = 5;
    float _distance;
    bool _detected;
    bool _isDead;

    Animation anim;

    void Start()
    {
        damage = 20;
        anim = GetComponent<Animation>();
        gem.SetActive(false);
    }

    void Update()
    {
        var distance = (character.transform.position - transform.position).magnitude;
        _distance = distance;

        if(_isDead == false)
        {
            if (_distance <= _followRange)
            {
                _detected = true;
            }
            else
            {
                anim.Play("Idle");
                _detected = false;
            }

            if (_detected)
            {
                FollowCharacter();
            }
        }
    }

    public void MonsterDead()
    {
        _isDead = true;
        anim.Play("Dead");
        damage = 0;
        gem.SetActive(true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _followRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    void FollowCharacter()
    {
        if (_distance <= _attackRange)
        {
            Attack();
        }
        else
        {
            isAttacking = false;
            anim.Play("Run");
            transform.position += transform.forward * 35 * Time.deltaTime;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(character.position - 
            transform.position), rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    void Attack()
    {
        anim.Play("Attack");
        isAttacking = true;
    }

}
