using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public Transform character;
    public Transform level;
    public int sceneNumber;
    public static float totalTimeElapsedInGame;
    public float timeElapsed;

    bool _onPause;
    ModelCharacter model;
    GameManager gameManager;

    void Start()
    {
        model = FindObjectOfType<ModelCharacter>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (_onPause == false)
        {
            totalTimeElapsedInGame += Time.deltaTime;
            timeElapsed += Time.deltaTime;
        }
    }

    void LevelSuccessStatistics()
    {
        Scene _scene = SceneManager.GetActiveScene();

        Analytics.CustomEvent("Level Complete - Objects Found", new Dictionary<string, object>
        {
            {"Level", _scene.name},
            {"Gem Found", ObjectsCount.instance.gems},
            {"Total Substance Found", ObjectsCount.instance.totalManaFound},
            {"Checkpoint Found",  model.usedCheckpoint}

        });

        Analytics.CustomEvent("Level Complete - Stats", new Dictionary<string, object>
        {
            {"Level", _scene.name},
            {"Remaining Life", model.life},
            {"Remaining Oil", Math.Truncate(model.oil)},
            {"Total Consumed Oil",  model.totalConsumedOil},
            {"Total Damage Taken",  model.totalDamage},

        });

        Analytics.CustomEvent("Level Complete - Time Record", new Dictionary<string, object>
        {
            {"Level", _scene.name },
            {"Taken Time to finish Level",  Math.Round(timeElapsed)},

        });
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<Character>())
        {
            SceneManager.LoadScene(sceneNumber);
            LevelSuccessStatistics();
        }
    }

    public void FinishGameStatistics()
    {
        Scene _scene = SceneManager.GetActiveScene();

        Analytics.CustomEvent("Level Complete - Time Record", new Dictionary<string, object>
        {
            {"Level", _scene.name },
            {"Taken Time to finish Level",  Math.Round(totalTimeElapsedInGame)},

        });
    }

    public void OnPause(bool onPause)
    {
        _onPause = onPause;
    }

}
