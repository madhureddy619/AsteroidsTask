using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public const string key_highScore = "prefs_HighScore";
    public const string key_maxWaves = "prefs_MaxWaves";

    void Awake()
    {
        //Setting up default data 
        if (!PlayerPrefs.HasKey("InistialLaunch") || PlayerPrefs.GetInt("InistialLaunch") == 0)
        {
            PlayerPrefs.SetInt("InistialLaunch", 1);
            PlayerPrefs.SetInt(key_highScore, 0);
            PlayerPrefs.SetInt(key_maxWaves, 0);
        }
    }

    public static int higeScore(int count)
    {
        if (PlayerPrefs.GetInt(key_highScore) <= count)
            PlayerPrefs.SetInt(key_highScore, count);

        return PlayerPrefs.GetInt(key_highScore,0);
    }

    public static int maxWaves(int count)
    {
        if (PlayerPrefs.GetInt(key_maxWaves) <= count)
            PlayerPrefs.SetInt(key_maxWaves, count);

        return PlayerPrefs.GetInt(key_maxWaves, 0);
    }


}
