using Project.Common;
using Project.Views.Combat;
using UnityEngine;

namespace Project.Views.World.Entities
{
    public class AttackState : IState
    {
        private WeaponUi _weapon;

        private float _minTime;
        private float _maxTime;

        private float _timer;

        public float DecisionUpdateRate { get; private set; } = 0.1f;
        public bool StateEnded { get; private set; } = false;

        public AttackState(WeaponUi weapon, float minTime, float maxTime)
        {
            _weapon = weapon;

            _minTime = minTime;
            _maxTime = maxTime;
        }

        public void OnEnter()
        {
            _timer = Random.Range(_minTime, _maxTime);
            StateEnded = false;
        }

        public void OnExit()
        {
            //
        }

        public void Tick()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                StateEnded = true;
                return;
            }

            _weapon.Attack();
        }
    }
}
