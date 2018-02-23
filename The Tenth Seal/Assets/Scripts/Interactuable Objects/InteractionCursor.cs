using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCursor : MonoBehaviour, IObservable {

    List<IObserver> observerList = new List<IObserver>();

    public void Suscribe(IObserver observer)
    {
        if (!observerList.Contains(observer))
        {
            observerList.Add(observer);
        }
    }

    public void Unsuscribe(IObserver observer)
    {
        if (observerList.Contains(observer))
        {
            observerList.Remove(observer);
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<ControllerCharacter>())
        {
            foreach (var item in observerList)
            {
                item.Notify(gameObject);
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.GetComponent<ControllerCharacter>())
        {
            foreach (var item in observerList)
            {
                item.Notify(gameObject);
            }
        }
    }

    void OnDestroy()
    {
        foreach (var item in observerList)
        {
            item.Notify(gameObject);
        }
    }
}
