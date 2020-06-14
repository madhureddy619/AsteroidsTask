using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { IS_PLAYING, PAUSED, UI_MENU, INTRUCTIONS, GAME_OVER, NONE }
    public static GameManager instance;

    [Header("")]
    public GameState presentState = GameState.UI_MENU; // this is a default state.

    [Tooltip("Assign PM - PlayerPrefsManager scripts")]
    public PlayerPrefsManager PM;

    [Tooltip("Assign UM - UIManager script")]
    public UIManager UM;

    [Header("Variables")]
    public static int score = 0;
    public static int waves = 1;

    [Header("Type of Asteroids")]
    public GameObject asteroid_large;
    public GameObject asteroid_medium;
    public GameObject asteroid_small;

    [Header("Game Field Data")]
    public GameObject spaceShip_pc;
    public GameObject asteroidsParent;
    [Header("")]
    public GameObject gameFiledObj;
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
        score = 0;
        waves = 1;
        //Set default state
        presentState = GameState.UI_MENU;
        UM.ButtonClick("MAIN_MENU");
    }


    private void LateUpdate()
    {

        if (presentState == GameState.IS_PLAYING)
        {
            UM.hud_score.text = "Score : "+score.ToString();
            UM.hud_waves.text = "Waves : "+waves.ToString();
        }
    }

    public void GenerateAsteroids(GameObject type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject asteroidsClone = Instantiate(type);
            asteroidsClone.transform.SetParent(asteroidsParent.transform);
        }
    }
    public void GenerateAsteroids(GameObject type, int count, Vector3 pos)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject asteroidsClone = Instantiate(type, pos, Quaternion.identity);
            asteroidsClone.transform.SetParent(asteroidsParent.transform);
        }
    }


    #region Game Cotrolling methods
    //This is used to pause & resume game
    public void StopGamePlay()
    {
        gameFiledObj.SetActive(false);
    }

    public void StartGame()
    {
        score = 0;
        waves = 1;
        spaceShip_pc.SetActive(true);
        GenerateAsteroids(asteroid_large, 5);
        presentState = GameState.IS_PLAYING;

    }

    public void ResumeGame()
    {
        
        gameFiledObj.SetActive(true);
        presentState = GameState.IS_PLAYING;
    }


    public void ResetGame()
    {
        score = 0;
        waves = 1;
        //Reset Space ship position
        spaceShip_pc.GetComponent<Rigidbody>().isKinematic = true;
        spaceShip_pc.transform.position = Vector3.zero;
        spaceShip_pc.transform.rotation = Quaternion.identity;
        spaceShip_pc.GetComponent<Rigidbody>().isKinematic = false;

        //Destroy all remainig rocks
        foreach (Transform child in asteroidsParent.transform)
        {
            Destroy(child.gameObject);
        }
        spaceShip_pc.SetActive(true);
    }

    public void ReplayGame()
    {
        ResetGame();
        gameFiledObj.SetActive(true);
        GenerateAsteroids(asteroid_large, 5);
        presentState = GameState.IS_PLAYING;
    }

    public void GameOver()
    {
        UM.ButtonClick("GAME_OVER");
        spaceShip_pc.SetActive(false);
        ResetGame();
        presentState = GameState.GAME_OVER;
    }

    #endregion

}
