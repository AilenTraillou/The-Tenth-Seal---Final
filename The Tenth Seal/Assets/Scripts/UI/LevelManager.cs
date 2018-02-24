using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public event Action PassNextLevel = delegate { };
    public event Action NoMedicineUsedAchievement = delegate { };
    public event Action AllItemsFoundOnLevel1= delegate { };
    public event Action TimeRecordOnLevel1 = delegate { };
    public event Action NoOilCharged = delegate { };
    public event Action LessDamageTaken = delegate { };

    public Transform character;
    public Transform level;
    public int sceneNumber;
    public static float totalTimeElapsedInGame;
    public float timeElapsed;

    bool _onPause;
    ModelCharacter model;
    GameManager gameManager;
    Achievements achievements;

    static bool noMedicineUsedAchivOnLevel1;
    static bool allItemsFoundOnLevel1;
    static bool timeRecordOnLevel1;
    static bool noOilCharged;
    static bool lessDamageTaken;

    void Start()
    {
        model = FindObjectOfType<ModelCharacter>();
        gameManager = FindObjectOfType<GameManager>();
        achievements = FindObjectOfType<Achievements>();


        PassNextLevel += model.SetStatsOnNextLevel;
        NoMedicineUsedAchievement += achievements.NoMedicineAchievement;
        AllItemsFoundOnLevel1 += achievements.AllItemsFoundOnLevel1;
        TimeRecordOnLevel1 += achievements.TimeRecordONLevel1;
        TimeRecordOnLevel1 += achievements.NoOilCharged;
        TimeRecordOnLevel1 += achievements.LessDamageTaken;

        if (noMedicineUsedAchivOnLevel1)
            NoMedicineUsedAchievement();

        if (allItemsFoundOnLevel1)
            AllItemsFoundOnLevel1();

        if (timeRecordOnLevel1)
            TimeRecordOnLevel1();

        if (noOilCharged)
            NoOilCharged();

        if (lessDamageTaken)
            LessDamageTaken();
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
            {"Total Consumed Oil",  Math.Truncate(model.totalConsumedOil)},
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
            PassNextLevel();
            FinishGameAchievements();
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

    public void FinishGameAchievements()
    {
        Scene _scene = SceneManager.GetActiveScene();

        if (ObjectsCount.instance.lifeRecovered == 0)
            noMedicineUsedAchivOnLevel1 = true;

        if (ObjectsCount.instance.totalItemsOnLevelFound == ObjectsCount.instance.itemsOnLevel1
        && _scene.buildIndex == 3)     
            allItemsFoundOnLevel1 = true;

        if (ObjectsCount.instance.totalItemsOnLevelFound == ObjectsCount.instance.itemsOnLevel1
        && _scene.buildIndex == 3)
            allItemsFoundOnLevel1 = true;


        if (timeElapsed <= 120)
            timeRecordOnLevel1 = true;

        if (ObjectsCount.instance.totalOilCharged == 0 && ObjectsCount.instance.lifeRecovered == 0)
            noOilCharged = true;

        if (ObjectsCount.instance.damageTaken <= 30)
            lessDamageTaken = true;

        
    }

}
