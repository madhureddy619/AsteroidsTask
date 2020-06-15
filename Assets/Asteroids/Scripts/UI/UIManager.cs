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
    [Header("Lives UI")]
    public GameObject[] livesUI;

    [Header("")]
    public Text hud_score;
    public Text hud_waves;
    public Text hud_stageText;
    public Animator animBanner;

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

                menu_HighScore.text = "High Score : " + PlayerPrefsManager.higeScore(GameManager.instance.score).ToString();
                menu_maxWaves.text = "Higest Wave : " + PlayerPrefsManager.maxWaves(GameManager.instance.waves).ToString();

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

                gameover_score.text = "Score : " + GameManager.instance.score.ToString();
                gameover_waves.text = "Waves : " + GameManager.instance.waves.ToString();

                gameover_highscore.text = "High Score : " + PlayerPrefsManager.higeScore(GameManager.instance.score).ToString();
                gameover_maxwaves.text = "Higest Wave : " + PlayerPrefsManager.maxWaves(GameManager.instance.waves).ToString();

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
                Debug.Log("########## Please assign correct name of button ############");
                break;
        }
    }


    #endregion

    #region public methods
    public void SetLivesUI(int count)
    {
        if (count < 4)
        {
            for (int i = 0; i < livesUI.Length; i++)
            {
                if (i < count)
                    livesUI[i].SetActive(true);
                else
                    livesUI[i].SetActive(false);
            }
        }
    }
    public void ResetLivesUI()
    {
        livesUI[0].SetActive(true);
        livesUI[1].SetActive(true);
        livesUI[2].SetActive(true);
    }
    #endregion

}
