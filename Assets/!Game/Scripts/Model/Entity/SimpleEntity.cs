using UnityEngine;

namespace Model
{
    public class SimpleEntity : Entity
    {
        // --- Components ---

        private void FixedUpdate()
        {
            Rigidbody.MovePosition(Rigidbody.position + Speed * Time.deltaTime * Direction);
        }
    }
}
