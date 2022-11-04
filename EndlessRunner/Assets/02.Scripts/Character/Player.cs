using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
public class Player : MonoBehaviour
{
    public static Player instance;    
    public CharacterPlayer character;
    [SerializeField] private LayerMask _itemLayer;

    public void StartMove()
    {
        character.StartMove();
    }

    private void Awake()
    {
        instance = this;
        character = GetComponent<CharacterPlayer>();
        GameStateManager.instance.OnStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.instance.OnStateChanged -= OnGameStateChanged;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == _itemLayer)
        {
            other.gameObject.GetComponent<Item>().OnEarn();
            Destroy(other.gameObject);
        }
    }

    private void OnGameStateChanged(GameStates newState)
    {
        enabled = newState == GameStates.Play;
    }
}