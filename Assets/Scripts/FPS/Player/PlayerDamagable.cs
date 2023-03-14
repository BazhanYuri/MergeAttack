using System.Collections;
using UnityEngine;


namespace FPS
{
    public class PlayerDamagable : Damagable
    {
        [SerializeField] private float _repairedValuePerSecond;

        private void Start()
        {
            GameManager.GameplayStarted += StartRepairHealth;
        }
        private void OnDisable()
        {
            GameManager.GameplayStarted -= StartRepairHealth;
        }
        private void StartRepairHealth()
        {

            StartCoroutine(RepairHealth());
        }    
        private IEnumerator RepairHealth()
        {
            while (true)
            {
                if (_currentHealth <= 100)
                {
                    yield return new WaitForSeconds(0.1f);
                    _currentHealth += _repairedValuePerSecond / 10f;
                    HealthUpdated();
                }
                yield return null;
            }
        }
    }
}

