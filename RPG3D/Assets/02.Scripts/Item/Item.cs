using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData Data;
    public int Num;
    private bool _isPickingUp;
    private float _moveSpeed = 2.0f;
    private float _arriveDistance = 0.5f;

    public virtual void PickUp(GameObject picker)
    {
        if (_isPickingUp == false &&
            InventoryPresenter.instance.addCMD.CanExecute(this))
        {
            _isPickingUp = true;
            StartCoroutine(E_PickUpEffect(picker.transform,
                                          (item) => InventoryPresenter.instance.addCMD.TryExecute(item)));
        }
        Debug.Log("Tried pick up");
    }

    public virtual void Use()
    {

    }

    private IEnumerator E_PickUpEffect(Transform picker, Func<Item, bool> OnFinish)
    {
        while (Vector3.Distance(transform.position, picker.position) > _arriveDistance)
        {
            transform.position = Vector3.Lerp(transform.position,
                                              picker.position,
                                              _moveSpeed * Vector3.Distance(transform.position, picker.position) * Time.deltaTime);

            yield return null;
        }

        if (OnFinish.Invoke(this))
        {
            Destroy(gameObject);
        }
    }
}
