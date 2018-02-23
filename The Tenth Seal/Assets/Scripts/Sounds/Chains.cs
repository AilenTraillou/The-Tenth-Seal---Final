using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chains : MonoBehaviour {


    public Transform chains;
    public Transform player;

    private float _distance;

    private float targetDistance = 10f;
    private float minDist = 10f;
    

    private int _followRange = 120;
    

    private bool _detected;


    
    

    void Start()
    {
       
        chains = this.transform;
    }

    void Update()
    {
        var distance = (player.transform.position - chains.transform.position).magnitude;
        _distance = distance;


        if (_distance <= _followRange)
        {
            _detected = true;
        }
        else _detected = false;

        if (_detected)
        {
            SoundsManager.instancia.Play((int)SoundID.Chains, 0.5f, false);
            Destroy(this.gameObject);
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _followRange);
  
    }
    
}
