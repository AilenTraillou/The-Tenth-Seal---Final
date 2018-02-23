using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable {

    void Suscribe(IObserver observer);

    void Unsuscribe(IObserver observer);
}
