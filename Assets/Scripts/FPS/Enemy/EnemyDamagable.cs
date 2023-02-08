using UnityEngine;



namespace FPS
{
    public class EnemyDamagable : Damagable
    {
        [SerializeField] private Enemy _enemy;




        public override void HealthUpdated()
        {
            base.HealthUpdated();
            PlayHitSound();
        }
        private void PlayHitSound()
        {
            switch (_enemy.EnemyType)
            {
                case EnemyType.Soldier:
                    SoundManager.Instance.SoldierHitted(_enemy.Visual);
                    break;
                case EnemyType.Jagernaut:
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

