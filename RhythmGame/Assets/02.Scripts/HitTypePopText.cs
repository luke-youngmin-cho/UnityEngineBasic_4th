using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HitTypePopText : MonoBehaviour
{
    public static HitTypePopText instance;
    [SerializeField] private Color _colorCool;
    [SerializeField] private Color _colorGreat;
    [SerializeField] private Color _colorGood;
    [SerializeField] private Color _colorMiss;
    [SerializeField] private Color _colorBad;
    [SerializeField] private float _fadingTime = 0.1f;
    [SerializeField] Text _text;
    private Coroutine _coroutine;
    private bool _isFading;
    private HitType _hitType;
    public HitType hitType
    {
        set
        {
            if (_isFading)
                StopCoroutine(_coroutine);
            RefreshText(value);
            _coroutine = StartCoroutine(E_Fading());
            _hitType = value;
        }
    }

    private void RefreshText(HitType hitType)
    {
        switch (hitType)
        {
            case HitType.Bad:
                _text.text = "BAD";
                _text.color = _colorBad;
                break;
            case HitType.Miss:
                _text.text = "MISS";
                _text.color = _colorMiss;
                break;
            case HitType.Good:
                _text.text = "Good";
                _text.color = _colorGood;
                break;
            case HitType.Great:
                _text.text = "Great";
                _text.color = _colorGreat;
                break;
            case HitType.Cool:
                _text.text = "Cool";
                _text.color = _colorCool;
                break;
            default:
                break;
        }
    }
    IEnumerator E_Fading()
    {
        _isFading = true;
        while (_text.color.a > 0)
        {
            _text.color = new Color(_text.color.r,
                                    _text.color.g,
                                    _text.color.b,
                                    _text.color.a - (1.0f * _fadingTime * Time.deltaTime));
            yield return null;
        }
        _isFading = false;
        _coroutine = null;
    }

    private void Awake()
    {
        instance = this;
    }
}
