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
            InvokeSound();
        }
        private void InvokeSound()
        {
            switch (_enemy.EnemyType)
            {
                case EnemyType.Soldier:
                    SoundManager.Instance.SoldierDied(_enemy.Visual);
                    break;
                case EnemyType.Jagernaut:
                    SoundManager.Instance.JahherDead(_enemy.Visual);
                    break;
                case EnemyType.Dron:
                    break;
                case EnemyType.Copter:
                    break;
                default:
                    break;
            }
        }
    }
}

