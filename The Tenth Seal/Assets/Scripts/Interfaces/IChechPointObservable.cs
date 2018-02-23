using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChechPointObservable {

    void Suscribe( ICheckPointObserver observer);
    
    void Unsuscribe(ICheckPointObserver observer);

}
