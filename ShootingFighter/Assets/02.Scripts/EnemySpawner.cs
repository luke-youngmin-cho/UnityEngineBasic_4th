using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDelay;
    private float timer;
    [SerializeField] private float oscillationLength;
    private float t;
    [SerializeField] private float oscillationSpeed;
    private void Update()
    {
        if (timer < 0)
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            timer = spawnDelay;
        }
        timer -= Time.deltaTime;

        transform.position = new Vector3(oscillationLength * Mathf.Sin(t * Random.Range(0f, oscillationSpeed)),
                                         transform.position.y,
                                         transform.position.z);
        t += Time.deltaTime;
    }
}
