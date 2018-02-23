using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour, IObserver {

    public Image mana;
    private IObservable ManaCharge;
    private IObservable ConsMana;
    bool consumeManaIsActive = false;
    public bool restMana = false;

    private void Update()
    {     
        if (restMana)
        {
            mana.fillAmount -= 0.001f;
        }

        if (mana.fillAmount <= 0)
        {
            restMana = false;
        }
    }


    public void Notify(GameObject _object)
    {
        if (_object.GetComponent(typeof(PowerUpLight)))
        {
            mana.fillAmount += 25f;
        }

        if (_object.GetComponent<ConsumeMana>())
        {
            consumeManaIsActive = !consumeManaIsActive;

            if (consumeManaIsActive)
            {
                restMana = true;
            }
            else restMana = false;
        }
    }
}
