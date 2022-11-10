using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPersonSayingHi : MonoBehaviour
{
    private void Start()
    {
        TestButton.instance.Subscribe(gameObject, () => Debug.Log("Hi"));
    }
}
