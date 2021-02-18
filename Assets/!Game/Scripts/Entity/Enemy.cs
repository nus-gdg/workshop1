using System;
using Combat.Weapons;
using Pathfinding;
using UnityEngine;
using UnityEngine.Events;

namespace Entity
{
    [Serializable]
    public class EnemyEvent : UnityEvent<Enemy> { }

    public class Enemy : MonoBehaviour, IPathfinder, IAttacker
    {
        [SerializeField]
        private IAstarAI ai;
        public IAstarAI Ai => ai;

        public Transform Transform { get; private set; }
        
        [SerializeField]
        private Weapon weapon;
        public Weapon Weapon
        {
            get => weapon;
            set => weapon = value;
        }

        [SerializeField]
        private EnemyState state;

        private void Start()
        {
            ai = GetComponent<IAstarAI>();
            Transform = transform;
        }

        public void Update()
        {
            if (state == null) return;
            state = state.Execute(this);
        }
    }
}
