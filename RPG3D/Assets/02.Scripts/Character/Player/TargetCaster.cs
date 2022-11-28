using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCaster : MonoBehaviour
{
    private bool _on;
    public bool On
    {
        get
        {
            return _on;
        }
        set
        {
            if (value &&
                _on != value)
            {
                _targetsCasted.Clear();
            }
            _on = value;
        }
    }
    [SerializeField] private LayerMask _targetLayer;
    private Dictionary<int, GameObject> _targetsCasted = new Dictionary<int, GameObject>();

    public IEnumerable<GameObject> GetTargets()
    {
        return _targetsCasted.Values;
    }

    private void OnTriggerStay(Collider other)
    {
        if (On)
        {
            if ((1<<other.gameObject.layer & _targetLayer) > 0)
            {
                if (_targetsCasted.ContainsKey(other.gameObject.GetInstanceID()) == false)
                {
                    _targetsCasted.Add(other.gameObject.GetInstanceID(), other.gameObject);
                }
            }
        }
    }
}
