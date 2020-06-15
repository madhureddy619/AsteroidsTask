using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [Header("PowerUp Prefabs")]
    public GameObject[] powerUpPrefs;
    public GameObject shieldObj;

    [HideInInspector]public int powerUpCounter;
    [HideInInspector] public bool isThreeShot = false;
    [HideInInspector] public Vector3 dir;

    PowerType currType;

    public void SpawnRandomPower(Transform Pos)
    {
        if (powerUpCounter >= Random.Range(5, 15))
        {
            int rand = Random.Range(0,powerUpPrefs.Length);        
            Instantiate(powerUpPrefs[rand], Pos.position,Quaternion.identity);
            powerUpCounter = 0;
        }
    }


    private void Update()
    {
        if (currType == PowerType.SHIELD)
            shieldObj.transform.Rotate(0f,2.5f,0f);
    }

    public void ActivatePower(PowerType type)
    {
        switch (type)
        {
            case PowerType.THREE_FIRE:
                isThreeShot = true;
                break;            
            case PowerType.LIFE:
                if(GameManager.instance.lives<=2)
                GameManager.instance.lives++;
                break;
            case PowerType.SHIELD:
                currType = type;
                shieldObj.SetActive(true);
                break;            
        }
    }

    public void DeActivatePower(PowerType type)
    {
        switch (type)
        {
            case PowerType.THREE_FIRE:
                isThreeShot = false;
                break;            
            case PowerType.LIFE:
                break;
            case PowerType.SHIELD:
                currType = PowerType.NONE;
                shieldObj.SetActive(false);
                break;
            
        }
    }


}