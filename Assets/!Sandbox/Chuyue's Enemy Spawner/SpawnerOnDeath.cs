using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

public class SpawnerOnDeath : Spawner
{
    public Health myEnemy;
    public bool hasSavedSpawn;

    void Start()
    {
        hasSavedSpawn = false;
        //listen for myEnemy's death
        myEnemy.OnDeath += TryToSpawn;
    }

    void TryToSpawn() {
        if (isEnabled) {
            Spawn();
        } else {
            hasSavedSpawn = true;
        }
    }

    protected override void AfterSpawn() {
        myEnemy = justSpawned.GetComponentInChildren<Health>();
        myEnemy.OnDeath += TryToSpawn;        
    }

    protected override void OnDisable() {
        Debug.Log("callinng on dsiable in death");
    }

    protected override void OnEnable() {
        Debug.Log("callinng on enable in death");
        if (hasSavedSpawn) {
            Spawn();
        }
    }

    void OnDestroy() {
        myEnemy.OnDeath -= TryToSpawn;
    }
}
