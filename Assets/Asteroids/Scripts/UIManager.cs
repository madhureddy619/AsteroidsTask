using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Menu Objects")]
    public GameObject ui_mainMenu;
    public GameObject ui_intructions;
    public GameObject ui_pause;
    public GameObject ui_gameOver;
    public GameObject ui_hud;

    [Header("Text Objects")]
    public Text menu_HighScore;
    public Text menu_maxWaves;

    [Header("")]
    public Text hud_score;
    public Text hud_waves;

    [Header("")]
    public Text gameover_score;
    public Text gameover_waves;
    public Text gameover_highscore;
    public Text gameover_maxwaves;

    #region Buttons Clicks
    void DisableAllMenus()
    {
        ui_mainMenu.SetActive(false);
        ui_intructions.SetActive(false);
        ui_pause.SetActive(false);
        ui_gameOver.SetActive(false);
        ui_hud.SetActive(false);
    }

    public void ButtonClick(string menuName)
    {
        DisableAllMenus();
        switch (menuName)
        {
            case "MAIN_MENU":
                ui_mainMenu.SetActive(true);

                menu_HighScore.text = "High Score : " + PlayerPrefsManager.higeScore(GameManager.score).ToString();
                menu_maxWaves.text = "Higest Wave : " +PlayerPrefsManager.maxWaves(GameManager.waves).ToString();

                GameManager.instance.presentState = GameManager.GameState.UI_MENU;
                break;
            case "INTRUCTIONS":
                ui_intructions.SetActive(true);
                GameManager.instance.presentState = GameManager.GameState.INTRUCTIONS;
                break;
            case "START":
                ui_hud.SetActive(true);
                GameManager.instance.StartGame();
                
                break;
            case "GAME_OVER":
                ui_gameOver.SetActive(true);

              
                gameover_score.text = "Score : "+GameManager.score.ToString();
                gameover_waves.text = "Waves : "+GameManager.waves.ToString();

                gameover_highscore.text = "High Score : "+PlayerPrefsManager.higeScore(GameManager.score).ToString();
                gameover_maxwaves.text = "Higest Wave : "+PlayerPrefsManager.maxWaves(GameManager.waves).ToString();

                break;
            case "PAUSE":
                ui_pause.SetActive(true);
                GameManager.instance.StopGamePlay();
                GameManager.instance.presentState = GameManager.GameState.PAUSED;
                break;
            case "REPLAY":
                ui_hud.SetActive(true);

                GameManager.instance.ReplayGame();
                break;
            case "RESUME":
                ui_hud.SetActive(true);

                GameManager.instance.ResumeGame();
                break;
            default:
                Debug.Log("########## Please assign correct name of buttonc ############");
                break;
        }
    }




    #endregion



}
