using UnityEngine;

namespace Common.Logic
{
    public class BehaviourTreeTimer
    {
        private float _time;

        public BehaviourTreeTimer(float time)
        {
            _time = time;
        }

        public void Tick()
        {
            _time -= Time.deltaTime;
        }

        public bool IsCompleted()
        {
            return _time < 0f;
        }
    }
}
