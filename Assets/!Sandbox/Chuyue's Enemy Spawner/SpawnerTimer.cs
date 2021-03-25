using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTimer : Spawner
{
    public int secondsToWait = 5;
    float timePassed = 0;

    void Update()
    {
        if (isEnabled) {
            timePassed += Time.deltaTime;
        }
        
        if (timePassed >= secondsToWait) {
            Spawn();
        }
    }

    protected override void AfterSpawn() {
        timePassed = 0;
    }

    protected override void OnDisable() {
        
    }

    protected override void OnEnable() {
        
    }
}
