using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject asteroid_obj;

    // Start is called before the first frame update
    void Start()
    {
        CreateAsteroids(5);
    }

    void CreateAsteroids(int asteroidsNum)
    {
        for (int i = 0; i < asteroidsNum; i++)
        {
            GameObject asteroidsClone = Instantiate(asteroid_obj);
        }
    }

}
