using UnityEngine;

namespace Entity
{
    public abstract class EnemyState : ScriptableObject
    {
        public abstract EnemyState Execute(Enemy enemy);
    }
}
