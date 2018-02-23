using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class ViewCharacter : MonoBehaviour, IOnPause {

    public Image cursor;
    public Image life;
    public Image mana;
    public Text oil;
    public Text deathText;
    public Image blackScreen;
    public Text manaCount;
    public Light characterLight;
    byte alpha = 0;


    public Image dialogBox;
    public Text message;
    public AudioClip[] clips;
    public AudioSource[] channels;

    public ParticleSystem steam;

    CameraWalkAnimation walkAnimation;
    bool onPause;
    bool onAcidBurn;
    float _totalDamage;

    void Awake()
    {
        channels = new AudioSource[clips.Length];

        for (int i = 0; i < clips.Length; i++)
        {
            channels[i] = gameObject.AddComponent<AudioSource>();
            channels[i].clip = clips[i];
        }

        steam.playOnAwake = false;
        steam.Stop();
    }

	void Start ()
    {
        cursor.enabled = false;
        walkAnimation = Camera.main.GetComponent<CameraWalkAnimation>();
    }

    private void Update()
    {
        if (life.fillAmount <= 0)
        {
            if (alpha < 255)
                alpha++;
            ChangeAlpha(alpha);
            Invoke("RestartScene", 4f);
        }       
    }

    public void OnPause(bool isOnPause)
    {
        onPause = isOnPause;

        if(onPause)
        {
            if(walkAnimation.walk)
                walkAnimation.walk = false;
            if (steam.isEmitting && steam.isPlaying)
            {
                steam.Pause();
                channels[CharacterSounds.CHARACTER_ACID_BURN].Pause();
            }
        }else
        {
            if (steam.isPaused && onAcidBurn)
            {
                steam.Play();
                channels[CharacterSounds.CHARACTER_ACID_BURN].Play();
            }
            
        }

        if (onAcidBurn == false)
            steam.Stop();
    }

    public void GetCursor()
    {
        cursor.enabled = true;
    }

    public void Idle()
    {
        walkAnimation.walk = false;
        
    }

    public void GetLife(float _lifeValue, float totalDamage)
    {
        life.fillAmount = _lifeValue / 100;
        _totalDamage = totalDamage;
        
    }

    public void GetOil(float _oilValue, bool takeOil)
    {
        if (_oilValue > 100.9f) _oilValue = 100.9f;
        oil.text = "Oil: %" + Math.Truncate(_oilValue);

        if(takeOil)
        Play(CharacterSounds.CHARACTER_PICKUP_OIL);
    }

    public void LightOnOff(bool active)
    {
        characterLight.enabled = active;
    }

    public void GetKey()
    {
        Play(CharacterSounds.CHARACTER_PICKUP_KEY);
    }

    public void GetMana(float _manaValue)
    {
        mana.fillAmount = _manaValue / 100;
    }

    public void ManageCheckpoint(GameObject _object)
    {
        if (_object.GetComponent<Mana>())
        {
            ObjectsCount.instance.mana++;
            ObjectsCount.instance.totalManaFound++;
            Play(CharacterSounds.CHARACTER_PICKUP_MANA, 0.7f);
        }
        if (_object.GetComponent<Jar>())
        {
            if (ObjectsCount.instance.mana > 0)
                ObjectsCount.instance.mana--;
            else
                ObjectsCount.instance.mana = 0;
        }

        manaCount.text = "x" + ObjectsCount.instance.mana;
    }

    public void DestroyObject(GameObject _object)
    {
        Destroy(_object);
    }

    void ChangeAlpha(byte newcolor)
    {
        Color newTextAplha = new Color32(255, 255, 255, newcolor);
        Color newScreenAlpha = new Color32(0, 0, 0, newcolor);
        deathText.color = newTextAplha;
        blackScreen.color = newScreenAlpha;
    }

    void RestartScene()
    {
        Cursor.lockState = CursorLockMode.None;
        Analytics.CustomEvent("Damage", new Dictionary<string, object>
        {
            {"Total accumulative Damage",  _totalDamage}
            
        });
        print("evento");
        SceneManager.LoadScene(2);
    }

    public void AcidBurn(bool active)
    {
        onAcidBurn = active;

        if (active)
        {
            Play(CharacterSounds.CHARACTER_ACID_BURN, 0.5f);
        }
        else
        {
            Stop(CharacterSounds.CHARACTER_ACID_BURN);
            steam.Play();
        }        
    }

    public void Walking(float _volume, bool isRuning, bool onWater)
    {

        if (isRuning)
        {
            walkAnimation.walk = true;
            walkAnimation.animSpeed = 0.3f;
            if (!onWater)
            {
                channels[CharacterSounds.CHARACTER_RUN].pitch = 0.8f;
                Play(CharacterSounds.CHARACTER_RUN, _volume);
            }
            else
            {
                channels[CharacterSounds.CHARACTER_RUN_WATER].pitch = 1f;
                Play(CharacterSounds.CHARACTER_RUN_WATER, _volume);
            }
        }
        else
        {
            walkAnimation.walk = true;
            walkAnimation.animSpeed = 0.2f;
            if (!onWater)
            {
                channels[CharacterSounds.CHARACTER_WALK].pitch = 1f;
                Play(CharacterSounds.CHARACTER_WALK, _volume);                      
            }
            else
            {
                channels[CharacterSounds.CHARACTER_WALK_WATER].pitch = 1f;
                Play(CharacterSounds.CHARACTER_WALK_WATER, _volume);
            }
        }     
    }

    public void Jumping()
    {
        Play(CharacterSounds.CHARACTER_JUMP, 0.7f);
    }

    void Play(int soundID, float volume = 1, bool loop = false)
    {
        if (channels[soundID].isPlaying) return;

        channels[soundID].Play();
        channels[soundID].volume = volume;
        channels[soundID].loop = loop;

    }

    public void Stop(int soundID)
    {
        channels[soundID].Stop();
    }
}

public class CharacterSounds
{
    public const int CHARACTER_WALK = 0;
    public const int CHARACTER_JUMP = 1;
    public const int CHARACTER_TAKING_DAMAGE = 2;
    public const int CHARACTER_PULSE = 3;
    public const int CHARACTER_PICKUP_KEY = 4;
    public const int CHARACTER_PICKUP_OIL = 5;
    public const int CHARACTER_RUN = 6;
    public const int CHARACTER_WALK_WATER = 7;
    public const int CHARACTER_RUN_WATER = 8;
    public const int CHARACTER_ACID_BURN = 9;
    public const int CHARACTER_PICKUP_MANA = 10;

}
