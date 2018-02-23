using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour {

    public GameObject achievement;
    public Text achievementText;

    public GameObject achievement2;
    public Text achievementText2;

    string achievements;

    public void NoMedicineAchievement()
    {
        //achievement.SetActive(true);
        //achievementText.text = "001: No Life Recovery Complete";
        //Invoke("CloseAchievementWindow", 5f);
        achievements = achievements + " ✓001: No Life Recovery.";

    }

    public void AllItemsFoundOnLevel1()
    {
        //achievement.SetActive(true);
        //achievementText.text = "002: Found All Items On Level";
        //Invoke("CloseAchievementWindow", 5f);
        achievements = achievements + " ✓002: Found All Items On Level.";

    }

    public void TimeRecordONLevel1()
    {
        //if (achievement.activeSelf)
        //{
        //    print("asasasasas");
        //    achievement2.SetActive(true);
        //    achievementText.enabled = true;
        //    achievementText2.enabled = true;
        //    achievementText2.text = "003: Time Record On Level";
        //    Invoke("CloseAchievementWindow", 5f);
        //}
        //else
        //{
        //    print("time record only");
        //    achievement.SetActive(true);
        //    achievementText.enabled = true;
        //    achievementText.text = "003: Time Record On Level";
        //    Invoke("CloseAchievementWindow", 5f);
        //}

        achievements = achievements + " ✓003: Time Record On Level.";

    }

    public void CloseAchievementWindow()
    {
        achievement.SetActive(false);
        achievementText.text = "";
        achievement2.SetActive(false);
        achievementText2.text = "";
    }

    public void NoOilCharged()
    {
        achievements = achievements + " ✓004: No Life or Oil Recovery.";
    }

    public void LessDamageTaken()
    {
        achievements = achievements + " ✓ 005: Less Damage Taken.";
    }

    public void Update()
    {
        if(achievements != "")
        {
            MessegeController.instance.OpenDialog(achievements);
            achievements = "";
        }
    }
}
