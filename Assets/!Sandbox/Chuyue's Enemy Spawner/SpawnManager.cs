using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Spawner[] spawners;
    public bool isPaused;
    public bool previousPaused;

    void Start() {
        spawners = GetComponentsInChildren<Spawner>();
        isPaused = false;
        previousPaused = false;
    }

    void Update() {
        if (isPaused != previousPaused) {
            if (isPaused) {
                OnPause();
            } else {
                OnUnpause();
            }
            previousPaused = isPaused;
        }
    }

    void OnPause() {
        foreach (Spawner spawner in spawners) {
            spawner.Disable();
        }
    }

    void OnUnpause() {
        foreach (Spawner spawner in spawners) {
            spawner.Enable();
        }
    }
}
