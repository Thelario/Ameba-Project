using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCanvasManager : MonoBehaviour
{
    public GameObject gameMenu;
    public GameObject endGameMenu;
    public GameObject pauseMenu;
    public GameObject victoryMenu;
    public GameObject hintMenu;

    private void Awake()
    {
        GameManager.LevelStart += ActivateHintMenu;
        StatsBattleManager.LostGame += LostInGame;
        StatsBattleManager.WonGame += WonInGame;
    }

    public void ActivateGameMenu()
    {
        gameMenu.SetActive(true);
        endGameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        victoryMenu.SetActive(false);
        hintMenu.SetActive(false);
        Time.timeScale = 1f;
    }   
    
    public void ActivateEndGameMenu()
    {
        gameMenu.SetActive(false);
        endGameMenu.SetActive(true);
        pauseMenu.SetActive(false);
        victoryMenu.SetActive(false);
    } 
    
    public void ActivatePauseGameMenu()
    {
        gameMenu.SetActive(true);
        endGameMenu.SetActive(false);
        pauseMenu.SetActive(true);
        victoryMenu.SetActive(false);
        Time.timeScale = 0f;
    }    

    public void ActivateVictoryGameMenu()
    {
        gameMenu.SetActive(false);
        endGameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        victoryMenu.SetActive(true);
    }

    public void ActivateHintMenu()
    {
        if (GameManager.GetInstance().hintImage.sprite == null)
        {
            ActivateGameMenu();
            return;
        }

        gameMenu.SetActive(true);
        endGameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        hintMenu.SetActive(true);
        victoryMenu.SetActive(false);
        Time.timeScale = 0f;
        StartCoroutine(StartGame());
    }

    public void LostInGame()
    {
        StartCoroutine(LostGame());
    }

    public void WonInGame()
    {
        StartCoroutine(WonGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(4f);
        ActivateGameMenu();
    }

    IEnumerator WonGame()
    {
        ActivateVictoryGameMenu();
        yield return new WaitForSecondsRealtime(3f);
        ActivateGameMenu();
        GameManager.GetInstance().EndedInGame();
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.WorldMapMenu);
    }

    IEnumerator LostGame()
    {
        ActivateEndGameMenu();
        yield return new WaitForSecondsRealtime(3f);
        ActivateGameMenu();
        GameManager.GetInstance().EndedInGame();
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.WorldMapMenu);
    }
}
