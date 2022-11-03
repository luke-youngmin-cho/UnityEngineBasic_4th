using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {
        Player.instance.StartMove();
        GameStateManager.instance.SetState(GameStates.Play);
    }

    private void Start()
    {
        GameStateManager.instance.SetState(GameStates.Paused);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameStateManager.instance.SetState(
                GameStateManager.instance.Current
                == GameStates.Play ? GameStates.Paused : GameStates.Play
            );
        }
    }
}
