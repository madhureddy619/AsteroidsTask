using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Tooltip("Assign PM - PowerUpManager script")]
    public PowerUpManager PUM;

    [Header("Variables")]
    public int initialwave_asteroids = 3;
    public int waveAsteroids_frequency = 2;

    int Score;
    public int score
    {
        get { return Score; }
        set
        {
            Score = value;
            SetHUDValues();
            CheckCurrentStage();
        }
    }

    int Waves;
    public int waves
    {
        get { return Waves; }
        set {
            Waves = value; SetHUDValues();
            UM.hud_stageText.text = "Stage "+waves.ToString();
           // CheckCurrentStage();
        }
    }

    int Lives;
    public int lives
    {
        get { return Lives; }
        set
        {
            if (lives > 3)
                lives = 3;

            Lives = value;
            UM.SetLivesUI(lives);
        }
    }


    [Header("Type of Asteroids")]
    public GameObject asteroid_large;
    public GameObject asteroid_medium;
    public GameObject asteroid_small;

    [Header("Game Field Data")]
    public GameObject spaceShip_pc;
    public GameObject playerExplosion;

    [Header("")]
    public GameObject gameFiledObj;

    public ObjectPool astPoolLarge;
    public ObjectPool astPoolMedium;
    public ObjectPool astPoolSmall;

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
        lives = 3;
        //Set default state
        presentState = GameState.UI_MENU;
        UM.ButtonClick("MAIN_MENU");
    }


    public void CheckCurrentStage()
    {       
        if (IsAllAsteroidsDisabled() && score > 0 && presentState ==  GameState.IS_PLAYING)
        {
            waves++;
            
            UM.animBanner.Play("bannerClip", 0, 0f);
            StartCoroutine(GenerateWithDalay(2f));
            
        }  
    }
    IEnumerator GenerateWithDalay(float sec)
    {
        yield return new WaitForSeconds(sec);
        GenerateAsteroids(AsteroidController.asteroidType.Large, initialwave_asteroids + (waves * waveAsteroids_frequency));
        yield return null;
    }

    void SetHUDValues()
    {
        UM.hud_score.text = "Score : " + score.ToString();
        UM.hud_waves.text = "Waves : "+waves.ToString();
    }

    public void GenerateAsteroids(AsteroidController.asteroidType type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject asteroidsClone = null;
            switch (type)
            {
                case AsteroidController.asteroidType.Large:
                    asteroidsClone = astPoolLarge.GetPoolObj();break;
                case AsteroidController.asteroidType.Medium:
                    asteroidsClone = astPoolMedium.GetPoolObj();break;
                case AsteroidController.asteroidType.Small:
                    asteroidsClone = astPoolSmall.GetPoolObj();break;
            }
           // asteroidsClone.transform.SetParent(asteroidsParent.transform);
            asteroidsClone.SetActive(true);
        }
    }
    public void GenerateAsteroids(AsteroidController.asteroidType type, int count, Vector3 pos)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject asteroidsClone = null;
            switch (type)
            {
                case AsteroidController.asteroidType.Large:
                    asteroidsClone = astPoolLarge.GetPoolObj(); break;
                case AsteroidController.asteroidType.Medium:
                    asteroidsClone = astPoolMedium.GetPoolObj(); break;
                case AsteroidController.asteroidType.Small:
                    asteroidsClone = astPoolSmall.GetPoolObj(); break;
            }
            asteroidsClone.transform.position = pos;
           // asteroidsClone.transform.SetParent(asteroidsParent.transform);
            asteroidsClone.SetActive(true);
        }
    }

    //This is use full to update waves & spawn new asteroids
    bool IsAllAsteroidsDisabled()
    {
        //Check all remainig rocks are disabled or not
        foreach (Transform child in astPoolLarge.transform)
        {
            if (child.gameObject.activeSelf)
                return false;
        }
        foreach (Transform child in astPoolMedium.transform)
        {
            if (child.gameObject.activeSelf)
                return false;
        }
        foreach (Transform child in astPoolSmall.transform)
        {
            if (child.gameObject.activeSelf)
                return false;
        }

        return true;
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
        lives = 3;
        spaceShip_pc.SetActive(true);
        GenerateAsteroids(AsteroidController.asteroidType.Large, initialwave_asteroids);
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
        lives = 3;
        UM.ResetLivesUI();
        //Reset Space ship position
        spaceShip_pc.GetComponent<Rigidbody>().isKinematic = true;
        spaceShip_pc.transform.position = Vector3.zero;
        spaceShip_pc.transform.rotation = Quaternion.identity;
        spaceShip_pc.GetComponent<Rigidbody>().isKinematic = false;

        //disable all remainig rocks
        foreach (Transform child in astPoolLarge.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in astPoolMedium.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in astPoolSmall.transform)
        {
            child.gameObject.SetActive(false);
        }
        spaceShip_pc.SetActive(true);
    }

    public void ReplayGame()
    {
        ResetGame();
        gameFiledObj.SetActive(true);
        GenerateAsteroids(AsteroidController.asteroidType.Large, initialwave_asteroids);
        presentState = GameState.IS_PLAYING;
    }

    public void GameOver()
    {
        if (lives <= 0)
        {
            UM.ButtonClick("GAME_OVER");
            SoundManager.instance.PlayClip(EAudioClip.FAILURE_SFX, 1);
            spaceShip_pc.SetActive(false);
            Instantiate(playerExplosion, spaceShip_pc.transform.position, spaceShip_pc.transform.rotation);

            ResetGame();
            presentState = GameState.GAME_OVER;
        }
        else
        {
            CheckCurrentStage();
        }
    }

    #endregion

}
