using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class destrutivel : MonoBehaviour
{
    public float destructionTime = 1f;

    [Range(0f,1f)]
    public float itemSpawnChance = 0.2f;
    public GameObject[] spawnableItems;

    private void Start()
    {
        Destroy(gameObject, destructionTime);
    }

    private void OnDestroy()
    {
        if (spawnableItems.Length > 0 && Random.value < itemSpawnChance)
        {
            int randomIdex = Random.Range(0, spawnableItems.Length);
            Instantiate(spawnableItems[randomIdex], transform.position, Quaternion.identity);
        }
    }
}
