using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float timeBetweenMove;
    public float timeToMove;

    private Rigidbody2D rigidbody;
    private bool moving;
    private Vector3 moveDirection;
    private float timeBetweenMoveCounter;
    private float timeToMoveCounter;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        timeBetweenMove = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMove = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f); 
        moving = (Random.Range(-1,1) > 0);
        timeBetweenMoveCounter = timeBetweenMove;
        timeToMoveCounter = timeToMove;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) {
            timeToMoveCounter -= Time.deltaTime;
            rigidbody.velocity = moveDirection;

            if (timeToMoveCounter < 0f) {
                moving = false;
                timeBetweenMoveCounter = timeBetweenMove;
            }

        } else {
            timeBetweenMoveCounter -= Time.deltaTime;
            rigidbody.velocity = Vector2.zero;

            if (timeBetweenMoveCounter < 0f) {
                moving = true;
                timeToMoveCounter = timeToMove;

                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0);
            }
        }
    }
}
