using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGameObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private List<GameObject> _spawnItems;
    [SerializeField] private float _spawnPercent; 

    private void OnEnable()
    {
        if (Random.Range(0.0f, 100.0f) < _spawnPercent)
        {
            GameObject item = Instantiate(_spawnItems[Random.Range(0, _spawnItems.Count)], _spawnPoint);
        }        
    }
}
