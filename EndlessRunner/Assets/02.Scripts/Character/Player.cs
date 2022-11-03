using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
public class Player : MonoBehaviour
{
    public static Player instance;

    private int _hp;
    public int hp
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value < 0)
                value = 0;

            _hp = value;

            for (int i = 0; i < _hpMax; i++)
            {
                if (i < value - 1)
                    _hpIcons[i].SetActive(true);
                else
                    _hpIcons[i].SetActive(false);
            }

            if (value == 0)
            {
                _character.ChangeMachineState(CharacterPlayer.StateTypes.Die);
            }
        }
    }
    [SerializeField] private int _hpInit = 3;
    private int _hpMax;
    [SerializeField] private Transform _hpIconContent;
    private List<GameObject> _hpIcons;
    private CharacterPlayer _character;
    [SerializeField] private LayerMask _itemLayer;

    public void StartMove()
    {
        _character.StartMove();
    }

    private void Awake()
    {
        instance = this;
        _character = GetComponent<CharacterPlayer>();

        _hpIcons = new List<GameObject>();
        for (int i = 0; i < _hpIconContent.childCount; i++)
            _hpIcons.Add(_hpIconContent.GetChild(i).gameObject);

        _hpMax = _hpIcons.Count;
        hp = _hpInit;

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