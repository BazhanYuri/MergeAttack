using System.Collections;
using UnityEngine;




namespace FPS
{
    public class EnemyShootable : MonoBehaviour
    {
        [SerializeField] protected Enemy _enemy;
        [SerializeField] protected ParticleSystem _shootParticle;




        protected EnemyInfo _inf;


        private void OnEnable()
        {
            _enemy.Death.Dead += OnDead;
        }
        private void OnDisable()
        {
            _enemy.Death.Dead -= OnDead;
        }



        private void Start()
        {
            _inf = _enemy.EnemyInfo;
            StartCoroutine(StateChoosing());
        }
        private IEnumerator StateChoosing()
        {
            SetMoving();
            yield return new WaitForSeconds(_inf.TimeToStartShoot);
            while (true)
            {
                SetMoving();
                yield return new WaitForSeconds(Random.Range(_inf.MinTimeToMove, _inf.MaxTimeToMove));
                yield return StartCoroutine(SetShooting());

                SetMoving();
            }
        }
        protected virtual void SetMoving()
        {
            _enemy.EnemyMovement.StartMoving();
        }
        protected virtual IEnumerator SetShooting()
        {
            _enemy.EnemyMovement.StopMoving();

            int countOfShoot = Random.Range(_inf.MinCountOfShoot, _inf.MaxCountOfShoot);

            for (int i = 0; i < countOfShoot; i++)
            {
                yield return new WaitForSeconds(Random.Range(_inf.MinDelayOfShoot, _inf.MaxDelayOfShoot));
                Shoot();
            }
        }
        protected virtual void Shoot()
        {
            Player.Instance.Damagable.GetDamage(_inf.DamagePerHit);
            PlaySound();
            return;
        }
        protected virtual void OnDead()
        {
            StopAllCoroutines();
            _enemy.EnemyMovement.StopMoving();
        }
        private void PlaySound()
        {
            switch (_enemy.EnemyType)
            {
                case EnemyType.Soldier:
                    SoundManager.Instance.SoldierShoot(_enemy.Visual);
                    break;
                case EnemyType.Jagernaut:
                    SoundManager.Instance.JahherShoot(_enemy.Visual);
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

