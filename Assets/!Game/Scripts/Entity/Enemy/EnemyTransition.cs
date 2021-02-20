using System;
using Common.Logic.States;

namespace Entity
{
    /**
     * Represents conditional state changes for Enemy objects
     */
    [Serializable]
    public class EnemyTransition : Transition<EnemyState, EnemyCondition> { }
}
