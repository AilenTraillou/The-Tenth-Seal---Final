using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class ModelCharacter : MonoBehaviour, IOnPause {

    public event Action<float, float> GetLife = delegate { };
    public event Action<float> TakeMana = delegate { };
    public event Action<float, bool> TakeOil = delegate { };
    public event Action<GameObject> DestroyGameObject = delegate { };
    public event Action<string> OpenDialog = delegate { };
    public event Action<float, bool, bool> OnWalk = delegate { };
    public event Action OnIdle = delegate { };
    public event Action TakeKey = delegate { };
    public event Action OnJump = delegate { };
    public event Action<GameObject> OnScreamer = delegate { };
    public event Action InteractCursor = delegate { };
    public event Action<bool> AcidBurn = delegate { };
    public event Action<GameObject> ManaManage = delegate { };
    public event Action<bool> ConsumeOil = delegate { };

    public static float lifeToNextLevel = 100f;
    public static float oilToNextLevel = 100.9f;

    public float life = 100;
    //public float mana = 100;
    public float oil = 100.9f;
    [HideInInspector]
    public float lifeToRecover = 0.0001f;
    [HideInInspector]
    public float manaToRecover = 0.0f;
    [HideInInspector]
    public float oilToRecover = 0.0f;

    public float characterSpeed;                  
    public float characterRunSpeed;               
                                                  
    public Vector3 direction;                     
    public GameObject visual;                     
                                                  
    public Rigidbody rb;                          
    public float characterJumpStr;

    ///run speed = 80
    private float chSpeed = 60;
    private float chRunSpeed = 70;
    private float chJumpStr;

    public bool isJumping;
    public bool isGrounded;
    private int ground;

    public bool isIdle;
    public bool isRuning;
    public bool isWalking;
    public bool isWalkingOnWater;

    public bool activateObject;
    public bool pickUpMana;
    public bool pickUpOil;
    public bool pickUpKey;
    public bool consumeMana;
    public bool consumeOil = true;

    public bool onPause;

    GameObject objectOnTrigger;
    WaterAndWaterfall water;

    IAdvance currentState;
    IAdvance jump;
    IAdvance walk;
    IAdvance run;

    public static int deathTimes;
    public bool usedCheckpoint;
    public float totalDamage;
    public float totalConsumedOil;
    public bool spikeWallAttack;
    public bool acidWaterAttack;
    public bool screamerAttack;
    float timeToFindGem;
    bool foundGem;
    

    void Start () {

        rb = GetComponent<Rigidbody>();
        water = FindObjectOfType<WaterAndWaterfall>();

        /// STRATEGY OF MOVEMENT
        currentState = null;
        walk = new WalkAdvance(rb, characterSpeed);
        run = new WalkAdvance(rb, characterRunSpeed);
        jump = new JumpAdvance(rb, characterJumpStr);

        chSpeed = characterSpeed;
        chRunSpeed = characterRunSpeed;
        chJumpStr = characterJumpStr;

        life = lifeToNextLevel;
        oil = oilToNextLevel;
    }
	
	void Update () {

        if (onPause == false)
            OnGame();
	}

    public void OnPause(bool isOnPause)
    {
        onPause = isOnPause;
    }

    public void OnGame()
    {
        if(water != null)
        {
            if (water.waterOn == false)
                isWalkingOnWater = false;
        }
        if (isGrounded)
        {
            Grounding();
        }

        ManageLife();
        ManageOil();

        if (!isWalking && !isRuning && !isJumping && !isWalkingOnWater)
            Idle();

        if (isWalkingOnWater)
            AcidBurn(true);
        else
            AcidBurn(false);

        if(foundGem == false)
            timeToFindGem += Time.deltaTime;
    }

    public void SetStatsOnNextLevel()
    {
        lifeToNextLevel = life;
    }

    public void TakeDamage(float value)
    {
        totalDamage += value;
        life -= value;
        GetLife(life, totalDamage);
    }

    public void Run(bool horizontal, float dir)
    {
        currentState = run;

        if (!isJumping)
        {
            if(!isWalkingOnWater)
                OnWalk(0.7f, true, false);
            else
                OnWalk(0.7f, true, true);
        }

        if (horizontal)
            currentState.Advance(dir, true);
        else
        if (horizontal == false)
            currentState.Advance(dir, false);
        
        isWalking = true;
        isRuning = true;
        isIdle = false;
        isJumping = false;
    }

    public void Walk(bool horizontal, float dir)
    {
        currentState = walk;

        if (!isJumping)
        {
            if (!isWalkingOnWater)
                OnWalk(0.4f, false, false);
            else
                OnWalk(0.4f, false, true);
        }

        if (horizontal)
            currentState.Advance(dir, true);
        else
        if(horizontal == false)
            currentState.Advance(dir, false);

        isWalking = true;
        isIdle = false;
        isJumping = false;

    }

    public void WalkWithJoystick(float hor, float ver)
    {
        rb.transform.Translate(new Vector3(hor, 0, ver).normalized * chSpeed * Time.deltaTime);
        isIdle = false;
        isJumping = false;
        isRuning = false;
        isWalking = true;
        if (!isJumping && !isIdle)
        {
            OnWalk(0.4f, false, false);
        }
    }

    public void Jump()
    {
        OnJump();
        currentState = jump;
        currentState.Advance(0, false);
        isJumping = true;
        isRuning = false;
        isWalking = false;
        isIdle = false;
        ground = 1;
    }

    public void Idle()
    {
        OnIdle();
        isIdle = true;
        currentState = null;
        isRuning = false;
        isWalking = false;
    }

    void Grounding()
    {
        OnJump();
        isGrounded = false;
        ground = 0;
    }

    public void RestartFromDeath()
    {
        life += 100;
        GetLife(life, totalDamage);
        oil += 100.9f;
        TakeOil(oil, false);
    }

    void Death()
    {
        deathTimes++;

        Scene _scene = SceneManager.GetActiveScene();
        Analytics.CustomEvent("Death Statistics", new Dictionary<string, object>
        {
            {"Current Total Deaths", deathTimes},
            {"Total Consumed Oil",  Math.Truncate(totalConsumedOil)},
            {"Used Checkpoint",  usedCheckpoint},
            {"Level", _scene.name }
        });

        Analytics.CustomEvent("Assasins Statistics", new Dictionary<string, object>
        {
            {"Total Damage Taken",  totalDamage},
            {"SpikeWall attack",  spikeWallAttack},
            {"Acid Water attack",  acidWaterAttack},
            {"Screamer attack",  screamerAttack},
            {"Level", _scene.name }
        });    
    }

    void GemStatistics()
    {
        foundGem = false;

        Scene _scene = SceneManager.GetActiveScene();
        Analytics.CustomEvent("Gem Statistics - ", new Dictionary<string, object>
        {
            {"Taken Time to found Gem",  Math.Round(timeToFindGem)},
            {"Level", _scene.name }
        });
    }

    /// 
    /// LIFE, MANA AND OIL MANAGER BELOW
    /// 

    void ManageLife()
    {
        if (life > 100)
            life = 100;

        life += lifeToRecover;
        GetLife(life, totalDamage);

        if (life <= 0)
        {
            life = 0;
            Death();
        }
    }

    //void ManageMana()
    //{
    //    if (mana > 100)
    //        mana = 100;
    //    if(manaToRecover != 0)
    //        mana += manaToRecover;
    //    TakeMana(mana);

    //    if (consumeMana)
    //    {
    //        mana -= 0.1f;
    //        TakeMana(mana);
    //    }
    //}

    public void ConsumeMana()
    {
        consumeMana = !consumeMana;
    }

    void ManageOil()
    {
        if (oil > 100.9)
            oil = 100.9f;

        if(oilToRecover != 0)
            oil += oilToRecover;

        if (consumeOil)
        {
            if (oil > 0)
                oil -= 0.01f;
                //oil -= 0.002f;

            totalConsumedOil += 0.01f;

            TakeOil(oil, false);
        }

        ConsumeOil(consumeOil);

        oilToNextLevel = oil;
    }

    /// 
    /// INTERACCIONS WITH OBJECTS
    /// 

    public void Interact()
    {
        if (activateObject)
        {
            if(objectOnTrigger != null)
            {
                if (objectOnTrigger.GetComponent(typeof(IInteractuableObjects)))
                    objectOnTrigger.GetComponent<IInteractuableObjects>().ActivateObject();

                if (objectOnTrigger.GetComponent(typeof(Key)))
                {
                    TakeKey();
                    DestroyGameObject(objectOnTrigger);
                    ObjectsCount.instance.totalItemsOnLevelFound++;
                }

                if (objectOnTrigger.GetComponent(typeof(Oil)))
                {
                    if (objectOnTrigger.GetComponent<Oil>().canGetOil)
                    {
                        oil += objectOnTrigger.GetComponent<Oil>().oilRecover;
                        TakeOil(oil, true);
                        objectOnTrigger.GetComponent<Oil>().canGetOil = false;
                        ObjectsCount.instance.totalOilCharged += objectOnTrigger.GetComponent<Oil>().oilRecover;
                    }
                }

                if (objectOnTrigger.GetComponent(typeof(Medicine)))
                {              
                    life += objectOnTrigger.GetComponent<Medicine>().hpRecover;
                    GetLife(life, 0);
                    DestroyGameObject(objectOnTrigger);
                    ObjectsCount.instance.lifeRecovered++;
                    ObjectsCount.instance.totalItemsOnLevelFound++;
                }

                if (objectOnTrigger.GetComponent(typeof(Gems)))
                {
                    GemStatistics();
                    DestroyGameObject(objectOnTrigger);
                    ObjectsCount.instance.totalItemsOnLevelFound++;
                }

                if (objectOnTrigger.GetComponent(typeof(Mana)))
                {
                    ManaManage(objectOnTrigger);
                    DestroyGameObject(objectOnTrigger);
                    ObjectsCount.instance.totalItemsOnLevelFound++;
                }

                if (objectOnTrigger.GetComponent(typeof(RedSubstance)))
                {
                    DestroyGameObject(objectOnTrigger);
                }

                if (objectOnTrigger.GetComponent(typeof(Jar)))
                {
                    ManaManage(objectOnTrigger);
                }
            }         
        }
    }

    /// 
    /// COLLISIONS SECCION
    /// 
    /// FIRST TRIGGERS, THEN NORMAL ONES.
    /// 

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<AcidWater>() && water.waterOn)
        {
            acidWaterAttack = true;
            isWalkingOnWater = true;
        }

        if (c.GetComponent(typeof(IInteractuableObjects)))
        {
            if (c.GetComponent<Lever>())
            {
                c.GetComponent<Lever>().ActivateOnTrigger();
            }

            activateObject = true;
            objectOnTrigger = c.gameObject;
        }

        if (c.GetComponent<Screamer>())
        {
            screamerAttack = true;
            OnScreamer(c.gameObject);
            c.GetComponent<Screamer>().ActivateScreamer();
            objectOnTrigger = c.gameObject;
        }

        if (c.gameObject.GetComponent<Enemy>())
        {
            if (c.gameObject.GetComponent<Enemy>())
                spikeWallAttack = true;

            TakeDamage(c.gameObject.GetComponent<Enemy>().damage);
            ObjectsCount.instance.damageTaken += c.gameObject.GetComponent<Enemy>().damage;
        }
    }

    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.GetComponent<AcidWater>() && water.waterOn)
        {
            isWalkingOnWater = true;
            TakeDamage(c.gameObject.GetComponent<AcidWater>().damage);
            ObjectsCount.instance.damageTaken += c.gameObject.GetComponent<AcidWater>().damage;
        }

        if (c.GetComponent(typeof(IInteractuableObjects)))
        {
            c.GetComponent<IInteractuableObjects>().ActivateOnTrigger();
            InteractCursor();
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.GetComponent<AcidWater>())
        {
            isWalkingOnWater = false;
        }

        if (c.GetComponent(typeof(IInteractuableObjects)) && c.gameObject != null)
        {
            objectOnTrigger = c.gameObject;
            objectOnTrigger.GetComponent<IInteractuableObjects>().DesactivateObject();
            activateObject = false;
        }
    }

    ///                
    /// -232.3
    /// NORMAL COLLISIONS
    /// 

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.layer == 8)
        {
            isJumping = false;
        }

        if (ground == 1)
        {
            isGrounded = true;
            isWalking = true;
        }

        else isGrounded = false;

        if (c.gameObject.GetComponent<Enemy>())
        {
            if (c.gameObject.GetComponent<SkeletonMonster>() == false)
                TakeDamage(c.gameObject.GetComponent<Enemy>().damage);

            if (c.gameObject.GetComponent<SkeletonMonster>() && c.gameObject.GetComponent<SkeletonMonster>().isAttacking)
            {
                TakeDamage(c.gameObject.GetComponent<Enemy>().damage);
                print("ddasddadas monster");
            }
            
            ObjectsCount.instance.damageTaken += c.gameObject.GetComponent<Enemy>().damage;
        }
    }

    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.GetComponent<Enemy>())
        {
            TakeDamage(c.gameObject.GetComponent<Enemy>().damage);
            ObjectsCount.instance.damageTaken += c.gameObject.GetComponent<Enemy>().damage;
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.layer == 8)
        {
            isGrounded = false;
        }
    }
}
