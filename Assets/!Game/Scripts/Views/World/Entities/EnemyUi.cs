using Pathfinding;
using Project.Views.Combat;
using UnityEngine;

namespace Project.Views.World.Entities
{
    public class EnemyUi : MonoBehaviour
    {
        [SerializeField]
        private IAstarAI ai;

        [SerializeField]
        private WeaponUi weapon;

        [SerializeField]
        private int range;

        private WorldView _view;

        public Vector3 Position => transform.position;

        private void Awake()
        {
            ai = GetComponent<IAstarAI>();
        }

        public void Init(WorldView view)
        {
            _view = view;
        }

        // Always update state in the Update loop.
        // Note that we can also not update the state if the game is paused
        private void Update()
        {
            ai.destination = _view.PlayerPosition;
            weapon.Aim(_view.PlayerPosition);

            var currentPosition = transform.position;
            var playerPosition = _view.PlayerPosition;
            var distance = (playerPosition - currentPosition).sqrMagnitude;

            if (distance < range * range)
            {
                weapon.Attack();
            }
        }
    }
}
