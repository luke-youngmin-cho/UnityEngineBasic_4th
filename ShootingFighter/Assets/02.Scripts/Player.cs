using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public static Player instance;

    private float _hp;
    public float hp
    {
        set
        {
            if (value < 0)
                value = 0;

            _hp = value;
            _hpSlider.value = _hp / _hpMax;

            if (_hp <= 0)
                GameManager.instance.GameOver();
        }
        get
        {
            return _hp;
        }
    }
    [SerializeField] private float _hpMax;
    [SerializeField] private Slider _hpSlider;

    public void RecoverHP()
    {
        hp = _hpMax;
    }

    private void Awake()
    {
        hp = _hpMax;
        instance = this;
    }
}
