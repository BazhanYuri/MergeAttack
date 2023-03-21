using System.Collections;
using UnityEngine;


namespace FPS
{
    public class PlayerDamagable : Damagable
    {
        [SerializeField] private float _repairedValuePerSecond;


        private bool _isOutOfAmmo = false;
        private bool _isOutOfGranates = true;


        private void Start()
        {
            GameManager.GameplayStarted += StartRepairHealth;
            Shootable.OutOfAmmo += OutOfAmmo;
            GranateChooser.ExpoEquiped += ThereIsGranate;
            Explosinable.NoGranate += NoGranates;
        }
        private void OnDisable()
        {
            GameManager.GameplayStarted -= StartRepairHealth;
            Shootable.OutOfAmmo -= OutOfAmmo;
            GranateChooser.ExpoEquiped -= ThereIsGranate;
            Explosinable.NoGranate -= NoGranates;
        }
        private void StartRepairHealth()
        {
            StartCoroutine(RepairHealth());
        }    
        private IEnumerator RepairHealth()
        {
            while (true)
            {
                if (_isOutOfAmmo == true && _isOutOfGranates == true)
                {
                    _repairedValuePerSecond = 0;
                }
                if (_currentHealth <= 100)
                {
                    yield return new WaitForSeconds(0.1f);
                    _currentHealth += _repairedValuePerSecond / 10f;
                    HealthUpdated();
                }
                yield return null;
            }
        }


        private void OutOfAmmo()
        {
            _isOutOfAmmo = true;
        }
        private void ThereIsGranate()
        {
            _isOutOfGranates = false;
        }
        private void NoGranates()
        {
            _isOutOfGranates = true;
        }
    }
}

