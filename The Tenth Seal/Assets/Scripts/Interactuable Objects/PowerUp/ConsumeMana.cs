using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeMana : MonoBehaviour, IObservable {

    List<IObserver> observersList = new List<IObserver>();
    public bool consumeManaIsActive = false;

    LightBlinder lightBlinder;

    private void Awake()
    {
        lightBlinder = FindObjectOfType<LightBlinder>();
    }
    public void Suscribe(IObserver observer)
    {
        if (!observersList.Contains(observer))
        {
            observersList.Add(observer);
        }
    }

    public void Unsuscribe(IObserver observer)
    {
        if (observersList.Contains(observer))
        {
            observersList.Remove(observer);
        }

    }
	void Update () {

        if (Input.GetMouseButtonUp(1) && lightBlinder.manaManager.mana.fillAmount != 0)
        {
            for (int i = 0; i < observersList.Count; i++)
            {
                observersList[i].Notify(this.gameObject);
            }
        }

    }

}
