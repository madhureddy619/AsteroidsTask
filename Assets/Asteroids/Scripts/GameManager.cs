using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Type of Asteroids")]
    public GameObject asteroid_large;
    public GameObject asteroid_medium;
    public GameObject asteroid_small;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            throw new SingletonException("Only one instance is allowed");
        }

    }
    void Start()
    {
        GenerateAsteroids(asteroid_large,5);
    }


    public static void GenerateAsteroids(GameObject type,int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject asteroidsClone = Instantiate(type);
        }
    }
    public static void GenerateAsteroids(GameObject type, int count,Vector3 pos)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject asteroidsClone = Instantiate(type,pos,Quaternion.identity);
        }
    }

}
