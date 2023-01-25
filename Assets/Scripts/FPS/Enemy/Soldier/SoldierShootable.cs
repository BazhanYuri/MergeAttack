using System.Collections;
using UnityEngine;



namespace FPS
{
    public class SoldierShootable : EnemyShootable
    {
        [SerializeField] private SoldierAnimationController _soldierAnimationController;
        [SerializeField] private Transform _shootPoint;


        protected override void SetMoving()
        {
            _soldierAnimationController.Run();
            base.SetMoving();
        }
        protected override IEnumerator SetShooting()
        {
            _soldierAnimationController.Aiming();
            return base.SetShooting();
        }
        protected override void Shoot()
        {
            _soldierAnimationController.Shoot();
            SpawnParticle();
            base.Shoot();
        }
        private void SpawnParticle()
        {
            Instantiate(_shootParticle).transform.position = _shootPoint.position;
        }
    }
}

