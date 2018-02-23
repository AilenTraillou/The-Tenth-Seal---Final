using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Slug : Enemy{

    Transform phSlug;
    public Transform character;

    float distanceSlug;
    float speedSlug = 25f;
    float rotationSpeedSlug = 5f;
    int followRangeSlug = 60;
    int attackRangeSlug = 20;
    bool detectedSlug;

    void Start()
    {
        damage = 0.5f;
        phSlug = transform;
        character = FindObjectOfType<Character>().gameObject.transform;
        GetComponent<Animation>().Stop("babosa");
    }

    void Update()
    {
        var distance = (character.transform.position - phSlug.transform.position).magnitude;
        distanceSlug = distance;

        if (distanceSlug <= followRangeSlug)
        {
            detectedSlug = true;
        }
        else
        {
            GetComponent<Animation>().Stop("babosa");
            detectedSlug = false;
        }

        if (detectedSlug)
        {
            GetComponent<Animation>().Play("babosa");
            FollowThePlayer();
        }        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRangeSlug);

    }

    void FollowThePlayer()
    {
        if (distanceSlug >= attackRangeSlug)
        {
            phSlug.position += phSlug.forward * speedSlug * Time.deltaTime;
        }

        phSlug.rotation = Quaternion.Slerp(phSlug.rotation, Quaternion.LookRotation(character.position - phSlug.position), rotationSpeedSlug * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

}
