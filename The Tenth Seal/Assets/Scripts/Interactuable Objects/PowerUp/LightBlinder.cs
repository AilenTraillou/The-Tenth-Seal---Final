using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlinder : MonoBehaviour, IObserver, IObservable
{
    public List<IObserver> enemyObserversList= new List<IObserver>();

    public ManaManager manaManager;
    private IObservable consumeMana;
    public new Light light;
    Color baseColor = new Color(0.99f, 0.92f, 0.31f, 255);
    Color effectBlueColor = new Color(0.31f, 0.77f, 0.99f, 255);
    bool consumeManaIsActive = false;


    void Awake()
    {
        consumeMana = FindObjectOfType<ConsumeMana>();
        if(consumeMana != null)
            consumeMana.Suscribe(this);

        manaManager = FindObjectOfType<ManaManager>();
    }

    public void Suscribe(IObserver enemyObserver)
    {
        if (!enemyObserversList.Contains(enemyObserver))
        {
            enemyObserversList.Add(enemyObserver);
        }
    }

    public void Unsuscribe(IObserver enemyObserver)
    {

        if (enemyObserversList.Contains(enemyObserver))
        {
            enemyObserversList.Remove(enemyObserver);
        }
    }

    void Update()
    {
        if (manaManager.mana.fillAmount == 0)
        {       
            light.color = baseColor;
        }
    }

    public void Notify(GameObject _object)
    {
        consumeManaIsActive = !consumeManaIsActive;
        if (consumeManaIsActive)
        {
            light.color = effectBlueColor;
            light.intensity = 3.08f;

            for (int i = 0; i < enemyObserversList.Count; i++)
            {
                enemyObserversList[i].Notify(gameObject);
            }
        }
        else
        {
            light.color = baseColor;

        }
    }
}
