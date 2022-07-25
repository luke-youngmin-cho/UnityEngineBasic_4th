using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject _gameOverUI;


    public void GameOver()
    {
        PauseGame();
        PopUpGameOverUI();
    }

    public void ContinueGame()
    {
        ReleasePauseGame();
        HideGameOverUI();
        Player.instance.RecoverHP();
    }

    private void Awake()
    {
        instance = this;
    }

    private void PopUpGameOverUI()
    {
        _gameOverUI.SetActive(true);
    }

    private void HideGameOverUI()
    {
        _gameOverUI.SetActive(false);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ReleasePauseGame()
    {
        Time.timeScale = 1f;
    }
}
