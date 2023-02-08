using System.Collections;
using UnityEngine;



namespace FPS
{
    public class RifleShootable : EnemyShootable
    {
        [SerializeField] private SoldierAnimationController _soldierAnimationController;
        [SerializeField] private Transform _shootPoint;


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
            if (_soldierAnimationController != null)
            {
                _soldierAnimationController.Shoot();
            }
            SpawnParticle();
            base.Shoot();
        }
        private void SpawnParticle()
        {
            Instantiate(_shootParticle).transform.position = _shootPoint.position;
        }
    }
}

