using System;
using Combat.Weapons;
using Core;
using Core.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Entity
{
    [Serializable]
    public class PlayerEvent : UnityEvent<Player> { }

    public class Player : MonoBehaviour, IAttacker
    {
        // Dependencies
        public InputManager.PlayerActions Controls { get; private set; }

        // Components
        [SerializeField]
        private new Rigidbody2D rigidbody;

        // Movement variables
        public float Speed { get; set; }
        public Vector2 Direction { get; set; }
        
        // Attacker
        [SerializeField]
        private Weapon weapon;
        public Weapon Weapon
        {
            get => weapon;
            set => weapon = value;
        }
        
        public Transform Transform { get; private set; }

        [SerializeField]
        private PlayerState state;

        private void Awake()
        {
            Controls = Game.Instance.Input.Player;
            Transform = transform;
            Game.Instance.World.Player = this;
        }
        
        public void Update()
        {
            if (state == null) return;
            state = state.Execute(this);
        }

        private void FixedUpdate()
        {
            rigidbody.MovePosition(rigidbody.position + Speed * Time.deltaTime * Direction);
        }
    }
}
