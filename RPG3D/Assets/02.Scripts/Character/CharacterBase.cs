using UnityEngine;

[RequireComponent(typeof(AnimationManager))]
public abstract class CharacterBase : MonoBehaviour, IStats
{
    protected Rigidbody rb;
    protected AnimationManager animationManager;


    public Stats stats { get; private set; }

    protected IStateMachine machine;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animationManager = GetComponent<AnimationManager>();
        machine = CreateStateMachine();
        stats = GetComponent<Stats>();
    }

    protected virtual void Update()
    {
        machine.Tick();
    }

    protected abstract IStateMachine CreateStateMachine();
}