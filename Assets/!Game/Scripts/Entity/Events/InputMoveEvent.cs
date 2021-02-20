using UnityEngine;

namespace Entity.Events
{
    [CreateAssetMenu(fileName = "InputMove", menuName = "Events/Input/Move")]
    public class InputMoveEvent : ScriptableObject
    {
        public float speed;

        public void Execute(IInputPlayerMoveListener listener)
        {
            var moveInput = listener.Controls.Move.ReadValue<Vector2>();
            listener.Direction = new Vector2(Mathf.RoundToInt(moveInput.x), Mathf.RoundToInt(moveInput.y));
            listener.Speed = speed;
        }
    }
}
