using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWallTakeDamage : Enemy {

    public SpikeWall_ScriptObject spikeWallData;

    void Start()
    {
        damage = spikeWallData.damage;
    }
    
}
