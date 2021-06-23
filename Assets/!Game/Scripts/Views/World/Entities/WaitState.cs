using Project.Common;
using UnityEngine;

namespace Project.Views.World.Entities
{
    public class WaitState : IState
    {
        private float _minTime;
        private float _maxTime;

        private float _timer;

        public float DecisionUpdateRate { get; private set; } = 0.1f;
        public bool StateEnded { get; private set; } = false;

        public WaitState(float minTime, float maxTime)
        {
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
        }
    }
}
