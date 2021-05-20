using Pathfinding;
using Project.Views.Combat;
using UnityEngine;

namespace Project.Views.World.Entities
{
    public class EnemyUi : MonoBehaviour
    {
        [SerializeField]
        private WorldView view;

        [SerializeField]
        private IAstarAI ai;

        [SerializeField]
        private WeaponUi weapon;

        [SerializeField]
        private int range;

        public Vector3 Position => transform.position;

        public void Init()
        {

        }

        // Always update state in the Update loop.
        // Note that we can also not update the state if the game is paused
        private void Update()
        {
            ai.destination = view.PlayerPosition;
            weapon.Aim(view.PlayerPosition);

            var currentPosition = transform.position;
            var playerPosition = view.PlayerPosition;
            var distance = (playerPosition - currentPosition).sqrMagnitude;

            if (distance < range * range)
            {
                weapon.Attack();
            }
        }
    }
}
