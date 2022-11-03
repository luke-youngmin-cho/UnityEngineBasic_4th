using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUnit : MonoBehaviour
{
    public float length;
    public event Action OnReachedToEnd;

    private void Awake()
    {
        GameStateManager.instance.OnStateChanged += OnGameStateChanged;
    }
    private void OnDestroy()
    {
        GameStateManager.instance.OnStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameStates newState)
    {
        enabled = newState == GameStates.Play;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * 5.0f * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("MapEnd"))
        {
            OnReachedToEnd?.Invoke();
            Destroy(gameObject);
        }
    }
}
