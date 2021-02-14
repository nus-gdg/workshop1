using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Targeter : MonoBehaviour
{
    public Transform[] waypoints;
    private AIDestinationSetter destinationScript;
    private int numOfWaypoints;
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        numOfWaypoints = waypoints.Length;
        destinationScript = gameObject.GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        // Currently, pressing E causes the enemy to rotate between
        // set waypoints as its destination
        if(Input.GetKeyDown(KeyCode.E))
        {
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        if (currentIndex == numOfWaypoints - 1)
        {
            currentIndex = 0;
        } else
        {
            currentIndex++;
        }
        destinationScript.target = waypoints[currentIndex];
    }
}
