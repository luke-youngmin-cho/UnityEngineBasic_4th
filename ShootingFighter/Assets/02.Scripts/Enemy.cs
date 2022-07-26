using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private float _hp;
    public float hp
    {
        set 
        {
            if (value < 0)
                value = 0;

            _hp = value;
            hpBar.value = _hp / hpMax;
            if (_hp <= 0)
            {
                GameObject effect = Instantiate(_destroyEffect, transform.position, transform.rotation);
                Destroy(effect, 2f);
                Destroy(gameObject);
            }   
        }
        get
        { 
            return _hp; 
        }
    }
    public float hpMax;
    public Slider hpBar;

    public float score;
    [SerializeField] private float _moveSpeed;

    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _damage;
    [SerializeField] private GameObject _destroyEffect;

    private void Awake()
    {
        hp = hpMax;
    }


    private void FixedUpdate()
    {
        Vector3 deltaMove = Vector3.back * _moveSpeed * Time.fixedDeltaTime;
        transform.Translate(deltaMove);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (1<<other.gameObject.layer == _targetLayer)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                player.hp -= _damage;
            }
            Destroy(gameObject);
        }
    }
}
