using System;
using UnityEngine;


namespace FPS
{
    public class EnemyDead : Death
    {
        [SerializeField] private Enemy _enemy;

        public static event Action DeadEnemy;
        public static event Action<EnemyType> DeadEnemyWithType;



        protected void InvokeDeadAction()
        {
            DeadEnemy?.Invoke();
            DeadEnemyWithType?.Invoke(_enemy.EnemyType);
        }
    }
}

