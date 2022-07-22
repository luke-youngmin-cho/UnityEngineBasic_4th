using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(bulletPrefab, firePoint); // 생성된 게임오브젝트를 해당 transform 에 종속시킴
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
