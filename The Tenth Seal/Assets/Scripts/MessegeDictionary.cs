using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessegeDictionary : MonoBehaviour {


    public const int LLAVE = 1;
    public const int ACEITE = 2;
    public const int MEDICINA = 3;
    public const int PUERTA_1_CASA = 4;
    public const int PUERTA_2_CASA = 5;
    public const int PUERTA_1_HOSPITAL = 6;

    public Dictionary<int, string> _Messeges;
    public static MessegeDictionary instance;

    void Awake()
    {
        instance = this;

        if (_Messeges == null)
        {
            _Messeges = new Dictionary<int, string>();
        }
        

    }

    public string CreateMessage(int key)
    {
        
            if (!_Messeges.ContainsKey(key))
            {
                _Messeges.Add(key, "mensaje");
            }

        return _Messeges[key];

    }

}
