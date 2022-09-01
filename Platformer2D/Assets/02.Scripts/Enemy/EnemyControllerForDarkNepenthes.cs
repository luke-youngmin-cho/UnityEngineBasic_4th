using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerForDarkNepenthes : EnemyController
{
    [Header("Projectile")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;

    protected override void AttackBehavior()
    {
        GameObject go = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
        go.GetComponent<Projectile>().SetUp(direction: Vector2.right * direction,
                                            damage: _enemySelf.damage,
                                            targetLayer: _targetLayer,
                                            calcMethod: null);
    }
}
