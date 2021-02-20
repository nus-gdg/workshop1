using Combat.Weapons;
using Common.Logic.States;
using UnityEngine;

namespace Entity
{
    /**
     * Custom StateMachine{Player, PlayerState}
     *     Player = This class, which inherits from StateMachine
     *     PlayerState = The state class, which is a State{Player}
     */
    public class Player : StateMachine<Player, PlayerState>,
        IAttacker
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
        
        // --- Functions ---

        private void Awake()
        {
            Transform = transform;
        }

        // Always update state in the Update loop.
        // Note that we can also not update the state if the game is paused
        private void Update()
        {
            UpdateState(this);
        }
    }
}
