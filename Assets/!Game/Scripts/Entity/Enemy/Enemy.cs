using Combat.Weapons;
using Common.Logic.States;
using Pathfinding;
using UnityEngine;

namespace Entity
{
    /**
     * Custom StateMachine{Enemy, EnemyState}
     *     Enemy = This class, which inherits from StateMachine
     *     EnemyState = The state class, which is a State{Enemy}
     */
    public class Enemy : StateMachine<Enemy, EnemyState>,
        IAttacker,
        IPathfinder
    {
        // --- Components ---

        public Transform Transform { get; private set; }

        [SerializeField]
        private Weapon weapon;
        public Weapon Weapon
        {
            get => weapon;
            set => weapon = value;
        }

        [SerializeField]
        private IAstarAI ai;
        public IAstarAI Ai
        {
            get => ai;
            set => ai = value;
        }

        // --- Functions ---

        private void Awake()
        {
            Transform = transform;
            Ai = GetComponent<IAstarAI>();
        }

        // Always update state in the Update loop.
        // Note that we can also not update the state if the game is paused
        private void Update()
        {
            UpdateState(this);
        }
    }
}
