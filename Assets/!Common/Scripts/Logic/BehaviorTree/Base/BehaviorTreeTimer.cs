using UnityEngine;

namespace Common.Logic
{
    public class BehaviorTreeTimer
    {
        private float _time;

        public BehaviorTreeTimer(float time)
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
