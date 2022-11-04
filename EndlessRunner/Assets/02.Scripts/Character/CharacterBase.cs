using UnityEngine;

[RequireComponent(typeof(AnimationManager))]
public abstract class CharacterBase : MonoBehaviour, IHp
{
    public float jumpForce;
    public int atk = 1;
    protected Rigidbody rb;
    protected AnimationManager animationManager;
    public LayerMask targetLayer;
    public GameObject target;
    public float detectRange;
    public float detectAttackRange;

    public abstract int hp { get; set; }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animationManager = GetComponent<AnimationManager>();
        GameStateManager.instance.OnStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.instance.OnStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameStates newState)
    {
        enabled = newState == GameStates.Play;

        switch (newState)
        {
            case GameStates.None:
            case GameStates.Idle:
            case GameStates.Play:
                animationManager.speed = 1.0f;
                break;
            case GameStates.Paused:
                animationManager.speed = 0.0f;
                break;
            default:
                break;
        }
    }
}