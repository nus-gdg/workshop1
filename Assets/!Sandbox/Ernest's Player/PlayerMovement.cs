using Project.Views.Combat;
using UnityEngine;

namespace Testing
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public Rigidbody2D rb;
        public Camera cam;
        public WeaponUi weapon;

        Vector2 movement;
        Vector2 mousePos;

        public Transform playerSkin;


        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -cam.transform.position.z));
            
            // danielnyan: Move firePoint to player? How should we handle fire point?
            Vector2 lookDir = mousePos - (Vector2)weapon.FirePoint.position;
            float rotateAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            playerSkin.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
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
}
