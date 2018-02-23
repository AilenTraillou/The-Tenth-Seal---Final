using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPainting : MonoBehaviour
{
    public Material screamMaterial;
    public Material bountyMaterial;
    public List<GameObject> listPainting;
    public int screamerPainting;
    public int bountyPainting;
    public List<GameObject> listTemp;
    void Awake ()
    {
        listTemp = new List<GameObject>();

        listTemp.AddRange(listPainting);

        for (int i = 0; i < screamerPainting; i++)
        {
            var random = Random.Range(0, listTemp.Count);
            listTemp[random].GetComponent<Renderer>().material = screamMaterial;
            listTemp.RemoveAt(random);
        }

        for (int i = 0; i < bountyPainting; i++)
        {
            var random = Random.Range(0, listTemp.Count);
            listTemp[random].GetComponent<Renderer>().material = bountyMaterial
                ;
            listTemp.RemoveAt(random);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
