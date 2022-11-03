using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public float jumpForce;
    protected Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
}