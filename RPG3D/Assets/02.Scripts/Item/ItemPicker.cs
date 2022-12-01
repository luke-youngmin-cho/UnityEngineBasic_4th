using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    [SerializeField] private LayerMask _itemLayer;

    private void OnTriggerStay(Collider other)
    {
        if (1<<other.gameObject.layer == _itemLayer)
        {
            other.gameObject.GetComponent<Item>().PickUp(gameObject);
        }
    }
}
