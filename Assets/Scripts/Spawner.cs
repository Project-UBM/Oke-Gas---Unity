using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabs;
    public float spawnRate = 1f;
    public float minHeight = -1f;
    public float maxHeight = 2f;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject lobang = Instantiate(prefabs, transform.position, Quaternion.identity);
        lobang.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
}
