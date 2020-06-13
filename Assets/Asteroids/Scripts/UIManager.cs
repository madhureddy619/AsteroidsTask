using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject ui_mainMenu;
    public GameObject ui_intructions;
    public GameObject ui_pause;
    public GameObject ui_gameOver;
    public GameObject ui_hud;

    void Start()
    {
        DisableAllMenus();
        ui_mainMenu.SetActive(true); // Default state 
    }
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
                GameManager.instance.presentState = GameManager.GameState.UI_MENU;
                break;
            case "INTRUCTIONS":
                ui_intructions.SetActive(true);
                GameManager.instance.presentState = GameManager.GameState.INTRUCTIONS;
                break;
            case "HUD":
                ui_hud.SetActive(true);
                GameManager.instance.presentState = GameManager.GameState.IS_PLAYING;
                break;
            case "GAME_OVER":
                ui_gameOver.SetActive(true);
                GameManager.instance.presentState = GameManager.GameState.GAME_OVER;
                break;
            case "PAUSE":
                ui_pause.SetActive(true);
                GameManager.instance.presentState = GameManager.GameState.PAUSED;
                break;
            case "REPLAY":
                ui_hud.SetActive(true);
                GameManager.instance.presentState = GameManager.GameState.IS_PLAYING;
                break;
            default:
                Debug.Log("########## Please assign correct name of buttonc ############");
                break;
        }
    }




    #endregion



}
