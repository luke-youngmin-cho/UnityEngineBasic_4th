using UnityEngine;

[RequireComponent(typeof(AnimationManager))]
public abstract class CharacterBase : MonoBehaviour, IStats
{
    public float jumpForce = 2.0f;
    public float moveSpeed = 1.0f;
    public int atk = 1;
    protected Rigidbody rb;
    protected AnimationManager animationManager;

    public int hp;

    public Stats stats { get; private set; }

    protected IStateMachine machine;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animationManager = GetComponent<AnimationManager>();
        machine = CreateStateMachine();
    }

    protected virtual void Update()
    {
        machine.Tick();
    }

    protected abstract IStateMachine CreateStateMachine();
}