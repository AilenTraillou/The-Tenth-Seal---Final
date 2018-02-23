using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBlackEffect : ObservableInterface
{
    public Image screamerImage;
    GameObject _screamer;
    List<GameObject> _screamerList = new List<GameObject>();
    bool blackScreenOn;
    byte _newAplha = 0;
    string screamerName;
    ModelCharacter model;
    AudioClip screamerSound;

    void Awake () {

        model = FindObjectOfType<ModelCharacter>();
        model.OnScreamer += GetScreamer;

        List<GameObject> aux = new List<GameObject>();
        aux.AddRange(FindObjectsOfType<GameObject>());
        for (int i = 0; i < aux.Count; i++)
        {
            if (aux[i].gameObject.GetComponent<Screamer>())
            {
                _screamerList.Add(aux[i]);
            }
        }
    }

    void Update()
    {
        if (blackScreenOn)
        {
            _newAplha = 255;
            blackScreenOn = false;
        }

        if (_newAplha != 0)
        {
            _newAplha--;

            Color newScreamerAlpha = new Color32(109, 72, 72, _newAplha);
            screamerImage.color = newScreamerAlpha;
            MainPlayer.instance.AddFear(70f);
        }

    }

    public void Notify(GameObject _object)
    {
        screamerName = _object.name;
    }

    public void GetScreamer(GameObject screamerObject)
    {
        blackScreenOn = true;
        screamerImage.sprite = screamerObject.GetComponent<Screamer>().screamerImage;

    }
}
