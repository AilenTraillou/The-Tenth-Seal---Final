using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScneGhostGirl : MonoBehaviour {

    public GameObject enfoque;
    public GameObject dialogue;
    public ModelCharacter character;
    public GameObject Maincamera;
    public GameObject ghost;
    public bool move;
    public bool move2;
    bool once;

    public List<GameObject> ListCanvas = new List<GameObject>();

    public IEnumerator Scene()
    {
        character.OnPause(false);
        enfoque.SetActive(true);
        dialogue.SetActive(true);
        ghost.SetActive(true);
        Maincamera.transform.LookAt(ghost.transform.position);
        foreach (var item in ListCanvas) item.SetActive(false);
        yield return new WaitForSeconds(2);
        dialogue.SetActive(false);       
        StartCoroutine(Movement());
    }

    public IEnumerator Movement()
    {
        move = true;
        ghost.transform.forward = new Vector3(-ghost.transform.forward.x, ghost.transform.forward.y, ghost.transform.forward.z);
        yield return new WaitForSeconds(3);
        move = false;
        StartCoroutine(Movement2());
    }

    public IEnumerator Movement2()
    {
        move2 = true;
        ghost.transform.forward = new Vector3(ghost.transform.forward.x, ghost.transform.forward.y, -5);
        yield return new WaitForSeconds(3);
        move2 = false;
        ghost.SetActive(false);
        foreach (var item in ListCanvas) item.SetActive(true);
        enfoque.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (move)
        {
            ghost.transform.position += ghost.transform.forward * 25 * Time.deltaTime;
        }

        if (move2)
        {
            ghost.transform.position += ghost.transform.forward * 25 * Time.deltaTime;
        }

    }

    public void OnTriggerEnter(Collider c)
    {

        if (!once)
        {
            StartCoroutine(Scene());
            once = true;
        }
    }
}
