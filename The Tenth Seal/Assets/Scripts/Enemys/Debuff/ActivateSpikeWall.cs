using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpikeWall : ObservableInterface
{

    void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<ControllerCharacter>())
        {
            foreach (var item in observerList)
            {
                item.Notify(gameObject);
            }
        }
    }

}
