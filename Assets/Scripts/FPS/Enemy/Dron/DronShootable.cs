using UnityEngine;
using DG.Tweening;


namespace FPS
{
    public class DronShootable : EnemyShootable
    {
        [SerializeField] private DisconectedPart _firstGun;
        [SerializeField] private DisconectedPart _secondGun;

        private Transform _firstGunPoint;
        private Transform _secondGunPoint;


        private int _currentCountOfWeapons = 2;

        private void OnEnable()
        {
            _firstGun.Disconected += MinusOneGun;
            _secondGun.Disconected += MinusOneGun;

            _firstGunPoint = _firstGun.transform.GetChild(0);
            _secondGunPoint = _secondGun.transform.GetChild(0);
        }
        private void OnDisable()
        {
            _firstGun.Disconected -= MinusOneGun;
            _secondGun.Disconected -= MinusOneGun;
        }
        private void MinusOneGun()
        {
            _currentCountOfWeapons--;


            if (_currentCountOfWeapons == 1)
            {
                ChangeValues(2);
            }
            else if (_currentCountOfWeapons == 0)
            {
                ChangeValues(0);
            }
        }

        private void ChangeValues(int indicator)
        {
            float value;

            if (indicator == 0)
            {
                value = 0;
            }
            else
            {
                value = indicator / (indicator * 2);
            }

            _inf.MinCountOfShoot = (int)(_inf.MinCountOfShoot * value);
            _inf.MaxCountOfShoot = (int)(_inf.MaxCountOfShoot * value);


            _inf.DamagePerHit *= value;

            _inf.MinDelayOfShoot *= indicator;
            _inf.MaxDelayOfShoot *= indicator;
        }

        protected override void Shoot()
        {
            SpawnParticle();
            base.Shoot();
        }
        private void SpawnParticle()
        {
            _firstGun.transform.parent.DOLookAt(Camera.main.transform.position, 0.3f);
            _secondGun.transform.parent.DOLookAt(Camera.main.transform.position, 0.3f);
            Instantiate(_shootParticle).transform.position = _firstGunPoint.position;
            Instantiate(_shootParticle).transform.position = _secondGunPoint.position;
        }
    }
}

