using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservableInterface : MonoBehaviour, IObservable {

    public List<IObserver> observerList = new List<IObserver>();

    public void Suscribe(IObserver observer)
    {
        if (!observerList.Contains(observer))
        {
            observerList.Add(observer);
        }
    }

    public void Unsuscribe(IObserver observer)
    {
        print(observer);
        if (observerList.Contains(observer))
        {
            observerList.Remove(observer);
        }
    }

}
