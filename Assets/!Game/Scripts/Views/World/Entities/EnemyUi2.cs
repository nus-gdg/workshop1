using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Project.Common;
using Project.Views.Combat;
using UnityEngine;

namespace Project.Views.World.Entities
{
    public class EnemyUi2 : EntityUi
    {
        [SerializeField]
        private StateMachine stateMachine;

        [SerializeField]
        private IAstarAI ai;

        [SerializeField]
        private WeaponUi weapon;

        [SerializeField]
        private float chaseDistance = 5f;

        [SerializeField]
        private float attackDistance = 3f;

        [SerializeField]
        private string currentState;

        public Vector3 Position => transform.position;

        private void Awake()
        {
            ai = GetComponent<IAstarAI>();
            stateMachine = new StateMachine();
        }

        public override void Init(WorldView view)
        {
            base.Init(view);

            IState idleState = new IdleState();
            IState chaseState = new ChaseState(view, ai);
            IState attackState = new AttackState(weapon, 1f, 2f);
            IState waitState = new WaitState(4f, 6f);

            stateMachine.AddAnyTransition(idleState, () => stateMachine.currentState == null);
            stateMachine.AddTransition(idleState, chaseState, () => DistanceFromPlayer() < chaseDistance);
            stateMachine.AddTransition(chaseState, attackState, () => DistanceFromPlayer() < attackDistance);
            stateMachine.AddTransitions(
                attackState,
                () => stateMachine.currentState.StateEnded,
                new KeyValuePair<IState, float>(attackState, 0.5f),
                new KeyValuePair<IState, float>(waitState, 0.5f));
            stateMachine.AddTransition(attackState, chaseState, () => DistanceFromPlayer() > chaseDistance);
            stateMachine.AddTransition(waitState, chaseState, () => DistanceFromPlayer() > chaseDistance);
            stateMachine.AddTransition(waitState, attackState, () => stateMachine.currentState.StateEnded);

            StartCoroutine(UpdateDecision());
        }

        private float DistanceFromPlayer()
        {
            var currentPosition = transform.position;
            var playerPosition = view.PlayerPosition;

            return (playerPosition - currentPosition).magnitude;
        }

        private void Update()
        {
            weapon.Aim(view.PlayerPosition);
            stateMachine.Tick();
            currentState = stateMachine.currentState.GetType().Name;
        }

        IEnumerator UpdateDecision()
        {
            yield return new WaitForSeconds(stateMachine.UpdateDecision());
            StartCoroutine(UpdateDecision());
        }
    }
}
