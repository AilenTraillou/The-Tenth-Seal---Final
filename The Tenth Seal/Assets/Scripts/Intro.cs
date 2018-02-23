using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour {

    public MovieTexture _intro;
    public AudioSource _introAudio;
    string _introPath = "IntroDecimoSelloFinal.mp4";

    bool startCounter;

	void Start ()
    {
        GetComponent<RawImage>().material.mainTexture = _intro as MovieTexture;
        _introAudio = GetComponent<AudioSource>();
        _introAudio.clip = _intro.audioClip;

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            _intro.Play();
            _introAudio.Play();
            startCounter = true;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            Handheld.PlayFullScreenMovie(_introPath, Color.black, FullScreenMovieControlMode.Full,
                FullScreenMovieScalingMode.AspectFill);

            startCounter = true;
        }

    }

    void Update()
    {
        Invoke("ChangeToMenu", 41);      
    }

    void ChangeToMenu()
    {
            SceneManager.LoadScene(1);       
    }

}
