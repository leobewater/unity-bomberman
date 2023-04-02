using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1f;

    // 20% chance to spawn an item after the brick is destroyed
    [Range(0, 1)]
    public float itemSpawnChance = 0.2f;
    public GameObject[] spawnableItems;

    private void Start() {
        Destroy(gameObject, destructionTime);
    }
}
