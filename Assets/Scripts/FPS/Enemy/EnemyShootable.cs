using System.Collections;
using UnityEngine;




namespace FPS
{
    public class EnemyShootable : MonoBehaviour
    {
        [SerializeField] protected Enemy _enemy;
        [SerializeField] protected ParticleSystem _shootParticle;
        [SerializeField] protected Transform _shootPoint;


        protected BulletTrailEffect _bulletTrailEffect;


        protected EnemyInfo _inf;


        private void OnEnable()
        {
            _enemy.Death.Dead += OnDead;
        }
        private void OnDisable()
        {
            _enemy.Death.Dead -= OnDead;
        }



        private void Awake()
        {
            _bulletTrailEffect = new BulletTrailEffect();
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
                if (CheckIsCanSeePlayer() == true && Player.Instance.Hidden.IsSafe == false)
                {
                    yield return StartCoroutine(SetShooting());
                }

                SetMoving();
            }
        }

        Ray ray;
        private bool CheckIsCanSeePlayer()
        {
           Vector3 playerDir = (Player.Instance.transform.position - _shootPoint.position).normalized;
            ray = new Ray(_shootPoint.position, playerDir);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 10);


            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return false;
            }
            return true;
        }
        private void OnDrawGizmos()
        {
            RaycastHit hit;
            Gizmos.color = Color.magenta;

            if (Physics.Raycast(ray, out hit))
            {
                Gizmos.DrawSphere(hit.point, 0.1f);
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
                if (Player.Instance.Hidden.IsSafe == false)
                {
                    yield return new WaitForSeconds(Random.Range(_inf.MinDelayOfShoot, _inf.MaxDelayOfShoot));
                    Shoot();
                }
            }
        }
        protected virtual void Shoot()
        {
            if (Player.Instance.Hidden.IsSafe == true)
            {
                return;
            }
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

