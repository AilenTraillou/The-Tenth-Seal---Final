using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour {

    [SerializeField]
    public float fillAmount;

    [SerializeField]
    private Image lifeBar;


    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }

    }

    public int roundManager = 0;


    void Start () {

	
	}
	
	void Update () {

        HandleBar();
        //if (fillAmount <= 0) roundManager = 1;
        //if (changeRound.resetCanvasOK) ResetLife();

	}

    private void HandleBar()
    {
        if (fillAmount != lifeBar.fillAmount) lifeBar.fillAmount = fillAmount;
        
    }


    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {

        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;

    }

    private void ResetLife()
    {
        fillAmount = 100;

    }
}
