using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour {


    void OnParticleCollision(GameObject g)
    {
        if (this.tag == "acuatico" && g.layer == 10) 
        {
            //Character.instance.ReciveDamage(0.05f);
            MainPlayer.instance.AddFear(0.7f);
        }else
        {   if(g.gameObject.tag == "Character")
            {
                //Character.instance.ReciveDamage(0.003f);
                MainPlayer.instance.AddFear(0.02f);
            }
            
        }
        
    }

}
