using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    public Weapon weapon;
    

    Vector2 movement;
    Vector2 mousePos;

    public Transform playerSkin;
    
    
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
        Debug.Log(Input.mousePosition);
        weapon.target = mousePos;
        Debug.Log(mousePos);
        playerSkin.rotation = Quaternion.LookRotation((Vector3) weapon.lookDir, Vector3.forward);
        if (Input.GetMouseButton(0))
        {
            weapon.Attack();
        }
        

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        
    }
}
