using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _hp;
    public float hp
    {
        set 
        { 
            _hp = value; 
            if (_hp <= 0)
                Destroy(gameObject);
        }
        get
        { 
            return _hp; 
        }
    }
    public float hpInit;

    public float score;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        hp = hpInit;
    }

    private void FixedUpdate()
    {
        Vector3 deltaMove = Vector3.back * moveSpeed * Time.fixedDeltaTime;
        transform.Translate(deltaMove);
    }
}
