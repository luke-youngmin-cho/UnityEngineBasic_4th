using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _pausedPanel;
    public void StartGame()
    {
        Player.instance.StartMove();
        GameStateManager.instance.SetState(GameStates.Play);
    }

    private void Start()
    {
        GameStateManager.instance.SetState(GameStates.Idle);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameStateManager.instance.SetState(
                GameStateManager.instance.Current
                == GameStates.Play ? GameStates.Paused : GameStates.Play
            );

            if (GameStateManager.instance.Current == GameStates.Paused)
                _pausedPanel.SetActive(true);
            else
                _pausedPanel.SetActive(false);
        }
    }
}
