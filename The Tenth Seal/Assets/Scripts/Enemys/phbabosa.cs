using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phbabosa : MonoBehaviour
{


    public Transform phBabosa;
    public Transform playerarm;

    private float _distanceBabosa;

    private float speedBabosa = 15f;

    private int followRangeBabosa = 60;
    private int _attackRangeBabosa = 20;

    private bool _detectedBabosa;

    private float _rotationSpeedBabosa = 5f;

    private int counter;

    public GameObject _babosa;
    public GameObject []ph;

    void Start()
    {
        counter = 0;
        phBabosa = transform;
    }

    void Update()
    {
        var distance = (playerarm.transform.position - phBabosa.transform.position).magnitude;
        _distanceBabosa = distance;


        if (_distanceBabosa <= followRangeBabosa)
        {
            _detectedBabosa = true;
        }
        else _detectedBabosa = false;

        if (_detectedBabosa && counter < 1)
        {
            counter++;
            for (int i = 0; i < ph.Length; i++)
            {
                GameObject babosa = GameObject.Instantiate(_babosa);
                _babosa.transform.position = ph[i].transform.position;


            } 
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRangeBabosa);

    }

    void FollowThePlayerLikeBabosa()
    {
        if (_distanceBabosa >= _attackRangeBabosa)
        {
            phBabosa.position += phBabosa.forward * speedBabosa * Time.deltaTime;
        }


        phBabosa.rotation = Quaternion.Slerp(phBabosa.rotation, Quaternion.LookRotation(playerarm.position - phBabosa.position), _rotationSpeedBabosa * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        SoundsManager.instancia.Play((int)SoundID.Sream, 0.5f, false);

    }


}
