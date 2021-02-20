using Combat.Weapons;
using Common.Logic.States;
using Core;
using Core.Managers;
using UnityEngine;

namespace Entity
{
    /**
     * Custom StateMachine{Player, PlayerState}
     *     Player = This class, which inherits from StateMachine
     *     PlayerState = The state class, which is a State{Player}
     */
    public class Player : StateMachine<Player, PlayerState>,
        IAttacker,
        IInputPlayerMoveListener,
        IInputPlayerAttackListener
    {
        // --- Components ---

        public Transform Transform { get; private set; }
        
        [SerializeField]
        private new Rigidbody2D rigidbody;
        public Rigidbody2D Rigidbody
        {
            get => rigidbody;
            set => rigidbody = value;
        }

        [SerializeField]
        private float speed;
        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public Vector2 Direction { get; set; }

        [SerializeField]
        private Weapon weapon;
        public Weapon Weapon
        {
            get => weapon;
            set => weapon = value;
        }
        
        public InputManager.PlayerActions Controls { get; private set; }
        
        // --- Functions ---

        private void Awake()
        {
            Transform = transform;
            Controls = Game.Instance.Input.Player;
        }

        // Always update state in the Update loop.
        // Note that we can also not update the state if the game is paused
        private void Update()
        {
            UpdateState(this);
        }
        
        private void FixedUpdate()
        {
            rigidbody.MovePosition(rigidbody.position + Speed * Time.deltaTime * Direction);
        }
    }
}
