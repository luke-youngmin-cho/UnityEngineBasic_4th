﻿using UnityEngine;

[RequireComponent(typeof(AnimationManager))]
public abstract class CharacterBase : MonoBehaviour
{
    public float jumpForce;
    public int atk = 1;
    protected Rigidbody rb;
    protected AnimationManager animationManager;
    public LayerMask targetLayer;
    public GameObject target;
    public float detectRange;
    public float detectAttackRange;

    public int hp;

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