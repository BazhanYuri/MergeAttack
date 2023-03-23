using System.Collections;
using UnityEngine;



namespace FPS
{
    public class RifleShootable : EnemyShootable
    {
        [SerializeField] private SoldierAnimationController _soldierAnimationController;


        protected override void SetMoving()
        {
            if (_soldierAnimationController != null)
            {
                _soldierAnimationController.Run();
            }
            base.SetMoving();
        }
        protected override IEnumerator SetShooting()
        {
            if (_soldierAnimationController != null)
            {
                _soldierAnimationController.Aiming();
            }
            return base.SetShooting();
        }
        protected override void Shoot()
        {
            base.Shoot();

            if (_soldierAnimationController != null)
            {
                _soldierAnimationController.Shoot();
            }
            SpawnParticle();
        }
        private void SpawnParticle()
        {
            Instantiate(_shootParticle).transform.position = _shootPoint.position;
            _bulletTrailEffect.ShowTrail(_inf.BulletPrefab, _shootPoint.position, Player.Instance.transform.position + Random.insideUnitSphere);
        }
    }
}

