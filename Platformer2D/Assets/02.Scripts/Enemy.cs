using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
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

            _hpBar.value = (float)value / _hpMax;
            _hp = value;
        }
    }
    [SerializeField] private Slider _hpBar;
    [SerializeField] private int _hpMax;
    [SerializeField] private int _damage;

    [SerializeField] private LayerMask _targetLayer;
    private EnemyController _controller;

    public void Hurt(int damage)
    {
        hp -= damage;
        if (_hp > 0)
            _controller.TryHurt();
        else
            _controller.TryDie();
    }



    private void Awake()
    {
        _controller = GetComponent<EnemyController>();
        hp = _hpMax;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go != null)
        {
            if (1 << go.layer == _targetLayer)
            {
                if (go.TryGetComponent(out Player player))
                {
                    if (player.invincible == false)
                    {
                        player.Hurt(_damage);
                        go.GetComponent<PlayerController>().KnockBack();
                    }
                }
            }
        }
    }
}
