using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "Controller Move", menuName = "Actions/Player/Controller Move")]
    public class ControllerMove : ScriptableObject
    {
        public float speed;

        public void Execute(Player player)
        {
            var moveInput = player.Controls.Move.ReadValue<Vector2>();
            player.Direction = new Vector2(Mathf.RoundToInt(moveInput.x), Mathf.RoundToInt(moveInput.y));
            player.Speed = speed;
        }
    }
}
