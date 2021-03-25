using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public GameObject justSpawned;
    public bool isEnabled;

    void Start() {
        isEnabled = true;
    }

    protected virtual void Spawn() {
        // Instantiate at spawner's position and zero rotation.
        justSpawned = Instantiate(enemyToSpawn, this.transform.position, Quaternion.identity);
        Debug.Log("spawned enemy");
        AfterSpawn();
    }

    protected abstract void AfterSpawn();

    public void Disable() {
        isEnabled = false;
        OnDisable();
    }

    protected abstract void OnDisable();

    public void Enable() {
        isEnabled = true;
        OnEnable();
    }

    protected abstract void OnEnable();
}
