using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Slug : ObservableInterface{

    public Transform phBabosa;
    public Transform playerarm;

    private float _distanceBabosa;
    private float speedBabosa = 25f;
    private int followRangeBabosa = 60;
    private int _attackRangeBabosa = 20;
    private bool _detectedBabosa;

    private float _rotationSpeedBabosa = 5f;
    public GameObject particles;
    public GameObject ph_particles;
    public bool compuerta;


    void Start()
    {
        compuerta = true;
        phBabosa = transform;
        playerarm = GameObject.Find("Personaje").gameObject.transform;
        GetComponent<Animation>().Stop("babosa");
    }

    void Update()
    {
        var distance = (playerarm.transform.position - phBabosa.transform.position).magnitude;
        _distanceBabosa = distance;


        if (_distanceBabosa <= followRangeBabosa)
        {

            _detectedBabosa = true;
        }
        else
        {
            GetComponent<Animation>().Stop("babosa");
            _detectedBabosa = false;
        }

        if (_detectedBabosa)
        {

            GetComponent<Animation>().Play("babosa");

            FollowThePlayer();
        }

        //if (compuerta)
        //{
        //    if (CompuertaAutomatica.instance._detected)
        //    {
        //        compuerta = false;
        //        Destroy(gameObject);
        //    }
        //}
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRangeBabosa);

    }

    void FollowThePlayer()
    {
        if (_distanceBabosa >= _attackRangeBabosa)
        {
            phBabosa.position += phBabosa.forward * speedBabosa * Time.deltaTime;
        }

        if (_distanceBabosa <= _attackRangeBabosa)
        {
            Attack();
        }

        phBabosa.rotation = Quaternion.Slerp(phBabosa.rotation, Quaternion.LookRotation(playerarm.position - phBabosa.position), _rotationSpeedBabosa * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        foreach (var item in observerList)
        {
            item.Notify(gameObject);
        }
    }


    void Attack()
    {
        GameObject attack_effect = Instantiate(particles);
        attack_effect.transform.position = ph_particles.transform.position;
        attack_effect.transform.forward = ph_particles.transform.forward;
    }

}
